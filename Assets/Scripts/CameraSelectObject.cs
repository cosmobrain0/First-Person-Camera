using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSelectObject : MonoBehaviour
{
    public GameObject highlightedObject;
    public Shader highlightShader;
    Material highlightMaterial;
    public float timeMultiplier;
    Shader highlightedObjectOriginalShader;

    public float maxHighlightDistance = 2;
    public string pickableTag;

    GameObject pickedObject;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        highlightMaterial?.SetFloat("_TimeParam", Time.time * timeMultiplier);
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            GameObject hitObject = hit.collider.gameObject;
            Vector3 hitPoint = hit.point;
            TrySetHighlightedObject(hitObject);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (pickedObject != null)
                DropObject();
            else if (highlightedObject != null)
                PickObject(highlightedObject);
        }
    }

    void DropObject()
    {
        if (pickedObject == null) return;
        pickedObject.transform.parent = null;
        pickedObject.GetComponent<Rigidbody>().useGravity = true;
        pickedObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
        pickedObject = null;
    }

    void PickObject(GameObject toPickUp)
    {
        toPickUp.transform.parent = transform;
        toPickUp.GetComponent<Rigidbody>().useGravity = false;
        toPickUp.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePosition;
        pickedObject = toPickUp;
    }

    void TrySetHighlightedObject(GameObject toHighlight)
    {
        if (highlightedObject == toHighlight) return;
        if (highlightedObject != null)
        {
            highlightedObject.GetComponent<Renderer>().material.shader = highlightedObjectOriginalShader;
            highlightedObject = null;
            highlightedObjectOriginalShader = null;
            highlightMaterial = null;
        }
        if (toHighlight == null) return;

        if (toHighlight.tag == pickableTag && (toHighlight.transform.position - transform.position).sqrMagnitude <= maxHighlightDistance*maxHighlightDistance)
        {
            highlightedObject = toHighlight;
            highlightedObjectOriginalShader = highlightedObject.GetComponent<Renderer>().material.shader;
            highlightedObject.GetComponent<Renderer>().material.shader = highlightShader;
            highlightMaterial = highlightedObject.GetComponent<Renderer>().material;
        }
    }
}
