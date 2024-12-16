using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartyBomb : MonoBehaviour
{
    //public AudioSource audioFireCrackling;
    //public AudioSource audioWhoosh;
    public GameObject myGrabbableTop;
    public GameObject myLid;

    private GameObject[] PartyBombObjects;
    private ActOnButton myStartButton;

    private bool bombActive = false;
    private int launchCount = 0;
    private int delayCounter  = 0;

    private ScoreDisplay myScoreDisplay;
    private AudioSource myCracklingFuseAudio;
    private AudioClip myCracklingFuseClip;
    private AudioSource myAirWooshAudio;
    private AudioClip myAirWooshClip;

    // Start is called before the first frame update
    void OnEnable()
    {
        //  Load array with all spawnable party bomb objects
        PartyBombObjects = GameObject.FindGameObjectsWithTag("PartyBombObject");

        //  Establish connection to start button
        myStartButton = FindAnyObjectByType<ActOnButton>();
        if (myStartButton != null)
        {
            myStartButton.ButtonPressed += StartButtonPressed;
        }

        //  Get display reference
        myScoreDisplay = GameObject.FindAnyObjectByType<ScoreDisplay>();

        //  Disable Party Bomb capsule collider to avoid skewing ejected objects
        gameObject.GetComponent<CapsuleCollider>().enabled = false;             //  No collision with Party Bomb body
        gameObject.GetComponentInChildren<SphereCollider>().enabled = false;    //  No collision with Party Bomb lid

        myCracklingFuseAudio = gameObject.AddComponent<AudioSource>() as AudioSource;
        myCracklingFuseClip = Resources.Load<AudioClip>("Mixkit-CampfireCrackles");
        myAirWooshAudio = gameObject.AddComponent<AudioSource>() as AudioSource;
        myAirWooshClip = Resources.Load<AudioClip>("Mixkit-AirWoosh");
    }

    private void OnDisable()
    {
        if (myStartButton != null)
        {
            myStartButton.ButtonPressed -= StartButtonPressed;
        }
    }

    private void StartButtonPressed()
    {
        if (! bombActive)
        {
            StartBomb();
        }
        else
        {
            StopBomb();
        }
    }


    void StartBomb()
    {
        bombActive = true;

        //  Start background sound
        myCracklingFuseAudio.PlayOneShot(myCracklingFuseClip);
        myCracklingFuseAudio.loop = true;

        //  Reset score
        launchCount = 0;
        myScoreDisplay.ResetScores();

        //  Replace red lid with black hole
        myLid.GetComponent<MeshRenderer>().material = Resources.Load("BlackMat") as Material;
        myLid.GetComponent<Transform>().localScale = new Vector3(0.9f, 0.01f, 0.9f);
    }

    private void StopBomb()
    {
        bombActive = false;                 //  Stop launches
        myCracklingFuseAudio.Stop();        //  Stop background sound

        //  Restore red lid
        myLid.GetComponent<MeshRenderer>().material = Resources.Load("RedMat") as Material;
        myLid.GetComponent<Transform>().localScale = new Vector3(1.066f, 0.15f, 1.066f);
    }


    // Update is called once per frame
    void Update()
    {
        if (bombActive == true)
        {
            if (launchCount == 100)
            {
                StopBomb();
            }
            else
            {
                delayCounter--;
                if (delayCounter <= 0)
                {

                    System.Random randomNumber = new System.Random();   //  Initialize C# random number generator
                    delayCounter = randomNumber.Next(20, 101);      //  Set next launch delay
                    launchCount++;

                    LaunchPartyBombObject();                        //  Launch object
                    myAirWooshAudio.PlayOneShot(myAirWooshClip);    //  ... with sound
                    myScoreDisplay.AddLaunches(1);                  //  ... and update display
                }
            }
        }
    }

    private void LaunchPartyBombObject()
    {        
        System.Random randomNumber = new System.Random();   //  Initialize C# random number generator

        Vector3 launchPosition = gameObject.transform.position + new Vector3(0, 0.4f, 0);
        GameObject New_PBO_Top = Instantiate(
            myGrabbableTop,                                 // What
            launchPosition,                                 // Where
            gameObject.transform.rotation                   // Rotation
        );
        New_PBO_Top.AddComponent<ActOnCatch>();

        int randomIndex = randomNumber.Next(0, PartyBombObjects.Length);
        GameObject New_PBO = Instantiate(
            PartyBombObjects[randomIndex],                  // What
            gameObject.transform.position,                  // Where
            gameObject.transform.rotation                   // Rotation
        );

        New_PBO.transform.parent = New_PBO_Top.transform;

        Rigidbody myRigidbody = New_PBO_Top.GetComponent<Rigidbody>();
        myRigidbody.useGravity = true;

        myRigidbody.AddForce(
            randomNumber.Next(-100, 100),  // X
            randomNumber.Next(400, 600),   // Y
            randomNumber.Next(-200, 0)     // Z
        );

        myRigidbody.AddTorque(New_PBO.GetComponent<PBO_Parent>().GetSpin());

        PBO_Parent myPBO_Object = New_PBO.GetComponent<PBO_Parent>();
        myPBO_Object.spawnedObject = true;
        myPBO_Object.maxDrag = randomNumber.Next(5, 20);
    }
}
