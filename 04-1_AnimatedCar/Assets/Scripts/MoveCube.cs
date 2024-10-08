using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCube : MonoBehaviour
{
    public float speed = 5.0f;
    public float rotationSpeed = 60.0f;

    private float horizontalInput;
    private float forwardInput;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        forwardInput = Input.GetAxis("Vertical");

        forwardInput = forwardInput * speed * Time.deltaTime;
        horizontalInput = horizontalInput * rotationSpeed * Time.deltaTime;

        transform.Translate(Vector3.forward * forwardInput);
        transform.Rotate(0, horizontalInput, 0);


    }
}
