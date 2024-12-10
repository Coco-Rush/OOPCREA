using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerObject : MonoBehaviour
{
    private SphereCollider _sphereCollider;
    private Rigidbody _rigidbody;
    private Material _material;
    // Start is called before the first frame update
    void Start()
    {
        _sphereCollider = gameObject.GetComponent<SphereCollider>();
        _rigidbody = gameObject.GetComponent<Rigidbody>();
        _material = gameObject.GetComponent<Material>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision other)
    {
        Debug.Log("Collision with " + other.gameObject.name);
    }
}
