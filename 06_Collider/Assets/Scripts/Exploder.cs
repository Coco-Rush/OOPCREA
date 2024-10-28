using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Exploder : Damager
{
    private MeshCollider _obstacleMeshCollider;
    public GameObject fireType;
    // Start is called before the first frame update
    // private new Collider obstacleBoxCollider;
    protected new void Start()
    {
        base.Start();
        Destroy(obstacleInstance.GetComponent<MeshCollider>());
        obstacleBoxCollider = obstacleInstance.AddComponent<BoxCollider>();
        obstacleBoxCollider.center = new Vector3(0, 0.5f, 0);
        obstacleBoxCollider.size = new Vector3(0.75f, 1f, 0.75f);
        // _obstacleMeshCollider = obstacleInstance.GetComponent<MeshCollider>();
        // _obstacleMeshCollider.transform.
        // _obstacleMeshCollider.transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
        // _obstacleMeshCollider.
        
        // deactivate mesh collider of the oil barrel
        Debug.Log(_obstacleMeshCollider);
        // obstacleInstance.GetComponent<MeshCollider>().enabled = false;
        
        // deactivate /destroy the rigidbody of the oil barrel
        // Destroy(obstacleInstance.GetComponent<Rigidbody>());
        Debug.Log("this is the car instance inside exploder: " + carInstanceReference);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Rair");
        GameObject otherObj = collision.gameObject;
        Debug.Log("Collision detected with " + otherObj.name);
        if (otherObj == carInstanceReference)
        {
            GameObject fireInstance = Instantiate(fireType, gameObject.transform.localPosition, Quaternion.identity);
            fireInstance.transform.parent = transform;
            
        }
    }
}
