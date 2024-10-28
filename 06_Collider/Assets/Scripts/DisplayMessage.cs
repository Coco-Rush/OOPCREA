using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DisplayMessage : MonoBehaviour
{
    private AnimateCar animatedCarReference;

    private TextMeshPro textDisplay;
    private GameObject backPlane;
    private Material greyMaterial;

    // Start is called before the first frame update
    void Start()
    {
        animatedCarReference = GetComponent<AnimateCar>();

        GameObject carInstanceReference = GameObject.Find("Tocus");
        Debug.Log(carInstanceReference);

        textDisplay = carInstanceReference.AddComponent<TextMeshPro>();
        textDisplay.fontSize = 4;
        textDisplay.alignment = TextAlignmentOptions.Top;

        //  Create and configure background plane
        //  =====================================
        backPlane = GameObject.CreatePrimitive(PrimitiveType.Plane);
        
        backPlane.transform.localPosition = new Vector3(0f, 3f, 0.1f);
        backPlane.transform.localRotation = Quaternion.Euler(-90, 0, 0);
        backPlane.transform.localScale = new Vector3(0.5f, 0.3f, 0.15f);
        
        /*
        backPlane.transform.localPosition = new Vector3(0f, 2.2f, 0.01f);
        backPlane.transform.localRotation = Quaternion.Euler(-90.0f, 0.0f, 0.0f);
        backPlane.transform.localScale = new Vector3(0.8f, 1.0f, 0.1f);
        */
        
        greyMaterial = Resources.Load("GreyMat") as Material;
        backPlane.GetComponent<MeshRenderer>().material = greyMaterial;
        backPlane.transform.SetParent(carInstanceReference.transform);

    }   // End of Start()

    // Update is called once per frame
    void Update()
    {

        textDisplay.text = animatedCarReference.textMessage;

    }   // End of Update()
}
