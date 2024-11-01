using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Damager : Obstacle, IHitAction
{
    private int _healthDeductions;
    public int damagePoints;
    
    // Start is called before the first frame update
    protected new void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public new void Impact()
    {
        
    }
    public new void Impact(int collisionSpeed)
    {
        
    }
    public new void Impact(float collisionSpeed)
    {
        
    }
    
    protected int GetDamage()
    {
        return CalculateDamage();
    }

    private int CalculateDamage()
    {
        this._healthDeductions = 0 - this.damagePoints;
        Debug.Log(_healthDeductions);
        return this._healthDeductions;
    }
}
