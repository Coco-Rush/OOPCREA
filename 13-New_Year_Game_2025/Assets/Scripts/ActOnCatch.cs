using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Oculus.Interaction;
using Oculus.Interaction.HandGrab;

public class ActOnCatch : MonoBehaviour
{
    private HandGrabInteractable myGrabInteract;

    private void OnEnable()
    {
        myGrabInteract = gameObject.GetComponent<HandGrabInteractable>();
        myGrabInteract.WhenPointerEventRaised += OnPointerEvent;
    }

    private void OnDisable()
    {
        myGrabInteract.WhenPointerEventRaised -= OnPointerEvent;
    }


    void OnPointerEvent(PointerEvent args)
    {
        if (args.Type == PointerEventType.Select)
        {
            myPBO_Parent = gameObject.GetComponentInChildren<PBO_Parent>();
            myPBO_Parent.OnCatch();
        }
    }

    //  The sphere and box colliders can be on the children, yet the "OnCollisionEnter" event
    //  fires on the level where the Rigidbody component is - which is on this level with the
    //  Handgrabbable scripts. So the collision handler needs to be on this level as well.
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.name == "InvisibleGround")
        {
            Destroy(gameObject);
        }
    }
}
