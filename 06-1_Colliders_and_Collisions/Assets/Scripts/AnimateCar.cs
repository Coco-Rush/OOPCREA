using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimateCar : MonoBehaviour
{
    public GameObject CarModel;
    public GameObject myCarInstance;
    public string textMessage;

    public float maxSpeed = 5;
    public float maxSteering = 30;
    public float maxRotationSpeed = 60;

    private Transform leftFrontWheelTransform;
    private Transform rightFrontWheelTransform;
    private Transform leftRearWheelTransform;
    private Transform rightRearWheelTransform;

    private float wheelRotation = 0;

    // Start is called before the first frame update
    void Awake()
    {
        //  Instatiate the car at zero location & rotation, a little larger than the model
        myCarInstance = Instantiate(CarModel, Vector3.zero, Quaternion.identity);
        myCarInstance.transform.parent = this.transform;
        myCarInstance.transform.localScale = new Vector3(1.1f, 1.1f, 1.1f);

        //  Add rigidbody to this class, not to the car!
        Rigidbody myRigidbody = gameObject.AddComponent<Rigidbody>();

        //  Add collider to this class, not the car!
        BoxCollider myCarBoxCollider = gameObject.AddComponent<BoxCollider>();
        myCarBoxCollider.size = new Vector3(1.6f, 1.4f, 4.45f);
        myCarBoxCollider.center = new Vector3(0, 0.3f, 0);

        //  Create reference instances for the moveable wheels
        leftFrontWheelTransform = myCarInstance.transform.Find("Tocus_Wheel_Left_Font");
        rightFrontWheelTransform = myCarInstance.transform.Find("Tocus_Wheel_Right_Front");
        leftRearWheelTransform = myCarInstance.transform.Find("Tocus_Wheel_Left_Back");
        rightRearWheelTransform = myCarInstance.transform.Find("Tocus_Wheel_Right_Back");

        textMessage = "Hello World";

    }   // End of Awake()


    // Update is called once per frame
    void Update()
    {
        const float wheelCircumference = 2.2f;

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
        textMessage = "Collision with" + other.gameObject.name;
        StartCoroutine(ClearTextMessage());
    }

    IEnumerator ClearTextMessage()
    {
        yield return new WaitForSeconds(2);
        textMessage = "";
    }

}
