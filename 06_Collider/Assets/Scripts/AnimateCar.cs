using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AnimateCar : MonoBehaviour, IHitAction
{
    private const float WHEEL_CIRCUMFERENCE = 2.2f;

    private int _health;
    private int _lastHeal;
    private string _foundMessage;
    private bool _isJumping;
    private float _forwardMovement;
    private float _rotationMovement;
    private float _wheelRotIncrement;
    private float _wheelSteering;
    
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

    private Rigidbody _myRigidbody;
    private BoxCollider _myCarBoxCollider;
    
    private float _wheelRotation = 0;
    void Awake()
    {
        _health = 100;
        this.textMessage = "";
        this._foundMessage = "";
        _isJumping = false;
        
        Debug.Log("Awake called");
        // maxSpeed = 5;
        // maxSteering = 30;
        // maxRotationSpeed = 60;
        
        //  Instatiate the car at zero location & rotation, a little larger than the model
        // myCarInstance = Instantiate(carModel, Vector3.zero, Quaternion.identity);
        // myCarInstance.transform.parent = this.transform;
        // myCarInstance.transform.localScale = new Vector3(1.1f, 1.1f, 1.1f);

        //  Add rigidbody to this class, not to the car!
        _myRigidbody = gameObject.AddComponent<Rigidbody>();

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
        float jumpInput;

        horizontalInput = Input.GetAxis("Horizontal");
        forwardInput = Input.GetAxis("Vertical");
        jumpInput = Input.GetAxis("Jump");

        if (jumpInput != 0 && !_isJumping)
        {
            Jump();
        }

        _forwardMovement = forwardInput * maxSpeed * Time.deltaTime;
        _rotationMovement = horizontalInput * maxRotationSpeed * Time.deltaTime;

        //  Apply movement to this class, then copy its position to the car
        transform.Translate(0, 0, _forwardMovement);
        transform.Rotate(0, _rotationMovement * forwardInput, 0);

        //  Animate wheels in rotation and direction (of front wheels)
        _wheelRotIncrement = _forwardMovement * 360 / WHEEL_CIRCUMFERENCE;
        _wheelRotation += _wheelRotIncrement;
        _wheelSteering = horizontalInput * maxSteering;
        
        _leftFrontWheelTransform.localRotation = Quaternion.Euler(_wheelRotation, _wheelSteering, 0);
        _rightFrontWheelTransform.localRotation = Quaternion.Euler(_wheelRotation, _wheelSteering, 0);
        _leftRearWheelTransform.Rotate(_wheelRotIncrement, 0, 0);
        _rightRearWheelTransform.Rotate(_wheelRotIncrement, 0, 0);
    } // End of Update()

    IEnumerator DestroyText()
    {
        yield return new WaitForSeconds(3);
        this.textMessage = "";
    }

    void OnCollisionEnter(Collision other)
    {
        // Debug.Log(other.gameObject);
        // Debug.Log("Collision with " + other.gameObject.name);
        GameObject otherObject = other.gameObject;
        // Material otherObjectMaterial = otherObject.GetComponent<MeshRenderer>().material;
        Impact(this._forwardMovement);
        
        if (otherObject.name == "Sphere")
        {
            // Change Colour
            // otherObjectMaterial.SetColor("_Color", Color.blue);
            
            this.textMessage = "You hit the sphere";
            // StartCoroutine(DestroyText());
        }

        if (!otherObject.transform.parent) return;
        GameObject otherObjectParent = otherObject.transform.parent.gameObject;
        if (!otherObjectParent.TryGetComponent<IHitAction>(out IHitAction hitAction))
        {
            return;
        }
        
        // HelperKit
        hitAction.Impact();
        this._foundMessage = "Found the " + hitAction.GetType().Name;
        this.textMessage = $"Health: {_health} - Value: {_lastHeal} \n {_foundMessage}";
        StartCoroutine(DestroyText());
        Debug.Log("Found " + hitAction.GetType().Name);

        // Exercise SW06 Nr. 5
        /*if (otherObjectParent.GetComponent<HelperKit>())
        {
            // Call Impact function of HelperKit with Interface
            HelperKit foundHelperKit = otherObject.transform.parent.GetComponent<HelperKit>();
            
            this._foundMessage = "Found the Helper Kit";
            this._health += foundHelperKit.GetHeal();
            this.textMessage = $"Health: {_health} - Heal: {foundHelperKit.GetHeal()} \n {_foundMessage}";
            
            otherObject.GetComponent<BoxCollider>().enabled = false;
            otherObjectParent.GetComponent<HelperKit>().rotationSpeed = 20;
            otherObjectParent.GetComponent<HelperKit>().StartScaleResizeWhenObjectInstanceCollected();
            StartCoroutine(DestroyText());
            
            // Debug Messages
            Debug.Log("Found Helper Kit");
            return;
        }*/

        /*if (otherObjectParent.GetComponent<Exploder>())
        {
            // Call Impact function of HelperKit with Interface
            
            Exploder foundExploder = otherObject.transform.parent.GetComponent<Exploder>();
            
            this._health -= foundExploder.GetDamage();
            this.textMessage = $"Health: {_health} - Damage: {foundExploder.GetDamage()}";
            
            StartCoroutine(DestroyText());
            
            // Debug Messages
            Debug.Log("Found Exploder");
            return;

        }*/

        // Exercise SW06 Nr. 4

        /*if (otherObjectParent.GetComponent<Obstacle>())
        {
            Obstacle foundObstacle = otherObject.transform.parent.GetComponent<Obstacle>();
            
            this._foundMessage = "Found the traffic cone";
            this._health -= foundObstacle.damage;
            this.textMessage = $"Health: {_health} - Damage: {foundObstacle.damage} \n {_foundMessage}";
            
            StartCoroutine(DestroyText());

            // Debug Messages
            Debug.Log("Found Obstacle");
            Debug.Log(this.textMessage);
        }*/
    }

    // After Pressing SpaceBar the Car should Jump exactly 1 meter high. 
    public void Jump()
    {
        _isJumping = true;
        _myRigidbody.AddForce(Vector3.up * 8, ForceMode.Impulse);
        StartCoroutine(StopJump());
    }
    public void UpdateHealth(int health)
    {
        this._lastHeal = health;
        this._health += health;
    }

    public void Impact()
    {
        
    }

    public void Impact(int collisionSpeed)
    {
        
    }

    public void Impact(float collisionSpeed)
    {
        
    }
    
    IEnumerator StopJump()
    {
        yield return new WaitForSeconds(2);
        _isJumping = false;
    }
}
