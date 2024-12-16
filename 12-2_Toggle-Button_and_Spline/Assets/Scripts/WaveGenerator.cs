using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class WaveGenerator : MonoBehaviour
{
    public float frequency = 10.0f;
    public float maxAmplitude = 0.5f;
    public Vector3 startPos;

    private bool waveActive = false;
    private float size = 0.01f;
    private const int Length = 400;
    private GameObject[] aCubes = new GameObject[Length];
    private float amplitude = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        KeyActor parentObject = gameObject.GetComponent<KeyActor>();
        parentObject.OnPressingKey += startWave;
        parentObject.OnReleasingKey += stopWave;

        for (int i = 0; i <Length;i++)
        {
            aCubes[i] = GameObject.CreatePrimitive(PrimitiveType.Cube);
            aCubes[i].transform.localScale = new Vector3(size, size, size);
            aCubes[i].transform.localPosition = startPos + new Vector3(0, 0, i * size);
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Increase or decrease amplitude
        if (waveActive && amplitude < maxAmplitude) amplitude += 0.02f;
        if (!waveActive && amplitude > 0) amplitude -= 0.02f;

        // Migration of positions
        for (int i = Length - 2; i >= 0; i--)
        {
            aCubes[i + 1].transform.localPosition = new Vector3(
                    aCubes[i].transform.localPosition.x,
                    aCubes[i].transform.localPosition.y,
                    aCubes[i + 1].transform.localPosition.z
                );
        }

        // New Position
        aCubes[0].transform.localPosition = startPos + new Vector3(0.0f, amplitude * Mathf.Sin(Time.time * frequency), 0.0f);
    }
       
    void startWave()
    {
        waveActive = true;
        Debug.Log("Start wave");
    }

    void stopWave()
    {
        waveActive = false;
        Debug.Log("Stop wave");
    }

}
