using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSelectObject : MonoBehaviour
{
    public GameObject highlightedObject;
    public bool holdingObject;
    public Material material;
    public float timeMultiplier;

    // Start is called before the first frame update
    void Start()
    {
        holdingObject = false;
    }

    // Update is called once per frame
    void Update()
    {
        material.SetFloat("_TimeParam", Time.time * timeMultiplier);
    }
}
