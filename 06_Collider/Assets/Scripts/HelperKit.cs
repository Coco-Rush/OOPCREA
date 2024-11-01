using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

public class HelperKit : Obstacle, IHitAction
{
    public int normalHeal;
    public float rotationSpeed;
    private int _largeHeal;
    private int _smallHeal;
    public float resizeScaleX;
    public float resizeScaleY;
    public float resizeScaleZ;
    private float _scaleIncrementX;
    private float _scaleIncrementY;
    private float _scaleIncrementZ;
    private bool _isResizing = false;
    private bool _ihitable;


    // Start is called before the first frame update

    protected new void Start()
    {
        base.Start();
        obstacleInstance.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
        // obstacleBoxCollider.enabled = false;
        
        obstacleRigidbody.useGravity = false;
        _ihitable = _carInstanceReference.TryGetComponent<IHitAction>(out _);
        Debug.Log("Is IHitAction: " + _ihitable);
    }

    private void Update()
    {
        if (!obstacleInstance.IsDestroyed())
        {
            transform.Rotate(0f, rotationSpeed, 0f);
        }

        if (_isResizing && !obstacleInstance.IsDestroyed())
        {
            obstacleInstance.transform.localScale = 
                new Vector3(
                obstacleInstance.transform.localScale.x + _scaleIncrementX,
                obstacleInstance.transform.localScale.y + _scaleIncrementY,
                obstacleInstance.transform.localScale.z + _scaleIncrementZ);
            
            this.transform.position = 
                new Vector3(
                _carInstanceReference.transform.position.x, 
                _carInstanceReference.transform.position.y + 2.75f, 
                _carInstanceReference.transform.position.z);
        }
    }
    public new void Impact()
    {
        obstacleInstance.GetComponent<BoxCollider>().enabled = false;
        rotationSpeed = 20;
        StartScaleResizeWhenObjectInstanceCollected();
        
        _carInstanceReference.GetComponent<AnimateCar>().UpdateHealth(GetHeal());
    }
    public new void Impact(int collisionSpeed)
    {
        
    }
    public new void Impact(float collisionSpeed)
    {
        
    }

    protected int GetHeal()
    {
        this._smallHeal = normalHeal / 2;
        return _smallHeal;
    }

    private void StartScaleResizeWhenObjectInstanceCollected()
    {
        _scaleIncrementX = (resizeScaleX - obstacleInstance.transform.localScale.x) / 300;
        _scaleIncrementY = (resizeScaleY - obstacleInstance.transform.localScale.y) / 300;
        _scaleIncrementZ = (resizeScaleZ - obstacleInstance.transform.localScale.z) / 300;
        
        _isResizing = true;
        
        StartCoroutine(DestroyHelperKit());
    }
    
    IEnumerator DestroyHelperKit()
    {
        yield return new WaitForSeconds(2);
        Destroy(obstacleInstance);
    }
}
