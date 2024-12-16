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

    public event Action ButtonPressed;
    public event Action ButtonReleased;

    private PokeInteractable myPokeInteract;

    private void OnEnable()
    {
        //  Get a reference to the Poke interactable
        myPokeInteract = gameObject.GetComponent<PokeInteractable>();
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
                ButtonPressed?.Invoke();
                ButtonCore.GetComponent<MeshRenderer>().material = activatedMat;
                break;

            case PointerEventType.Unselect:
                ButtonReleased?.Invoke();
                ButtonCore.GetComponent<MeshRenderer>().material = deactivatedMat;
                break;

            default:  break;
        }
    }


    //  Keyboard triggering for testing
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ButtonPressed?.Invoke();
        }
    }
}
