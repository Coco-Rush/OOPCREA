using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SphereDestroy : MonoBehaviour
{
    private GameObject mySphere;
    // Awake is called before the first frame update
    void Awake()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter(Collision collision)
    {
        GameObject otherObj = collision.gameObject;
        if (otherObj.name == "Tocus")
        {
            Destroy(otherObj,1);
        }
    }
}
