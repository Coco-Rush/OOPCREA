using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimateCar : MonoBehaviour
{
    public float speed;
    public float rotationSpeed;
    public GameObject CarModel;

    private GameObject myCarInstance;
    private Transform leftFrontWheelTransform;
    private Transform rightFrontWheelTransform;
    private Transform leftRearWheelTransform;
    private Transform rightRearWheelTransform;
    private Rigidbody myCarRigidBody;
    private BoxCollider myCarBoxCollider;

    // Start is called before the first frame update
    void Start()
    {
        speed = 5.0f;
        rotationSpeed = 60.0f;
        
        myCarInstance = Instantiate(CarModel, new Vector3(0,1.0f,0), Quaternion.identity);
        myCarInstance.transform.localScale = new Vector3(1.1f, 1.1f, 1.1f);
        myCarInstance.transform.Translate(0, 0.42f, 0);
        
        myCarRigidBody = myCarInstance.AddComponent<Rigidbody>();
        myCarRigidBody.useGravity = true;
        
        myCarBoxCollider = myCarInstance.AddComponent<BoxCollider>();
        myCarBoxCollider.size = new Vector3(1.9f, 1.35f, 4.5f);
        myCarBoxCollider.center = new Vector3(0, 0.32f, 0);

        leftFrontWheelTransform = myCarInstance.transform.Find("Tocus_Wheel_Left_Font");
        rightFrontWheelTransform = myCarInstance.transform.Find("Tocus_Wheel_Right_Front");
        leftRearWheelTransform = myCarInstance.transform.Find("Tocus_Wheel_Left_Back");
        rightRearWheelTransform = myCarInstance.transform.Find("Tocus_Wheel_Right_Back");
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

        // myCarInstance.transform.Translate(Vector3.forward * forwardMovement);
        myCarInstance.transform.Translate(0, 0, forwardMovement);
        myCarInstance.transform.Rotate(0, rotationMovement, 0);

        wheelIncrement = forwardMovement * 360 / wheelCircumference;
        Debug.Log($"ForwardInput = {forwardInput}, WheelPosition = {wheelIncrement}");

        leftFrontWheelTransform.Rotate(wheelIncrement, 0, 0);
        rightFrontWheelTransform.Rotate(wheelIncrement, 0, 0);
        leftRearWheelTransform.Rotate(wheelIncrement, 0, 0);
        rightRearWheelTransform.Rotate(wheelIncrement, 0, 0);
    }
}
