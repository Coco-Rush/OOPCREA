using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PBO_Star : PBO_Parent
{
    public bool spinLikeCrazy = false;

    //  Mandatory method, required by the abstract method definition in the parent
    public override void SetNameAndScore()
    {
        nameAndScore.name = "Star";
        nameAndScore.score = 12;
    }

    //  Mandatory method, required by the abstract method definition in the parent
    public override Vector3 GetSpin()
    {
        return new Vector3(
            Random.Range(-10.0f, 10.0f),    // Spin around X-axis
            Random.Range(-10.0f, 10.0f),    // Spin around Y-axis 
            Random.Range(-10.0f, 10.0f)     // Spin around Z-axis
        );
    }

    //  Mandatory method, required by the abstract method definition in the parent
    public override void CatchEffect()
    {
        spinLikeCrazy = true;
        AudioSource mySuccessAudio = gameObject.AddComponent<AudioSource>();
        AudioClip successClip = Resources.Load<AudioClip>("Fupicat_fweeng-reverb");        
        mySuccessAudio.PlayOneShot(successClip);
    }

    // Keyword "new" hides parent method - which is then called with "base.Update()"
    new void Update()
    {
        base.Update();
        if(spinLikeCrazy)
        {
            gameObject.transform.Rotate(10.0f, 10.0f, 10.0f);
        }
    }

}
