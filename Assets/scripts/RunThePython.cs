using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Python.Runtime;
using TMPro;
using System;
using System.IO;
using System.Net.Sockets;
using System.Net.WebSockets;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

using System.Net;
using Microsoft.Scripting.Utils;
using Microsoft.Scripting;
using System.Collections.Concurrent;
using UnityEngine.UI;
using System.Linq;
using Newtonsoft.Json;
using System.Text.RegularExpressions;
using Telepathy;
using PyToCMerge;
using System.Linq.Expressions;



public class RunThePython : NetworkBehaviour
{
    
    private int fileName = 0;
    private static readonly object _gilLock = new object();
 
    public UnityEvent triggerEvent;
    public mobileManger mobileManagerScript;
     
   
    public string pyString = "";
    [SerializeField] InGameTextEditor.TextEditor TeEditor;
    public TMP_InputField inputField;
    public TMP_Text PyOutputText;
    public GameObject PyConsole1;
    public TMP_Text PyConsolemobile;
    [SerializeField] Button StopBtn;
     
    //string connnectionID ="";
    [SerializeField] Button ExecuteBtn;
  
    [SerializeField] Button ExecuteBtnMobile;
     
    [SerializeField] GameObject TextArrayParent;

    [SerializeField] string pythonExecutiblePath;
    [SerializeField] string userScriptFolderPath_Server = @"/scripts/User_Scripts/";
    [SerializeField] string VirtualEnvModulePath_Server = @"/scripts/virtualenvironmentcontrollercopy.py";
    [SerializeField] string VirtualEnvModul = "\\Assets\\scripts\\virtualenvironmentcontrollercopy.py";
    public static ConcurrentDictionary<string, CancellationTokenSource> userScripts = new ConcurrentDictionary<string, CancellationTokenSource>();
    public static ConcurrentDictionary<string, CancelScripts> userScriptsCancelEvents = new ConcurrentDictionary<string, CancelScripts>();

    
    //command to run python code on server
    public Action runFunc;
 
    public void ExitConsole()
    {
        if (PyConsole1.activeSelf)
        {
            PyConsole1.SetActive(false);
        }
    }
    
     public void StopScript()
    {
        StopBtn.interactable = false;
        
        Debug.Log("stop script pressed");
            cmdStopScript();
 
       
    }

   async Task<string> stopScriptProcess (string name)
    {
        string allOutput = "";
        try
        {
            
            Debug.Log("name: " + name);
            System.Diagnostics.ProcessStartInfo Startinfo = new System.Diagnostics.ProcessStartInfo
            {
                FileName = "docker",
                Arguments = "kill " + name,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                UseShellExecute = false,
                RedirectStandardInput = true,
                WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden,

            };

            using (System.Diagnostics.Process process = System.Diagnostics.Process.Start(Startinfo))
            {
                //activate stop button

                string output = await process.StandardOutput.ReadToEndAsync();
                string err = await process.StandardError.ReadToEndAsync();
                process.WaitForExit();
                Debug.Log(output);
                Debug.Log(err);
                Console.WriteLine(output);
                Console.WriteLine(err);
                allOutput = output + "\n" + err;



                //return "successfully stopped";


            }
            
            
             
            Debug.Log("output: "+ allOutput );
            return allOutput;
        }
        
        catch (Exception ex)
        {
             
            Debug.Log(ex);
            return ex.ToString();
            
        }

    }
    [TargetRpc]
    public void ActiveStopBtn(NetworkConnection cli, bool toggle)
    {
        StopBtn.interactable = toggle;
    }


    [TargetRpc]
    public void RemoveFromListAfterDeletion(NetworkConnection cli, string name)
    {
        //reference content gobj
        Debug.Log("Name : " + name);
        GameObject ItemParent = GameObject.Find("InventoryList_Parent");
        
        if (ItemParent != null)
        {
            Debug.Log("Item Parent Found " );
            Transform childTransform = ItemParent.transform.Find("InventoryPanel").Find("Viewport").Find("Content").Find(name);

            if (childTransform != null)
            {
                GameObject childObj = childTransform.gameObject;
                if (childObj != null)
                {
                    Debug.Log("child obj found ");
                    Destroy(childObj);
                }
            }
        }
    }

    [Command(requiresAuthority = false)]
    async void cmdStopScript(NetworkConnectionToClient sender = null)
    {

        
        NetworkConnectionToClient netId = sender.identity.connectionToClient;

        int ConnID = netId.connectionId;
        //start process to stop script docker container
       
        string name = "id_" + ConnID.ToString() + "container_0";
        string resp = await stopScriptProcess(name);
        Debug.Log("resp: "+resp);
        string check = "";
        //CancelUserScripts.TryRemove(ConnID.ToString(), out check);
        if (check != "")
        {
            Debug.Log("script entry not successfully removed");
        }
        else
        {
            Debug.Log("COnnID: " + ConnID + " entry removed");
        }
 



    }

     void Start()
    {
         _ = runTCPServer();
    }

    [Server]
    public void SetupEnginePy()
    {

    }
    [Server]
    public static void SetupPy2()
    {
    }

    //pythonnet 
    [Server]
    public static void SetupPy()
    {
       
        Runtime.PythonDLL = @"/usr/lib64/libpython3.so";
        PythonEngine.Initialize();
        PythonEngine.BeginAllowThreads();

    }


    [Command(requiresAuthority = false)]
    void stopFunctionCmd (NetworkConnectionToClient sender = null)
    {
        NetworkConnectionToClient netId = sender.identity.connectionToClient;
      
        int ConnID = netId.connectionId;
        userScripts[ConnID.ToString()].Cancel();
        if (userScripts.ContainsKey(ConnID.ToString()) == true)
        {
            CancellationTokenSource cts = userScripts[ConnID.ToString()];
            cts.Cancel();
            //scriptEnginepub = null;
            CancelScripts cScript = userScriptsCancelEvents[ConnID.ToString()];
            cScript.triggerEvent();


            bool success = userScripts.TryRemove(ConnID.ToString(), out CancellationTokenSource val);
            if (success == true)
            {
                Debug.Log(ConnID.ToString() + " Successfully Removed");
            }
            else
            {
                Debug.Log(ConnID.ToString() + " not removed");
                Debug.Log(userScripts);
            }
        }
 
    }

    [TargetRpc]
    public void TargetPythonOutput(NetworkConnection target, string output)
    {

        PyOutputText.text = output;
        ExecuteBtn.interactable = true;
     
    }


    //[Command]
    [Command(requiresAuthority = false)]
    async void CmdRunPy(string pyStr, NetworkConnectionToClient sender = null)
    {
        NetworkConnectionToClient netId = sender.identity.connectionToClient;
        NetworkConnection cli = NetworkServer.connections[netId.connectionId];
        
       
        StringWriter x = new StringWriter();
        int connID = netId.connectionId;
         

        try
            {

#if UNITY_EDITOR || UNITY_STANDALONE_LINUX
            TaskCompletionSource<int> pyTasktcs = new TaskCompletionSource<int>();
           
            var cts = new CancellationTokenSource();
 
            userScripts[cli.connectionId.ToString()] = cts;
             
            Debug.Log($"Main Thread: ID = {Thread.CurrentThread.ManagedThreadId}, IsBackground = {Thread.CurrentThread.IsBackground}");
 

            string response =  await RunPythonProcess(pyStr, connID,cli);
            Debug.Log(response);
            TargetPythonOutput(cli, response);
#endif
 
        }

        catch (OperationCanceledException e)
        {
            
            Debug.Log("error occured: "+e.ToString());
            TargetPythonOutput(cli, e.Message);
           
        }
        catch (Exception e)
            {
                Debug.Log("error occured: " + e.ToString());
                TargetPythonOutput(cli, e.Message);
        }
    

    }

   

   


    public void CallCmdRunPy()
    {
        ExecuteBtn.interactable = false;
        Debug.Log($"Main Thread: ID = {Thread.CurrentThread.ManagedThreadId}, IsBackground = {Thread.CurrentThread.IsBackground}");


        string StrPy = inputField.text.ToString();
         
        //add try catch
        StrPy += "\nimport Virtualenvcontroller \nVirtualenvcontroller.stop.set()\n";
        
        string exceptHook1 = "import sys\ndef custom_exception(exc_type, exc_value, exc_traceback):\n\t";
        string exceptHook2 = "import traceback\n\tprint(exc_value)\n\ttraceback.print_tb(exc_traceback)\n\timport Virtualenvcontroller\n\tVirtualenvcontroller.stop.set()\n\tsys.exit()\nsys.excepthook = custom_exception \n";
        StrPy = exceptHook1 + exceptHook2 + StrPy;
        Debug.Log(StrPy);

        CmdRunPy(StrPy);
    }

    //coroutine

 

    public void CallCmdRunPyTxtEditor()
    {
        ExecuteBtn.interactable = false;
       
        int count = TextArrayParent.transform.childCount;
        TeEditor.prepForExecution(count);
        Invoke("callOnDelay", 1f);

    }

     void callOnDelay()
    {

        Debug.Log("Call on Delay");
        
        string StrPy = getEditorText();
        Debug.Log(StrPy);
        //add try catch
        StrPy += "\nimport Virtualenvcontroller \nVirtualenvcontroller.stop.set()\n";
        string userPy ="";
        //string pyTabNs = StrPy.Replace("\n", "\n\t");
        //pyTabNs = "\t" + pyTabNs;
        //string exceptBlock = "\nexcept Exception as e:\n\timport Virtualenvcontroller\n\tVirtualenvcontroller.stop.set()\n\traise\n\t"
        //    ;
        string exceptHook1 = "import sys\ndef custom_exception(exc_type, exc_value, exc_traceback):\n\t";
        string exceptHook2 = "import traceback\n\tprint(exc_value)\n\ttraceback.print_tb(exc_traceback)\n\timport Virtualenvcontroller\n\tVirtualenvcontroller.stop.set()\n\tsys.exit()\nsys.excepthook = custom_exception \n";
        StrPy = exceptHook1 + exceptHook2 + StrPy;
        Debug.Log(StrPy);
        
        CmdRunPy(StrPy);

    }


   
   




     async Task runTCPServer()
    {
        TcpListener server = null;
        IPAddress address = IPAddress.Parse("127.0.0.1");
        server = new TcpListener(address, 5000);
        server.Start();
        // Buffer for reading data
        Debug.Log("Waiting for a connection... ");

        bool ServerOn = true;
        while (ServerOn)
        {
            TcpClient client = await server.AcceptTcpClientAsync();
 
            _ = Task.Run(() => 
            { 
                handleClientComms(client); 
            });
        }
    }
  

   
  

    string dataHandlerFuncCaller(string message)
    {
        //data format:
        //object data = {Method: methodName, args: [1,23, "dafd" ...]}

        PyToC queueUnity = new PyToC();
        PyMessages dataSent = JsonConvert.DeserializeObject<PyMessages>(message);
        Debug.Log("dataSent: " + dataSent.args);
        
        if (dataSent == null)
        {
            return "data not recieved";
        }
        else
        {
            //Debug.Log()
          
            if(dataSent.Type != null)
            {
                Debug.Log(dataSent.Type);
            }
            // PRINT CUBE //
            if (dataSent.Method == "PrintCube")
            {
                 
                try
                {
                    string name = dataSent.args[0].ToString();
                    string idTemp = dataSent.args[1].ToString();
                     
                    int idP = Int32.Parse(idTemp);
                    
                    string resp = queueUnity.PrintCube(dataSent.args[0].ToString(), idP);
                     if(resp != "")
                    {
                        return resp;
                    }
                }
                catch (Exception e)
                {

                    return e.Message.ToString();
                }
                return "success";
            }
            // APPLY FORCE// 
            if (dataSent.Method == "applyForce")
            {
                 
                try
                {
                    string name = dataSent.args[0].ToString();
                    int ConnecID = Int32.Parse(dataSent.args[1].ToString());
                    float x = float.Parse(dataSent.args[2].ToString());
                    float y = float.Parse(dataSent.args[3].ToString());
                    float z = float.Parse(dataSent.args[4].ToString());

                     
                    string resp = queueUnity.addForceToObj(dataSent.args[0].ToString(), ConnecID, x,y,z);
                    if(resp != "") 
                    {
                        Debug.Log(resp);
                        return resp;
                    }
                     
                }
                catch (Exception e)
                {
                    Debug.Log("apply force exception seen");
                    Debug.Log(e);
                    return e.ToString();
                }
                return "success";
            }
            // CHANGE COLOR //
            if (dataSent.Method == "ChangeColor")
            {
               
                try
                {
                    string name = dataSent.args[0].ToString();
                    int ConnecID = int.Parse(dataSent.args[1].ToString());
                    float x = float.Parse(dataSent.args[2].ToString());
                    float y = float.Parse(dataSent.args[3].ToString());
                    float z = float.Parse(dataSent.args[4].ToString());


                    string response = queueUnity.CubeColorChange(dataSent.args[0].ToString(), ConnecID, x, y, z);
                     
                    if (response != "")
                    {
                        return response;
                    }
                    else
                    {
                        return "success";
                    }
                }
                catch (Exception e)
                {
                    Debug.Log(e);
                    return e.ToString();
                }
                
            }

            // DELETE OBJECT //
            if (dataSent.Method == "DeleteObj")
            {
                ;
              
                try
                {
                    string name = dataSent.args[0].ToString();
                    int ConnecID = int.Parse(dataSent.args[1].ToString());
                    string response = queueUnity.DeleteObj(name, ConnecID);
                    
                    if (response != "")
                    {
                        return response;
                    }
                    else
                    {
                        return "success";
                    }
                }
                catch (Exception e)
                {
                    Debug.Log(e);
                    return e.ToString();
                }

            }

            // MOVE OBJECT //
            if (dataSent.Method == "MoveObj")
            {
               
                try
                {
                    string name = dataSent.args[0].ToString();
                    int ConnecID = int.Parse(dataSent.args[1].ToString());
                    float x = float.Parse(dataSent.args[2].ToString());
                    float y = float.Parse(dataSent.args[3].ToString());
                    float z = float.Parse(dataSent.args[4].ToString());


                    string response = queueUnity.MovetheCube(dataSent.args[0].ToString(), ConnecID, x, y, z);
                   
                    if (response != "")
                    {
                        return response;
                    }
                    else
                    {
                        return "success";
                    }
                }
                catch (Exception e)
                {
                    Debug.Log(e);
                    return e.ToString();
                }

            }
            // ROTATE //
            if (dataSent.Method == "Rotate")
            {
                
                try
                {
                    string name = dataSent.args[0].ToString();
                    int ConnecID = int.Parse(dataSent.args[1].ToString());
                    float x = float.Parse(dataSent.args[2].ToString());
                    float y = float.Parse(dataSent.args[3].ToString());
                    float z = float.Parse(dataSent.args[4].ToString());


                   string response =  queueUnity.RotateObj(dataSent.args[0].ToString(),ConnecID, x, y, z);
                  
                    if(response != "") {
                        return response;
                    }
                    else
                    {
                        return "success";
                    }
                }
                catch (Exception e)
                {
                    Debug.Log(e);
                    return e.ToString();
                }
                
            }
        }
        return "success";
    }


    public void FindGameObj()
    {
        GameObject gObj = GameObject.Find("txt_1");
        if (gObj != null)
        {
            Debug.Log("gameobject was found: "+gObj.name);
        }
        else
        {
            Debug.Log("gameobject not found");
        }
    }

    void handleClientComms(TcpClient client)
    {
        int i;
        Byte[] bytes = new Byte[4000];
        String data = null;

        //network stream to read data from client
        NetworkStream stream = client.GetStream();
     
        //read data from stream place into bytes array. Once data is read it is removed from stream.
        //method returns the amount of data which was read once it == 0 means there is no more data left


        //should be fixed:
        while ((i = stream.Read(bytes, 0, bytes.Length)) != 0)
        {
            data = System.Text.Encoding.ASCII.GetString(bytes, 0, i);
            Debug.Log(data);
            string resp = dataHandlerFuncCaller(data);
            byte[] msg = System.Text.Encoding.ASCII.GetBytes(resp);

            // Send back a response.
            stream.Write(msg, 0, msg.Length);
            Debug.Log("Sent: {0}" + data);

        }
        client.Close();

    }
   async Task<string> stopProcess(string name)
    {
        string allOutput = "";
        System.Diagnostics.ProcessStartInfo Startinfo = new System.Diagnostics.ProcessStartInfo
        {
            FileName = "docker",
            Arguments = "stop " + name,
            RedirectStandardOutput = true,
            RedirectStandardError = true,
            UseShellExecute = false,
            RedirectStandardInput = true,
            WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden,

        };

        using (System.Diagnostics.Process process = System.Diagnostics.Process.Start(Startinfo))
        {
            string output = await process.StandardOutput.ReadToEndAsync();
            string err = await process.StandardError.ReadToEndAsync();
            process.WaitForExit();
            Debug.Log(output);
            Debug.Log(err);
            Console.WriteLine(output);
            Console.WriteLine(err);
            allOutput = output + "\n" + err;

        }
        return allOutput;


    }


    async Task<string> RunPythonProcess(string pyCode, int connID, NetworkConnection cli)
    {
        //setup Args for command
        string allOutput = "";
        string currDirectory = "";
        string classFile = "";

        string fileString = "userID =" + connID;
        string moduleFileName = "userInfo";
#if UNITY_EDITOR
        Debug.Log("editor runpy");
        currDirectory = Environment.CurrentDirectory + @"\Assets\scripts\User_Scripts\";
        classFile = @"-v "+Environment.CurrentDirectory +VirtualEnvModul + @":/Virtualenvcontroller.py";
        //classFile = "";

#elif UNITY_STANDALONE_LINUX
    Debug.Log("editor runpy linux");
     currDirectory = Environment.CurrentDirectory + @"/scripts/User_Scripts/";
     classFile = @"-v "+ Environment.CurrentDirectory +VirtualEnvModulePath_Server +  @":/Virtualenvcontroller.py";
#endif

        currDirectory = currDirectory.Replace(@"\", @"/");
        string modCurrDirectory = currDirectory;

        //python script:
        //create file name
        //create file
        while (File.Exists(currDirectory + fileName.ToString() + ".py"))
        {
            fileName += 1;
        }
        int modPrefix = 0;
 
        //python module script for userID:
        //create file name
        //create file
        while (File.Exists(currDirectory + moduleFileName +modPrefix.ToString()+ ".py"))
        {
            modPrefix = modPrefix + 1;
         }
        moduleFileName += modPrefix.ToString();
 
        currDirectory += fileName.ToString() + ".py";
        modCurrDirectory += moduleFileName + ".py";
 
        File.WriteAllText(currDirectory, pyCode);
        File.WriteAllText(modCurrDirectory, fileString);
 

        //container naming convention
        //id_1_container_0

        string nameCont = "id_" + connID.ToString() + "container_0";
 
        //CancelUserScripts[connID.ToString()] = nameCont;
        string Args = @"run --rm -P  --name " + nameCont + " -v " + currDirectory + ":/pyscript.py " + classFile + " -v " + modCurrDirectory + ":/userInfo.py " + " python python /pyscript.py";


        //start process runs docker container that runs python script
        System.Diagnostics.ProcessStartInfo Startinfo = new System.Diagnostics.ProcessStartInfo
        {
            //docker
            FileName = "docker",
            Arguments = Args,
            RedirectStandardOutput = true,
            RedirectStandardError = true,
            UseShellExecute = false,
            RedirectStandardInput = true,
            WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden,
        };

        using (System.Diagnostics.Process process = System.Diagnostics.Process.Start(Startinfo))
        {
            //activate stop button
            ActiveStopBtn(cli,true);
            string output = await process.StandardOutput.ReadToEndAsync();
            string err = await process.StandardError.ReadToEndAsync();
            process.WaitForExit();

            Debug.Log("see output " + output + "see err: " + err);
            if(err != "" && err != null)
            {
                //// fix line error #
                //string pattern = @"(?/pyscript.py, <=line )\d+";
                //Match mtch = Regex.Match(err, pattern);
                //if(mtch.Success)
                //{
                //    Debug.Log("num line"+ mtch.Value.ToString());
                //    int num = int.Parse(mtch.Value);
                //    num = num - 9;
                //    err = Regex.Replace(err, pattern, num.ToString());

                //}
            }
           
            allOutput = output + "\n" + err;

             
        }
        Debug.Log(allOutput);
        //deactivate btn
        ActiveStopBtn(cli, false);
     
        return allOutput;
    }

    

    public string getEditorText()
    {
        string strText = "";
        int count = TextArrayParent.transform.childCount;
        Debug.Log("count of txt editor: " + count);
       
        for (int i = 0; i < count; i++)
        {
            Transform gameObjTransform = TextArrayParent.transform.GetChild(i);
            GameObject childObj = gameObjTransform.gameObject;
            Debug.Log(childObj.name);
            if (childObj.name =="Main Text")
            {
                //Debug.Log(childObj.name);
                continue;
            }
            Text childText1 = childObj.GetComponent<Text>();
            strText += childText1.text +"\n";
            
        }
        return strText;
       
    }


}
