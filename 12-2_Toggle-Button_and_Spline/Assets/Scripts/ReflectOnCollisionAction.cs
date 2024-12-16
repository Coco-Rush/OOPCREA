using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReflectOnCollisionAction : MonoBehaviour
{
    public ActOnCollision EventSource;
    MeshRenderer meshToChange;

    private void OnEnable()
    {
        EventSource.OnSomethingEnterTrigger += SomethingEntered;
        EventSource.OnSomethingExitTrigger += SomethingExited;
    }

    private void OnDisable()
    {
        EventSource.OnSomethingEnterTrigger -= SomethingEntered;
        EventSource.OnSomethingExitTrigger -= SomethingExited;
    }

    // Start is called before the first frame update
    void Start()
    {
        meshToChange = gameObject.GetComponent<MeshRenderer>();
        meshToChange.material = EventSource.deactivatedMaterial;
    }

    private void SomethingEntered()
    {
        meshToChange.material = EventSource.activatedMaterial;
    }

    private void SomethingExited()
    {
        meshToChange.material = EventSource.deactivatedMaterial;
    }
}
