using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActOnCollision : MonoBehaviour
{ 
    //  Declare events other scripts can subscribe to
    public event Action OnSomethingEnterTrigger;
    public event Action OnSomethingExitTrigger;

    public Material deactivatedMaterial;
    public Material activatedMaterial;
    private MeshRenderer meshToChange;

    // Initialize deactivated display
    void Start()
    {
        meshToChange = gameObject.GetComponent<MeshRenderer>();     //  Initialize reference
        meshToChange.material = deactivatedMaterial;                //  Initialize color
        gameObject.GetComponent<BoxCollider>().isTrigger = true;    //  Make sure the collider is a trigger only
    }


    //  Collision Trigger detectors
    public void OnTriggerEnter(Collider other)
    {
        OnSomethingEnterTrigger?.Invoke();
        meshToChange.material = activatedMaterial;
    }

    public void OnTriggerExit(Collider other)
    {
        OnSomethingExitTrigger?.Invoke();
        meshToChange.material = deactivatedMaterial;
    }
}
