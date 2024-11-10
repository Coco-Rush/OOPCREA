using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Actor : MonoBehaviour
{
    private GameObject _simpleGameObjectSphere;
    private GameObject _instantiatedGameObject;
    private SphereCollider _sphereCollider;
    private Rigidbody _rigidbody;
    
    private float _verticalInput;
    private float _horizontalInput;
    private float _verticalMovement;
    private float _horizontalMovement;

    public float speed;
    // Start is called before the first frame update
    void Start()
    {
        _simpleGameObjectSphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        _instantiatedGameObject = Instantiate(_simpleGameObjectSphere, Vector3.zero, Quaternion.identity);
        Destroy(_simpleGameObjectSphere);
        _instantiatedGameObject.transform.SetParent(gameObject.transform);
        
        _instantiatedGameObject.transform.localPosition = Vector3.zero;
        _instantiatedGameObject.transform.localScale = Vector3.one * 0.05f;
        _instantiatedGameObject.transform.localRotation = Quaternion.identity;
        
        _sphereCollider = _instantiatedGameObject.GetComponent<SphereCollider>();
        _rigidbody = _instantiatedGameObject.AddComponent<Rigidbody>();
        _rigidbody.mass = 0.1f;
    }

    // Update is called once per frame
    void Update()
    {
        _verticalInput = Input.GetAxis("Vertical");
        _horizontalInput = Input.GetAxis("Horizontal");
        _verticalMovement = _verticalInput * Time.deltaTime * speed;
        _horizontalMovement = _horizontalInput * Time.deltaTime * speed;
        
        gameObject.transform.Translate(_horizontalMovement, 0, _verticalMovement);
    }
    
    private void OnCollisionEnter(Collision other)
    {
        Debug.Log("Collision with " + other.gameObject.name);
    }
}
