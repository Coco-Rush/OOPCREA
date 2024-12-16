using Oculus.Interaction;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActOnButton : MonoBehaviour
{
    public GameObject StateDisplay;
    public Material activatedMat;
    public Material deactivatedMat;

    private PokeInteractable myPokeInteract;

    public event Action OnSomethingEnteredTrigger;
    public event Action OnSomethingExitedTrigger;

    private void OnEnable()
    {
        //  Get a reference to the Poke interactable
        myPokeInteract = 
                 gameObject.GetComponent<PokeInteractable>();
        myPokeInteract.WhenPointerEventRaised += OnPointerEvent;
        StateDisplay.GetComponent<MeshRenderer>().material = deactivatedMat;
    }

    private void OnDisable()
    {
        myPokeInteract.WhenPointerEventRaised -= OnPointerEvent;
    }


    void OnPointerEvent(PointerEvent args)
    {
        switch (args.Type)
        {
            case PointerEventType.Select:
                StateDisplay.GetComponent<MeshRenderer>().material = activatedMat;
                OnSomethingEnteredTrigger?.Invoke();
                break;

            case PointerEventType.Unselect:
                StateDisplay.GetComponent<MeshRenderer>().material = deactivatedMat;
                OnSomethingExitedTrigger?.Invoke();
                break;

            default:  break;
        }
    }
}
