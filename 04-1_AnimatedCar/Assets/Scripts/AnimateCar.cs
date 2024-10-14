using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AnimateCar : MonoBehaviour
{
    public float currentSpeed;
    public float currentDirection;
    public float speed;
    public float rotationSpeed;
    
    private GameObject myCarInstance;
    public GameObject CarModel;
    public GameObject ArrowModel;
    
    public TextMeshPro textMesh;
    private TMP_Text debugTextMesh;
    
    private Rigidbody myCarRigidBody;
    private BoxCollider myCarBoxCollider;
    
    private Transform leftFrontWheelTransform;
    private Transform rightFrontWheelTransform;
    private Transform leftRearWheelTransform;
    private Transform rightRearWheelTransform;

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

        debugTextMesh = textMesh.GetComponent<TMP_Text>();
        debugTextMesh.text = "Rair";
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
        
        currentSpeed = forwardInput * speed;
        currentDirection = horizontalInput * rotationSpeed;
        forwardMovement = currentSpeed * Time.deltaTime;
        rotationMovement = currentDirection * Time.deltaTime;

        // myCarInstance.transform.Translate(Vector3.forward * forwardMovement);
        myCarInstance.transform.Translate(0, 0, forwardMovement);
        myCarInstance.transform.Rotate(0, rotationMovement, 0);

        wheelIncrement = forwardMovement * 360 / wheelCircumference;
        Debug.Log($"ForwardInput = {forwardInput}, WheelPosition = {wheelIncrement}");

        leftFrontWheelTransform.Rotate(wheelIncrement, 0, 0);
        rightFrontWheelTransform.Rotate(wheelIncrement, 0, 0);
        leftRearWheelTransform.Rotate(wheelIncrement, 0, 0);
        rightRearWheelTransform.Rotate(wheelIncrement, 0, 0);
        
        ArrowModel.transform.rotation = Quaternion.Euler(0.0f, 0.0f, currentDirection * -1);
        
        debugTextMesh.text = "speed: " + currentSpeed;
    }
}
