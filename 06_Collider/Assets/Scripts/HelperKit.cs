using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

public class HelperKit : Obstacle
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
    private GameObject _carInstanceReference;


    // Start is called before the first frame update

    protected new void Start()
    {
        base.Start();
        obstacleInstance.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
        // obstacleBoxCollider.enabled = false;
        
        obstacleRigidbody.useGravity = false;
        _carInstanceReference = GameObject.Find("Tocus");
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
                _carInstanceReference.transform.position.y + 2f, 
                _carInstanceReference.transform.position.z);
        }
    }

    public int GetHeal()
    {
        this._smallHeal = normalHeal / 2;
        return _smallHeal;
    }

    public void StartScaleResizeWhenObjectInstanceCollected()
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
