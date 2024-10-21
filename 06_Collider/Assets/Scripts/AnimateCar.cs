using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimateCar : MonoBehaviour
{
    private const float wheelCircumference = 2.2f;
    
    public float maxSpeed;
    public float maxSteering;
    public float maxRotationSpeed;
    public string textMessage;
    
    public GameObject CarModel;
    public GameObject myCarInstance;
    
    private Transform leftFrontWheelTransform;
    private Transform rightFrontWheelTransform;
    private Transform leftRearWheelTransform;
    private Transform rightRearWheelTransform;

    private float wheelRotation = 0;

    // Start is called before the first frame update
    void Awake()
    {
        Debug.Log("Awake called");
        // maxSpeed = 5;
        // maxSteering = 30;
        // maxRotationSpeed = 60;
        
        //  Instatiate the car at zero location & rotation, a little larger than the model
        // myCarInstance = Instantiate(CarModel, Vector3.zero, Quaternion.identity);
        // myCarInstance.transform.parent = this.transform;
        // myCarInstance.transform.localScale = new Vector3(1.1f, 1.1f, 1.1f);

        //  Add rigidbody to this class, not to the car!
        Rigidbody myRigidbody = gameObject.AddComponent<Rigidbody>();

        //  Add collider to this class, not the car!
        BoxCollider myCarBoxCollider = gameObject.AddComponent<BoxCollider>();
        myCarBoxCollider.size = new Vector3(1.6f, 1.4f, 4.45f);
        myCarBoxCollider.center = new Vector3(0, 0.3f, 0);

        //  Create reference instances for the moveable wheels
        leftFrontWheelTransform = transform.Find("Tocus_Wheel_Left_Font");
        rightFrontWheelTransform = transform.Find("Tocus_Wheel_Right_Front");
        leftRearWheelTransform = transform.Find("Tocus_Wheel_Left_Back");
        rightRearWheelTransform = transform.Find("Tocus_Wheel_Right_Back");

        textMessage = "Hello World";

    }   // End of Awake()

    // Update is called once per frame
    void Update()
    {
        float horizontalInput;
        float forwardInput;

        float forwardMovement;
        float rotationMovement;
        float wheelRotIncrement;
        float wheelSteering;

        horizontalInput = Input.GetAxis("Horizontal");
        forwardInput = Input.GetAxis("Vertical");

        forwardMovement = forwardInput * maxSpeed * Time.deltaTime;
        rotationMovement = horizontalInput * maxRotationSpeed * Time.deltaTime;

        //  Apply movement to this class, then copy its position to the car
        transform.Translate(0, 0, forwardMovement);
        transform.Rotate(0, rotationMovement * forwardInput, 0);

        //  Animate wheels in rotation and direction (of front wheels)
        wheelRotIncrement = forwardMovement * 360 / wheelCircumference;
        wheelRotation += wheelRotIncrement;
        wheelSteering = horizontalInput * maxSteering;

        leftFrontWheelTransform.localRotation = Quaternion.Euler(wheelRotation, wheelSteering, 0);
        rightFrontWheelTransform.localRotation = Quaternion.Euler(wheelRotation, wheelSteering, 0);
        leftRearWheelTransform.Rotate(wheelRotIncrement, 0, 0);
        rightRearWheelTransform.Rotate(wheelRotIncrement, 0, 0);

    }   // End of Update()
    
    void OnCollisionEnter(Collision other)
    {
        Debug.Log("Collision with " + other.gameObject.name);
    }
}
