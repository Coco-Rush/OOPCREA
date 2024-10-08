using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadarCarAnimation : MonoBehaviour
{
    public float speed = 5.0f;
    public float rotationSpeed = 60.0f;

    public GameObject radarCar;
    private GameObject radarCarInstance;

    private Transform radarDishTransform;

    // Start is called before the first frame update
    void Start()
    {
        radarCarInstance = Instantiate( // call the instantiation method
            radarCar,                   // for the class stored in "radarCar"
            Vector3.zero,               // at the position zero (as X=0, Y=0, Z=0)
            Quaternion.identity         // and rotation (quaternion) zero
        );

        radarDishTransform = radarCarInstance.transform.Find("Radar");

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

        radarCarInstance.transform.Translate(0, 0, forwardMovement);
        radarCarInstance.transform.Rotate(0, rotationMovement, 0);

        radarDishTransform.Rotate(0, Time.deltaTime * 180, 0);
    }
}

