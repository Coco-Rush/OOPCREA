using Oculus.Interaction;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActOnButton : MonoBehaviour
{
    public GameObject ButtonCore;
    public Material activatedMat;
    public Material deactivatedMat;

    private PokeInteractable myPokeInteract;

    public event Action ToggleOn;
    public event Action ToggleOff;

    private bool buttonActivated = false;

    private void OnEnable()
    {
        //  Get a reference to the Poke interactable
        myPokeInteract = 
                 gameObject.GetComponent<PokeInteractable>();
        myPokeInteract.WhenPointerEventRaised += OnPointerEvent;
        ButtonCore.GetComponent<MeshRenderer>().material = deactivatedMat;
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
                if (buttonActivated == true)
                {
                    buttonActivated = false;
                    ButtonCore.GetComponent<MeshRenderer>().material = deactivatedMat;
                    ToggleOff?.Invoke();
                }
                else
                {
                    buttonActivated = true;
                    ButtonCore.GetComponent<MeshRenderer>().material = activatedMat;
                    ToggleOn?.Invoke();
                }
                break;

            default:  break;
        }
    }
}
