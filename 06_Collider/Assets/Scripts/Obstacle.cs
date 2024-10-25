using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    public GameObject obstacleModel;

    protected GameObject obstacleInstance;
    protected Rigidbody obstacleRigidbody;
    protected BoxCollider obstacleBoxCollider;
    
    // Start is called before the first frame update
    protected void Start()
    {
        // this.transform stands for the transform of the GameObject this script is attached to
        
        // obstacleInstance.transform.parent stands for the transform of the parent of the obstacleInstance GameObject
        // At the beginning it is "empty" (the scene is the parent) but after applying a GameObject that will become the
        // parent of the obstacleInstance GameObject
        
        Debug.Log("Parents Local Position: " + this.transform.localPosition);
        obstacleInstance = Instantiate(this.obstacleModel, this.transform.localPosition, Quaternion.identity);
        obstacleInstance.transform.parent = this.transform;
        
        Debug.Log("Obstacle Local Position: " + this.obstacleInstance.transform.localPosition);
        Debug.Log("Obstacle Position: " + this.obstacleInstance.transform.position);
        
        // Basically like "Human Tom = new Human();"
        obstacleRigidbody = this.gameObject.AddComponent<Rigidbody>();
        obstacleBoxCollider = this.gameObject.AddComponent<BoxCollider>();
        
        // When a Box Collider is instantiated, the center is always at the origin (0, 0, 0) and the size is (1, 1, 1)
        
        obstacleBoxCollider.size = new Vector3(0.5f, 0.5f, 0.5f);
        obstacleBoxCollider.center = new Vector3(0, 0.5f, 0);

    }
}
