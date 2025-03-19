
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Text;


public class MyTextWriter : TextWriter
{
    private StringWriter _stringWriter;
    public MyTextWriter()
    { 
        _stringWriter = new StringWriter();

    }
   

    //public override Encoding Encoding => Encoding.UTF8;

    public override Encoding Encoding
    {
        get { return Encoding.UTF8; }
    }

    public override void Write(char value)
    {
        _stringWriter.Write(value);
        //Debug.Log("write to string: " + _stringWriter.ToString());
        //Debug.Log("class write char: " + value);
    }

    public override void Write(string value)
    {
        _stringWriter.Write(value);
        //Debug.Log("write to string: " + _stringWriter.ToString());
        //Debug.Log("class write: " + value);
    }

    public override void WriteLine(string value)
    {
        _stringWriter.WriteLine(value);
        //Debug.Log("write to string: "+ _stringWriter.ToString() );
        //Debug.Log("class writeline: " + value);
    }

    public string GetCapturedText()
    {
        //Debug.Log(_stringWriter.ToString());
        string x = _stringWriter.ToString();
        //Debug.Log("testing: " + x);
        return x;
    }

}
