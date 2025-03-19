using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class ObjClicked : MonoBehaviour, IPointerClickHandler
{
    int ConnID = -1;
    bool toggle = false;
    IventoryList InventoryListScript;
    public GameObject imageObj;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("start of start func" + ConnID);
        GameObject InvParent = GameObject.Find("InventoryList_Parent");
            if(InvParent != null )
        {
            Debug.Log("invParent not null");
            InventoryListScript = InvParent.GetComponent<IventoryList>();
            if( InventoryListScript != null )
            {
                Debug.Log("inv list script not null");
                ConnID = InventoryListScript.getConnID();
                Debug.Log("COnn ID at start = " + ConnID);
            }
        }
    }

   



    public void OnPointerClick(PointerEventData eventData)
    {
        string name = "";
         

        Transform txtChild = gameObject.transform.GetChild(1);
        if (txtChild != null)
        {
            TMP_Text textComp = txtChild.GetComponent<TMP_Text>();
            if (textComp != null)
            {
                name = textComp.text;
                Debug.Log(name + " txt items");
                if(ConnID !=- 1)
                {
                    name += "_" + ConnID;
                    if( InventoryListScript != null )
                    {
                        
                        InventoryListScript.itemClickedOnList(name,toggle);
                        if (imageObj.activeSelf == true)
                        {
                            imageObj.SetActive(false);
                             
                        }
                        else
                        {
                            imageObj.SetActive(true);
                             
                        }
                        if (toggle == false)
                        {

                            toggle = true;
                        }
                        else
                        {
                            toggle = false;
                        }

                       

                    }
                    // find gameobject
                }
                if( InventoryListScript == null ) 
                {
                    Debug.Log("InvListScript is null");
                    GameObject InvParent = GameObject.Find("InventoryList_Parent");
                    if (InvParent != null)
                    {
                        InventoryListScript = InvParent.GetComponent<IventoryList>();
                        if (InventoryListScript != null)
                        {
                            ConnID = InventoryListScript.getConnID();
                            Debug.Log("InvListScript is null new ConnID = "+ConnID);
                            if (ConnID != -1)
                            {
                                name += "_" + ConnID;
                                if (InventoryListScript != null)
                                {
                                   
                                    InventoryListScript.itemClickedOnList(name,toggle);
                                    if (imageObj.activeSelf == true)
                                    {
                                        imageObj.SetActive(false);
                                        Debug.Log("Image set false");
                                    }
                                    else
                                    {
                                        imageObj.SetActive(true);
                                        Debug.Log("Image set true");
                                    }
                                    if (toggle == false)
                                    {
                                        toggle = true;
                                    }
                                    else
                                    {
                                        toggle = false;
                                    }
                                    








                                }
                                // find gameobject
                            }
                        }
                    }
                }
                else
                {
                    Debug.Log("script not null");
                }
               
                //now get conn_id
            }
        }
        

    }
}
