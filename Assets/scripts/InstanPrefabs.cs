using Mirror;
using System.Collections;
using System.Collections.Generic;
//using Unity.VisualScripting;
using UnityEngine;

public class InstanPrefabs : MonoBehaviour
{
    public GameObject preCube;
    public static GameObject postCube;
    // Start is called before the first frame update
    void Start()
    {
        postCube = preCube;
    }



    public static GameObject switchObj()
    {
        //postCube = preCube;
        return postCube;
    }


}
