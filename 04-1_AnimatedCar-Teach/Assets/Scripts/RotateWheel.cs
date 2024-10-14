using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateWheel : MonoBehaviour
{
    public float speed = 5.0f;
    public float rotationSpeed = 60.0f;

    static float wheelCircumference = 2.20f;    // 18" tire with 119mm tire rim
    private float wheelSpeed;
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

        wheelSpeed = 360 * speed / wheelCircumference;
        forwardInput = forwardInput * wheelSpeed * Time.deltaTime;
        horizontalInput = horizontalInput * rotationSpeed * Time.deltaTime;

        // transform.Translate(Vector3.forward * forwardInput);
        transform.Rotate(forwardInput, 0, 0);

    }
}
