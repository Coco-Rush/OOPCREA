using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AnimateCar : MonoBehaviour
{
    private const float WHEEL_CIRCUMFERENCE = 2.2f;
    
    public float maxSpeed;
    public float maxSteering;
    public float maxRotationSpeed;
    private int _health;
    private int _damage;
    public string textMessage;
    private string _foundMessage;
    
    public GameObject carModel;
    public GameObject myCarInstance;
    
    private Transform _leftFrontWheelTransform;
    private Transform _rightFrontWheelTransform;
    private Transform _leftRearWheelTransform;
    private Transform _rightRearWheelTransform;

    private float _wheelRotation = 0;

    // Start is called before the first frame update
    void Awake()
    {
        _health = 100;
        _damage = 0;
        this.textMessage = "";
        this._foundMessage = "";
        
        Debug.Log("Awake called");
        // maxSpeed = 5;
        // maxSteering = 30;
        // maxRotationSpeed = 60;
        
        //  Instatiate the car at zero location & rotation, a little larger than the model
        // myCarInstance = Instantiate(carModel, Vector3.zero, Quaternion.identity);
        // myCarInstance.transform.parent = this.transform;
        // myCarInstance.transform.localScale = new Vector3(1.1f, 1.1f, 1.1f);

        //  Add rigidbody to this class, not to the car!
        Rigidbody myRigidbody = gameObject.AddComponent<Rigidbody>();

        //  Add collider to this class, not the car!
        BoxCollider myCarBoxCollider = gameObject.AddComponent<BoxCollider>();
        myCarBoxCollider.size = new Vector3(1.6f, 1.4f, 4.45f);
        myCarBoxCollider.center = new Vector3(0, 0.3f, 0);

        //  Create reference instances for the moveable wheels
        _leftFrontWheelTransform = transform.Find("Tocus_Wheel_Left_Font");
        _rightFrontWheelTransform = transform.Find("Tocus_Wheel_Right_Front");
        _leftRearWheelTransform = transform.Find("Tocus_Wheel_Left_Back");
        _rightRearWheelTransform = transform.Find("Tocus_Wheel_Right_Back");
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
        wheelRotIncrement = forwardMovement * 360 / WHEEL_CIRCUMFERENCE;
        _wheelRotation += wheelRotIncrement;
        wheelSteering = horizontalInput * maxSteering;

        _leftFrontWheelTransform.localRotation = Quaternion.Euler(_wheelRotation, wheelSteering, 0);
        _rightFrontWheelTransform.localRotation = Quaternion.Euler(_wheelRotation, wheelSteering, 0);
        _leftRearWheelTransform.Rotate(wheelRotIncrement, 0, 0);
        _rightRearWheelTransform.Rotate(wheelRotIncrement, 0, 0);

    }   // End of Update()
    
    void OnCollisionEnter(Collision other)
    {
        Debug.Log("Collision with " + other.gameObject.name);
        GameObject otherObject = other.gameObject;
        Material otherObjectMaterial = otherObject.GetComponent<MeshRenderer>().material;
        
        if (otherObject.name == "Sphere")
        {
            // Change Colour
            otherObjectMaterial.SetColor("_Color", Color.blue);
            
            this.textMessage = "You hit the sphere";
            StartCoroutine(DestroyText());
        }
        
        // Exercise SW06 Nr. 4
        Obstacle foundObstacle = otherObject.GetComponent<Obstacle>();
        if (foundObstacle)
        {
            Debug.Log("Found Obstacle");
            _foundMessage = "Found Obstacle - it's the traffic cone";
            
        }
        
    }

    IEnumerator DestroyText()
    {
        yield return new WaitForSeconds(3);
        this.textMessage = "";
    }
}
