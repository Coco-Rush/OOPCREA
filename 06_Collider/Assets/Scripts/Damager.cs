using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damager : Obstacle
{
    public int healthDeductions;
    public int damagePoints;
    protected GameObject carInstanceReference;
    
    // Start is called before the first frame update
    protected new void Start()
    {
        base.Start();
        carInstanceReference = GameObject.Find("Tocus");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public int GetDamage()
    {
        return this.damagePoints;
    }
}
