using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReflectOnButton : MonoBehaviour
{
    public ActOnButton EventSource;

    private MeshRenderer myMesh;

    // Start is called before the first frame update
    void OnEnable()
    {
        EventSource.OnSomethingEnteredTrigger += OnEntered;
        EventSource.OnSomethingExitedTrigger += OnExited;

        myMesh = gameObject.GetComponent<MeshRenderer>();
        myMesh.material = EventSource.deactivatedMat;
    }

        void OnEntered()
    {
        myMesh.material = EventSource.activatedMat;
    }

    
    void OnExited()
    {
        myMesh.material = EventSource.deactivatedMat;
    }
}
