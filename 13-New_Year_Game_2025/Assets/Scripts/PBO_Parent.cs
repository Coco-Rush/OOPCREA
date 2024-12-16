using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PBO_Parent : MonoBehaviour
{
    //  Abstract methodsab, that every child class needs to implement
    public abstract void SetNameAndScore();
    public abstract Vector3 GetSpin();
    public abstract void CatchEffect();

    //  Protected variables
    protected SNamesAndScores nameAndScore; //  needs to be set by child class

    //  Drag control
    public float maxDrag = 1.0f;
    public bool spawnedObject = false;
    protected Rigidbody myRigidbody;
    protected float drag = 0.0f;
    private bool caughtOnce = false;

    //  On catch, update the values on the scoreboard
    public void OnCatch()
    {
        if (!caughtOnce)
        {
            ScoreDisplay myScoreDisplay = GameObject.FindAnyObjectByType<ScoreDisplay>();
            myScoreDisplay.UpdateScore(nameAndScore);
            CatchEffect();
            caughtOnce = true;
        }
    }


    //  Start method - needs to be called by child classes with "base.Start()"
    public void Start()
    {        
        myRigidbody = gameObject.GetComponentInParent<Rigidbody>();  //  Establishing reference for the drag update
        SetNameAndScore();      //  Initialize object name and score contribution
    }

    //  Update method - needs to be called by child classes with "base.Update()"
    public void Update()
    {
        if (spawnedObject)
        {
            if (drag < maxDrag)
            {
                drag += 0.1f;
                myRigidbody.drag = drag;
            }
        }
    }

}
