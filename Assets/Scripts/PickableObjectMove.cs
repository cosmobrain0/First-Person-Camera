using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PickableObjectMove : MonoBehaviour
{
    GameObject holder;
    float holderMaxDistance;
    float holderIdealDistance;
    bool pickedUp;

    public float accelaration;

    public bool PickedUp { get => pickedUp; }

    // Start is called before the first frame update
    void Start()
    {
        pickedUp = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (pickedUp)
        {
            Vector3 globalTarget = holder.transform.position + holder.transform.forward * holderIdealDistance;
            Vector3 globalPosition = transform.position;
            Vector3 offset = globalTarget - globalPosition;
            Rigidbody rb = GetComponent<Rigidbody>();
            rb.AddForce(offset * offset.magnitude * accelaration * Time.deltaTime);
            rb.velocity *= Mathf.Exp(-2 * Time.deltaTime);

            if (offset.sqrMagnitude >= holderMaxDistance * holderMaxDistance || Vector3.Dot((transform.position-holder.transform.position).normalized, holder.transform.forward) <= 0.8f) pickedUp = false;
        }
    }

    public void PickUp(GameObject holder, float holderMaxDistance, float holderIdealDistance)
    {
        this.holder = holder;
        this.holderMaxDistance = holderMaxDistance;
        this.holderIdealDistance = holderIdealDistance;
        GetComponent<Rigidbody>().useGravity = false;
        GetComponent<Rigidbody>().angularDrag = 0.25f;
        pickedUp = true;
    }

    public void Drop()
    {
        holder = null;
        holderMaxDistance = 0;
        holderIdealDistance = 0;
        pickedUp = false;
        GetComponent<Rigidbody>().useGravity = true;
        GetComponent<Rigidbody>().angularDrag = 0.05f;
    }
}
