using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AnimateCar : MonoBehaviour
{
    private const float WHEEL_CIRCUMFERENCE = 2.2f;

    private int _health;
    private int _repair;
    private string _foundMessage;
    
    public float maxSpeed;
    public float maxSteering;
    public float maxRotationSpeed;
    public string textMessage;

    public GameObject carModel;
    private GameObject _myCarInstance;

    private Transform _rightRearWheelTransform;
    private Transform _leftFrontWheelTransform;
    private Transform _rightFrontWheelTransform;
    private Transform _leftRearWheelTransform;
    
    private float _wheelRotation = 0;
    void Awake()
    {
        _health = 100;
        _repair = 0;
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
    } // End of Awake()
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
    } // End of Update()
    
    void OnCollisionEnter(Collision other)
    {
        Debug.Log(other.gameObject);
        Debug.Log("Collision with " + other.gameObject.name);
        GameObject otherObject = other.gameObject;
        // Material otherObjectMaterial = otherObject.GetComponent<MeshRenderer>().material;
        
        if (otherObject.name == "Sphere")
        {
            // Change Colour
            // otherObjectMaterial.SetColor("_Color", Color.blue);
            
            this.textMessage = "You hit the sphere";
            // StartCoroutine(DestroyText());
        }

        if (!otherObject.transform.parent) return;
        GameObject otherObjectParent = otherObject.transform.parent.gameObject;

        // Exercise SW06 Nr. 5
        if (otherObjectParent.GetComponent<HelperKit>())
        {
            HelperKit foundHelperKit = otherObject.transform.parent.GetComponent<HelperKit>();
            Debug.Log("Found Helper Kit");
            this._foundMessage = "Found the Helper Kit";
            this._health += foundHelperKit.GetHeal();
            this.textMessage = $"Health: {_health} - Heal: {foundHelperKit.GetHeal()} \n {_foundMessage}";
            otherObject.GetComponent<BoxCollider>().enabled = false;
            otherObjectParent.GetComponent<HelperKit>().rotationSpeed = 20;
            otherObjectParent.GetComponent<HelperKit>().StartScaleResizeWhenObjectInstanceCollected();
            StartCoroutine(DestroyText());
            return;
        }

        if (otherObjectParent.GetComponent<Exploder>())
        {
            Exploder foundExploder = otherObject.transform.parent.GetComponent<Exploder>();
            Debug.Log("Found Damager");
            this._health -= foundExploder.GetDamage();
            this.textMessage = $"Health: {_health} - Damage: {foundExploder.GetDamage()}";
            StartCoroutine(DestroyText());
            return;

        }

        // Exercise SW06 Nr. 4

        if (otherObjectParent.GetComponent<Obstacle>())
        {
            Obstacle foundObstacle = otherObject.transform.parent.GetComponent<Obstacle>();
            Debug.Log("Found Obstacle");
            this._foundMessage = "Found the traffic cone";
            this._health -= foundObstacle.damage;
            this.textMessage = $"Health: {_health} - Damage: {foundObstacle.damage} \n {_foundMessage}";
            Debug.Log(this.textMessage);
            StartCoroutine(DestroyText());
        }
    }

    IEnumerator DestroyText()
    {
        yield return new WaitForSeconds(3);
        this.textMessage = "";
    }
}
