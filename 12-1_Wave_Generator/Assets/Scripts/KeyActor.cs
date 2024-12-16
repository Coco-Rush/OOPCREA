using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class KeyActor : MonoBehaviour
{
    public bool active = false;

    public float maxAmplitude = 0.2f;
    public float frequency = 5.0f;


    private bool oldActive = false;

    public event Action OnPressingKey;
    public event Action OnReleasingKey;

    private WaveGenerator myWaveGenerator;
    // Start is called before the first frame update
    void Start()
    {
        myWaveGenerator = gameObject.AddComponent<WaveGenerator>();
        myWaveGenerator.maxAmplitude = maxAmplitude;
        myWaveGenerator.frequency = frequency;
        myWaveGenerator.startPos = new Vector3(gameObject.transform.localPosition.x, 1, 2);
    }

    // Update is called once per frame
    void Update()
    {
        if (active != oldActive)
        {
            if (active)
            {
                OnPressingKey?.Invoke();
                Debug.Log("Activate");
            }
            else
            {
                OnReleasingKey?.Invoke();
                Debug.Log("Deactivate");
            }
            oldActive = active;
        }
    }
}
