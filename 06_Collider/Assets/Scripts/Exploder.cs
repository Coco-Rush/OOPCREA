using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Exploder : Damager
{
    public GameObject fireType;
    // Start is called before the first frame update
    // private new Collider obstacleBoxCollider;
    protected new void Start()
    {
        base.Start();
        //obstacleBoxCollider = obstacleInstance.GetComponent<MeshCollider>();
        obstacleBoxCollider.size = new Vector3(0.5f, 1f, 0.5f);
        
        // deactivate mesh collider of the oil barrel
        obstacleInstance.GetComponent<Collider>().enabled = false;
        
        // deactivate /destroy the rigidbody of the oil barrel
        Destroy(obstacleInstance.GetComponent<Rigidbody>());

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    void OnCollisionEnter(Collision collision)
    {
        GameObject otherObj = collision.gameObject;
        if (otherObj.GetComponent<AnimateCar>())
        {
            GameObject fireInstance = Instantiate(fireType, gameObject.transform.localPosition, Quaternion.identity);
            fireInstance.transform.parent = transform;
            
        }
    }
}
