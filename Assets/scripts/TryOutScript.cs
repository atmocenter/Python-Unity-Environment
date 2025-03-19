using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class TryOutScript : MonoBehaviour
{
    // code editor input field: //
    [SerializeField] TMP_InputField CeInputField;
    [SerializeField] string codeToTemplate;
    string[] codeList =
    {
//print cube
@"
#template code below  
#get the userID, automatically provided when connected to site  
import userInfo 
user_ID = userInfo.userID  
#bring cube into the environment, userID is needed 
from Virtualenvcontroller import VirtualEnvController 
environment = VirtualEnvController() 
environment.printCube('name1', user_ID)
",
//add force to cube
@"
#template code below  
#get the userID, automatically provided when connected to site  
import userInfo 
user_ID = userInfo.userID  
#bring cube into the environment, userID is needed 
from Virtualenvcontroller import VirtualEnvController 
environment = VirtualEnvController() 
#replace cube name with the name of your cube/object 
environment.applyForce('name1',user_ID, 2,1,1)
",
// move the cube
@"
#template code below  
#get the userID, automatically provided when connected to site  
import userInfo 
user_ID = userInfo.userID  
#bring cube into the environment, userID is needed 
from Virtualenvcontroller import VirtualEnvController 
environment = VirtualEnvController() 
#replace cube name with the name of your cube/object 
environment.MoveObj('name1',user_ID, 1,1,1)
",
//change cube color
@"
#template code below  
#get the userID, automatically provided when connected to site  
import userInfo 
user_ID = userInfo.userID  
#bring cube into the environment, userID is needed 
from Virtualenvcontroller import VirtualEnvController 
environment = VirtualEnvController() 
#replace cube name with the name of your cube/object 
environment.ChangeColor('name1',user_ID, 1,0,1)
",
//Rotate Cube
@"
#template code below  
#get the userID, automatically provided when connected to site  
import userInfo 
user_ID = userInfo.userID  
#bring cube into the environment, userID is needed 
from Virtualenvcontroller import VirtualEnvController 
environment = VirtualEnvController() 
#replace cube name with the name of your cube/object 
environment.RotateObj('name1',user_ID, 0,1,1)
",
//delete object
@"
#template code below  
#get the userID, automatically provided when connected to site  
import userInfo 
user_ID = userInfo.userID  
#bring cube into the environment, userID is needed 
from Virtualenvcontroller import VirtualEnvController 
environment = VirtualEnvController() 
#replace cube name with the name of your cube/object 
environment.DeleteObj('name1',user_ID)
",
@""

    };
   
    //Template To Editor (int i)
    // adds template script to code editor
    public void TemplateToEditor(int i)
    {
        CeInputField.text = codeList[i];
    }
}
