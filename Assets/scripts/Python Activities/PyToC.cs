using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

namespace PyToCMerge
{
    public class PyToC
    {
        GameObject cpyConn;
        PrintCubeCPy cubeScript;
        private int connIDKey;

        //take connectionID 
        public string PrintCube(string name, int ID)
        {
            var dict = new Dictionary<string, string>() { { "err", "" } };
            ManualResetEvent mre = new ManualResetEvent(false);
             
            RunInMainThread.ExecuteInUpdate(() =>
            {
                dict["err"] = PrintCubeCPy.printThatCube(name, ID);
                mre.Set();
            });
            mre.WaitOne();
            
            return dict["err"];


        }
        public string addForceToObj(string name, int ConnID, float x, float y, float z)
        {
            ManualResetEvent mre = new ManualResetEvent(false);
            string err = "";
            var dict = new Dictionary<string, string>() {{ "err",""} };
            RunInMainThread.ExecuteInFixedUpdate(() =>
            
            
            {dict["err"] = PrintCubeCPy.addForce(name, ConnID, x, y, z);
                    mre.Set();
                try
                {
                    
                }
                catch (Exception e)
                {
                    Debug.Log("add force to obje err: " + e.ToString());

                    //err = e.ToString();
                    //Debug.Log(err);

                }
                //mre.WaitOne();
                //if (dict["err"] != "")
                //{
                //    Debug.Log("error happening at pytoC: " + dict["err"]);
                //    //throw new Exception(err);

                //}


            });
            //Debug.Log("error pytoC +: " + dict["err"]);
            //return dict["err"];
            mre.WaitOne();
            if (dict["err"] != "")
            {
                Debug.Log("error happening at pytoC: " + dict["err"]);
                //throw new Exception(dict["err"]);
                return dict["err"];

            }
            return dict["err"];
        }
        public string MovetheCube(string name, int ConnID, float x, float y, float z)
        {
            ManualResetEvent mre = new ManualResetEvent(false);
            string err = "";
            var dict = new Dictionary<string, string>() { { "err", "" } };
            RunInMainThread.ExecuteInFixedUpdate(() =>
            {
                try
                {
                    dict["err"] = PrintCubeCPy.moveObject(name, ConnID, x, y, z);
                    mre.Set();
                }
                catch (Exception e)
                {

                    Debug.Log("Move obj err: " + e.ToString());
                }

            });
            mre.WaitOne();
            return dict["err"];
            //if (dict["err"] != "")
            //{
            //    Debug.Log("error happening at pytoC");
            //    throw new Exception(dict["err"]);

            //}

        }

        public string DeleteObj(string objName, int ConnID)
        {
            ManualResetEvent mre = new ManualResetEvent(false);
            var dict = new Dictionary<string, string>() { { "err", "" } };
            string err = "";
            RunInMainThread.ExecuteInUpdate(() =>
            {
                try
                {
                    dict["err"] = PrintCubeCPy.delObject(objName, ConnID);
                    //remove from list

                    mre.Set();
                }
                catch (Exception e)
                {

                }

            });
            mre.WaitOne();
            Debug.Log("before err comes pytoC");
            
            return dict["err"];
        }
        //has best way to handle errors with unity api methods
        public string CubeColorChange(string objName, int ConnID, float r, float g, float b)
        {
            ManualResetEvent mre = new ManualResetEvent(false);
            var dict = new Dictionary<string, string>() { { "err", "" } };
            string err = "";
            RunInMainThread.ExecuteInUpdate(() =>
            {
                try
                {
                    dict["err"] = PrintCubeCPy.changeCubeColor(objName, ConnID, r, g, b);
                    mre.Set();
                }
                catch (Exception e)
                {                    
                    //err = e.ToString();
                    
                }
                
            });
            mre.WaitOne();
            Debug.Log("before err comes pytoC");
            //if (dict["err"] != "")
            //{
            //    Debug.Log("error happening at pytoC");
            //    throw new Exception(dict["err"]);
            //}
            return dict["err"];
        }

        public string RotateObj(string objName, int ConnID, float x, float y, float z)
        {
            ManualResetEvent mre = new ManualResetEvent(false);
            string err = "";
            var dict = new Dictionary<string, string>() { { "err", "" } };
            RunInMainThread.ExecuteInUpdate(() =>
            {
                try
                {
                    dict["err"] = PrintCubeCPy.RotateObject(objName, ConnID, x, y, z);
                    mre.Set();
                }
                catch (Exception e)
                {

                }
              
            });
            mre.WaitOne();
            Debug.Log("before err comes pytoC");
            //if (dict["err"] != "")
            //{
            //    Debug.Log("error happening at pytoC");
            //    throw new Exception(dict["err"]);
            //}
            return dict["err"];

        }

        public void GetPlayerPos()

        {
            Vector3 x = Vector3.zero;
            ManualResetEvent mre = new ManualResetEvent(false);
            RunInMainThread.ExecuteInUpdate(() =>
            {
               x = PrintCubeCPy.getPlayerData(connIDKey);
                mre.Set();
            });
            mre.WaitOne();
            Debug.Log("getplayerPos Finished, data: " + x.ToString());
        }
        public void Textstr()
        {
            Debug.Log("bamn bamn");
        }
        public string TextstrMainThr()
        {
            ManualResetEvent mre = new ManualResetEvent(false);
            string x = "45 P gee";
            RunInMainThread.ExecuteInUpdate(() =>
            {
               
                x =  x + "45 33";
                mre.Set();
            });
            mre.WaitOne();
            return x;
        }

    }

}


