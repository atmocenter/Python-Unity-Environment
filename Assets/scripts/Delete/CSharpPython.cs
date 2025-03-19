using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace pyunitycsharp
{
    public class CSharpPython 
    {
        RunInMainThread RunInMainThreadScript;
        GameObject cpyConn;
        PrintCubeCPy cubeScript;
        public  CSharpPython()
        {
            Debug.Log("starting up cpyConn");
            cpyConn = GameObject.Find("cpyconnector");
            cubeScript = cpyConn.GetComponent<PrintCubeCPy>();
            RunInMainThreadScript = new RunInMainThread();
        }


        
        // Start is called before the first frame update
        
        
        // Update is called once per frame
        void Update()
        {
        
        }
        
        public void printCubeUnity(string nme)
        {



            RunInMainThread.ExecuteInUpdate(() =>
            {
                //PrintCubeCPy.printThatCube(nme);
            });
                
        }

        public void CubeColorUnity(string objName, float r, float g, float b)
        {
            RunInMainThread.ExecuteInUpdate(() =>
            {
                //cubeScript.changeCubeColor(objName, r, g, b);

            });
        }

       
    }
}

