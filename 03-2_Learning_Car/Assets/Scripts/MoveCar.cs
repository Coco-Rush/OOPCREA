using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCar : MonoBehaviour
{
    // Start is called before the first frame update
    public float speed = 5.0f;
    public float rotationSpeed = 60.0f;
    public float horizontalInput;
    public float forwardInput;

    void Start()
    {
        Debug.Log("Car is moving");
    }

    // Update is called once per frame
    void Update()
    {
        //Get Player Input
        horizontalInput = Input.GetAxis("Horizontal");
        forwardInput = Input.GetAxis("Vertical");
        
        //Move Car
        transform.Translate(Vector3.forward * (speed * Time.deltaTime * forwardInput));
        transform.Rotate(0, rotationSpeed * speed * Time.deltaTime * horizontalInput, 0);
    }
}