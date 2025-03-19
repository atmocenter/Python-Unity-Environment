using Mirror;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
//using MySql.Data.MySqlClient;
using System.Text;
using System.Security.Cryptography;
using MySqlConnector;
using TMPro;
using Newtonsoft.Json.Linq;

public class Authentication : NetworkBehaviour
{   
    // Database configuration
    [SerializeField] string db_server = "";
    [SerializeField] string db_name = "";
    [SerializeField] string db_user = "";
    [SerializeField] string db_password = "";
    [SerializeField] string db_table = "";
    
    string webToken = "";
 

    string userName1;
    string password2;
   
    [SerializeField] TMP_InputField userField;
    [SerializeField] TMP_InputField passField;
    [SerializeField] httpRequest httpReqScript;
    [SerializeField] GameObject LoginScreen;
    [SerializeField] NetworkManager myNetworkManager;
    [SerializeField] TMP_Text TextObj;

    [SerializeField] GameObject LoadingSpinner;

    public void login()
    {
        //get username
        userName1 = userField.text;
        Debug.Log(userName1 + " inputted");

        //get password
        password2 = passField.text;

        //deactivate submit button
        //have a guest account
//#if UNITY_ANDROID 
//       TextObj.text = "unity android detected";
//        myNetworkManager.StartClient();
//#endif
        LoadingSpinner.SetActive(true);
        //set input deactive
        userField.interactable = false;
        passField.interactable = false;
        runAuth(userName1, password2);

    }


    public void guestLogin()
    {
        //deactivate lms button and login screen
        LoginScreen.SetActive(false);
        
    }

   
    [Command(requiresAuthority = false)]
    async void runAuth(string uName, string passWrd, NetworkConnectionToClient sender = null)
    {
        NetworkConnectionToClient netId = sender.identity.connectionToClient;
        NetworkConnection cli = NetworkServer.connections[netId.connectionId];
       
        await auth(uName, passWrd,cli);
        
    }

    //authPass 
    //return a boolean whether pass or fail
    public async Task auth(string uName, string passWrd, NetworkConnection cli)
    {
      
        string userID = "";
        bool passAuth = false;
        string dbHash = "";
        string saltSplit = "";
        string roundsSplit = "";
        string hashFrmDB = "";
        string connectionString = "Server=" + db_server + ";Database=" + db_name + ";User ID=" + db_user + ";Password=" + db_password + ";";
        try
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
                    {
                        connection.Open();
                        string query = "SELECT password FROM " + db_table + " WHERE username = @username";
                        using (MySqlCommand command = new MySqlCommand(query, connection))
                        {
                            Debug.Log("conn about to query");
                            command.Parameters.AddWithValue("@username", uName);
                            using (MySqlDataReader reader = command.ExecuteReader())
                            {
                                if (reader.Read())
                                {
                                    string storedHash = reader["password"].ToString();
                                    Debug.Log("hash: " + storedHash);
                                    string[] hashSplit = storedHash.Split("$");
                                    dbHash = hashSplit[hashSplit.Length-1];
                                    Debug.Log("DB hash: " + dbHash);
                                    saltSplit = hashSplit[hashSplit.Length - 2];
                                    Debug.Log("salt hash: " + saltSplit);
                                    roundsSplit = hashSplit[hashSplit.Length - 3];
                            roundsSplit = roundsSplit.Substring(7);
                                    Debug.Log("rounds: " + roundsSplit);
                                     
                                    if (storedHash == "" || storedHash == null)
                                    {
                                        return ;
                                    }
                                    // get salt // get salt
                                    //string storedSalt = reader["password"].ToString();
                                    //if (storedHash == "" || storedHash == null)
                                    //{
                                    //    return;
                                    //}
                                }
                            }
                        }
                    }
        }
        catch (Exception e)
        {
            Debug.Log(e);
        }

#if UNITY_EDITOR
        string currDirectory = Environment.CurrentDirectory + @"\Assets\scripts\authpy.py";
#elif UNITY_STANDALONE_LINUX

string currDirectory = Environment.CurrentDirectory +@"/scripts/authpy.py";
#else
string currDirectory="";
#endif
        string Args = @"run --rm -P  -v " + currDirectory + ":/auth.py " + " python python /auth.py " + passWrd + " " + saltSplit +" " + roundsSplit;

        Debug.Log(Args);



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
            
            
            string output = await process.StandardOutput.ReadToEndAsync();
            string err = await process.StandardError.ReadToEndAsync();
            process.WaitForExit();

            Debug.Log(output);
            Debug.Log("see output " + output);
            string[] outArr = output.Split("$");
            string hashChk = outArr[outArr.Length-1];
            
            Debug.Log("Array Length: "+outArr.Length);

      

            Debug.Log("db hash: " + dbHash);
            Debug.Log("created hash: " + hashChk);
            dbHash= dbHash.Trim(' ');
            hashChk = hashChk.Trim();
            //compare the hash from db and hash created

            if (hashChk == dbHash )
            {
                Debug.Log("hash is the same");

                passAuth = true;
             

            }
            else
            {
                Debug.Log("not same");
            }
            if (err != "" && err != null)
            {

                Debug.Log(err);
            }
           
        }

        if (passAuth == true) 
        
        {
            try
            {
                Debug.Log("pass AUthh True");
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();
                    string query = "SELECT id FROM " + db_table + " WHERE username = @username";
                    Debug.Log("connection sql open");
                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                      ;
                        command.Parameters.AddWithValue("@username", uName);
                        Debug.Log(uName);
                        Debug.Log(command.CommandText);
                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            Debug.Log("created reader");
                            if (reader.Read())
                            {
                                userID = reader["id"].ToString();
                                Debug.Log("stored ID: " + userID);
                            
                             
                            }
                        }
                    }
                  
                }

                if(userID != "")
                {
                    Debug.Log("user ID not == to nothing");
                    string new_db_name = "unity";
                    string webTkns_table = "xr_webtokens";
                    connectionString = "Server=" + db_server + ";Database=" + new_db_name + ";User ID=" + db_user + ";Password=" + db_password + ";";
                    using (MySqlConnection connection = new MySqlConnection(connectionString))
                    {
                        Debug.Log("start connection userID");
                        connection.Open();
                        Debug.Log("conn open");             

                        string query = "SELECT webtoken FROM " + webTkns_table + " WHERE mdl45_user_id = " + userID;
                        using (MySqlCommand command = new MySqlCommand(query, connection))
                        {
                            Debug.Log("new command");
                           
                            Debug.Log(command.CommandText);
                            using (MySqlDataReader reader = command.ExecuteReader())
                            {
                                if (reader.Read())
                                {
                                    webToken = reader["webtoken"].ToString();
                                    Debug.Log("web token : " + webToken);

                                 
                                }
                            }
                        }

                    }
                    if(webToken != "")
                    {
                  
                        
                        UpdateTknToUser(cli,webToken, passAuth);
                        
                    }
                    else
                    {
                        UpdateTknToUser(cli, webToken, passAuth);
                    }
                }

            }
            catch (Exception ex) 
            { 
                Debug.Log(ex);
            }
            
            
        }
        else
        {
            UpdateTknToUser(cli, "", passAuth);
        }
    }


    public void cmdUpdateTk()
    {

    }
    


    [TargetRpc]
    void UpdateTknToUser(NetworkConnection target, string tkn, bool LoginSuccess)
    {
        //#if !UNITY_EDITOR
        //        myNetworkManager.StartClient();
        //#endif
        if (LoginSuccess == true)
        {
            LoginScreen.SetActive(false);
            userField.text = "";
            passField.text = "";
            userField.interactable = true;
            passField.interactable = true;
            LoadingSpinner.SetActive(false);
            httpReqScript.GetTokenFromHandler(tkn);
            httpReqScript.getLMSContent();
        }
        else
        {
            userField.interactable = true;
            passField.interactable = true;
            LoadingSpinner.SetActive(false);
        }
    }




}
  