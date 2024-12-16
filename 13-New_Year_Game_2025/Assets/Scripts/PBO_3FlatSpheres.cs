using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PBO_3FlatSpheres : PBO_Parent
{
    public bool blowUp = false;
    public float blowUpSize = 1.0f;

    //  Mandatory method, required by the abstract method definition in the parent
    public override void SetNameAndScore()
    {
        nameAndScore.name = "FlatSphere";
        nameAndScore.score = 10;
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
        blowUp = true;
        AudioSource mySuccessAudio = gameObject.AddComponent<AudioSource>();
        AudioClip successClip = Resources.Load<AudioClip>("Fupicat_fweeng-reverb");
        mySuccessAudio.PlayOneShot(successClip);
    }

    // Keyword "new" hides parent method - which is then called with "base.Update()"
    new void Update()
    {
        base.Update();
        if(blowUp)
        {
            if (blowUpSize < 3)
            {
                blowUpSize += 0.02f;
                gameObject.transform.localScale = new Vector3(blowUpSize, blowUpSize, blowUpSize);
            }
        }
    }

}
