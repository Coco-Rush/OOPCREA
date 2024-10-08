using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class MovingPlane : MonoBehaviour
{
    public GameObject plane;
    private GameObject myPlane;
    public float speed;
    public float rotationSpeed;
    private Transform myPropellerTransform;
    
    // Start is called before the first frame update
    void Start()
    {
       myPlane = Instantiate(
           plane,
           Vector3.zero,
           Quaternion.identity
           );

           speed = 100.0f;
           rotationSpeed = 60.0f;   
       
       myPropellerTransform = myPlane.transform.Find("Propeller");
    }

    // Update is called once per frame
    void Update()
    {
        float forwardInput;
        float horizontalInput;
        float forwardMovement;
        float rotationMovement;
        
        forwardInput = Input.GetAxis("Vertical");
        horizontalInput = Input.GetAxis("Horizontal");
        forwardMovement = forwardInput * speed * Time.deltaTime;
        rotationMovement = horizontalInput * rotationSpeed * Time.deltaTime;
        
        myPlane.transform.Translate(0,0,forwardMovement);
        myPlane.transform.Rotate(0,rotationMovement,0);

        myPropellerTransform.Rotate(0, 0, Time.deltaTime * 3600);

    }
}
