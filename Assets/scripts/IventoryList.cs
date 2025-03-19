using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Mirror;

public class IventoryList : MonoBehaviour
{
    public GameObject UITemp;
    public GameObject ItemParent;
    private int ConnID =-1;
    PrimScript ObjPrimScript;
    string currName = "";
    public PrintCubeCPy cubeCpyScript;
    [SerializeField] private GameObject InvPanel;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //addd to  list
    //will be called in primScript when item is first instantiated or on server via rpc
    //requires name
    public void AddToList()
    {
        GameObject newItem = Instantiate(UITemp);
        //get btn child
        Transform btnExitTransform = newItem.transform.Find("Button");
        GameObject ExitBtn = btnExitTransform.gameObject;
        Button btnComp = ExitBtn.GetComponent<Button>();
        btnComp.onClick.AddListener(() =>
        {
            RemoveFromList(ExitBtn);
        });
        newItem.transform.SetParent(ItemParent.transform, false);

    }
    public void setConnID(int idVal)
    {
        ConnID = idVal;
    }
    public void AddToList(string name)
    {
        GameObject newItem = Instantiate(UITemp);
        //get btn child
        newItem.name = name;
        Transform nameText = newItem.transform.Find("Text (TMP)");
        GameObject nameObj = nameText.gameObject;
        TMP_Text nameTextObj = nameObj.GetComponent<TMP_Text>();
        nameTextObj.text = name;
        Transform btnExitTransform = newItem.transform.Find("Button");
        GameObject ExitBtn = btnExitTransform.gameObject;
        Button btnComp = ExitBtn.GetComponent<Button>();
        btnComp.onClick.AddListener(() =>
        {
            RemoveFromList(ExitBtn);
        });
        newItem.transform.SetParent(ItemParent.transform, false);

    }

    //[TargetRpc]
    //public void RemoveFromListAfterDeletion(NetworkConnection cli, string name)
    //{
    //    //reference content gobj
    //    Transform childTransform = ItemParent.transform.Find(name);
    //}
    //btn pressed
    //get parent
    //remove parent

    public void RemoveItemsOnList()
    {
        //remove all items within content
        // Loop through all children of the parentObject
        foreach (Transform child in ItemParent.transform)
        {
            // Destroy each child GameObject
            Destroy(child.gameObject);
        }
    }

    public void DeactivateObj()
    {

        InvPanel.SetActive(false);
    }

    public void RemoveFromList(GameObject itemBtn)
    {
        string name = "";
        Transform parentTrans = itemBtn.transform.parent;
        GameObject parentObj = parentTrans.gameObject;
        // get the child named Text (TMP)
        Transform txtChild = parentObj.transform.GetChild(1);
        if(txtChild != null) 
        { 
            TMP_Text textComp = txtChild.GetComponent<TMP_Text>();
            if(textComp != null)
            {
                name = textComp.text;
                Debug.Log(name + " txt items");
            }
        }
        
        Destroy(parentObj);


        // remove actual gameobject
        if(ConnID != -1)
        {
            //cubeCpyScript.delObjectFromUI(name, ConnID);
            string fullName = name + "_" + ConnID;
            GameObject primObj = GameObject.Find(fullName);
            Debug.Log("delete : " + fullName);
            if (primObj != null)
            {
                PrimScript primObjScript = primObj.GetComponent<PrimScript>();
                if (primObjScript != null)
                {
                    primObjScript.CallCmdDeleteObj();
                }
            }
        }
        

    }
    public int getConnID()
    {
       
            return ConnID;
        
       
    }
    public void itemClickedOnList(string name, bool toggle)
    {
        //change background 
        //make obj glow
        if(currName == name)
        {
            return;
        }
        GameObject primObj = GameObject.Find(name);
        if(primObj != null)
        {
            ObjPrimScript = primObj.GetComponent<PrimScript>();
            if(ObjPrimScript != null)
            {
                Debug.Log("itemclickedselected to highligight glow");
                if(toggle == false)
                {
                    ObjPrimScript.itemClickedHighlight();
                }
                else
                {
                    ObjPrimScript.itemDeselected();
                }
                
            }
        }
    }
}
