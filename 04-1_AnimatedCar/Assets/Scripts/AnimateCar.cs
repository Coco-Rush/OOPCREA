using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimateCar : MonoBehaviour
{
    public float speed = 5.0f;
    public float rotationSpeed = 60.0f;
    public GameObject CarModel;

    private GameObject myCarInstance;
    private Rigidbody myRigidbody;
    private BoxCollider myCarBoxCollider;

    private Transform leftFrontWheelTransform;
    private Transform rightFrontWheelTransform;
    private Transform leftRearWheelTransform;
    private Transform rightRearWheelTransform;

    private Transform leftFrontDoorTransform;
    private Transform leftFrontDoorGlasTransform;
    private Transform rightFrontDoorTransform;
    private Transform rightFrontDoorGlasTransform;

    // Start is called before the first frame update
    void Start()
    {
        myCarInstance = Instantiate(CarModel, Vector3.zero, Quaternion.identity);
        myCarInstance.transform.localScale.Scale(new Vector3(1.1f, 1.1f, 1.1f));
        myCarInstance.transform.Translate(0, 0.42f, 0);

        myRigidbody = myCarInstance.AddComponent<Rigidbody>();

        myCarBoxCollider = myCarInstance.AddComponent<BoxCollider>();
        myCarBoxCollider.size = new Vector3(1.6f, 1.4f, 4.45f);
        myCarBoxCollider.center = new Vector3(0, 0.3f, 0);

        leftFrontWheelTransform = myCarInstance.transform.Find("Tocus_Wheel_Left_Font");
        rightFrontWheelTransform = myCarInstance.transform.Find("Tocus_Wheel_Right_Front");
        leftRearWheelTransform = myCarInstance.transform.Find("Tocus_Wheel_Left_Back");
        rightRearWheelTransform = myCarInstance.transform.Find("Tocus_Wheel_Right_Back");

        leftFrontDoorTransform = myCarInstance.transform.Find("Tocus_Door_Left_Front");
        leftFrontDoorGlasTransform = myCarInstance.transform.Find("Tocus_Window_Door_Left_Front");
        rightFrontDoorTransform = myCarInstance.transform.Find("Tocus_Door_Right_Front");
        rightFrontDoorGlasTransform = myCarInstance.transform.Find("Tocus_Win_Door_Right_Front");
    }

    // Update is called once per frame
    void Update()
    {
        const float wheelCircumference = 2.2f;

        float horizontalInput;
        float forwardInput;

        float forwardMovement;
        float rotationMovement;
        float wheelIncrement;

        horizontalInput = Input.GetAxis("Horizontal");
        forwardInput = Input.GetAxis("Vertical");

        forwardMovement = forwardInput * speed * Time.deltaTime;
        rotationMovement = horizontalInput * rotationSpeed * Time.deltaTime;

        myCarInstance.transform.Translate(0, 0, forwardMovement);
        myCarInstance.transform.Rotate(0, rotationMovement, 0);

        wheelIncrement = forwardMovement * 360 / wheelCircumference;

        leftFrontWheelTransform.Rotate(wheelIncrement, 0, 0);
        rightFrontWheelTransform.Rotate(wheelIncrement, 0, 0);
        leftRearWheelTransform.Rotate(wheelIncrement, 0, 0);
        rightRearWheelTransform.Rotate(wheelIncrement, 0, 0);

        if (Input.GetButton("Fire1"))
        {
            Debug.Log("Fire 1");
            leftFrontDoorTransform.localRotation = Quaternion.Euler(0.0f, 45.0f, 0.0f);
            leftFrontDoorTransform.localPosition = new Vector3(-0.87f, 0.0f, -0.26f);
            leftFrontDoorGlasTransform.localRotation = Quaternion.Euler(0.0f, 45.0f, 0.0f);
            leftFrontDoorGlasTransform.localPosition = new Vector3(-0.87f, 0.0f, -0.26f);
        }
        if (Input.GetButton("Fire2"))
        {
            Debug.Log("Fire 2");
            leftFrontDoorTransform.localRotation = Quaternion.Euler(0.0f, 0.0f, 0.0f);
            leftFrontDoorTransform.localPosition = new Vector3(0.0f, 0.0f, 0.0f);
            leftFrontDoorGlasTransform.localRotation = Quaternion.Euler(0.0f, 0.0f, 0.0f);
            leftFrontDoorGlasTransform.localPosition = new Vector3(0.0f, 0.0f, 0.0f);
        }
        if (Input.GetButton("Fire3"))
        {
            Debug.Log("Fire 3");
            rightFrontDoorTransform.localRotation = Quaternion.Euler(0.0f, -45.0f, 0.0f);
            rightFrontDoorTransform.localPosition = new Vector3(0.87f, 0.0f, -0.26f);
            rightFrontDoorGlasTransform.localRotation = Quaternion.Euler(0.0f, -45.0f, 0.0f);
            rightFrontDoorGlasTransform.localPosition = new Vector3(0.87f, 0.0f, -0.26f);
        }
        if (Input.GetButton("Jump"))
        {
            Debug.Log("Jump");
            rightFrontDoorTransform.localRotation = Quaternion.Euler(0.0f, 0.0f, 0.0f);
            rightFrontDoorTransform.localPosition = new Vector3(0.0f, 0.0f, 0.0f);
            rightFrontDoorGlasTransform.localRotation = Quaternion.Euler(0.0f, 0.0f, 0.0f);
            rightFrontDoorGlasTransform.localPosition = new Vector3(0.0f, 0.0f, 0.0f);
        }
    }
}
