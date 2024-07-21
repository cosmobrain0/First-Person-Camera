using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{

    public float accelaration = 0;
    public float rotationSpeed = 0f;

    // Start is called before the first frame update
    void Start()
    {
        Rigidbody rigidbody = GetComponent<Rigidbody>();
        rigidbody.maxLinearVelocity = 2f;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float horizontalMovement = Input.GetAxis("Horizontal");
        float verticalMovement = Input.GetAxis("Vertical");
        Vector3 movement = new Vector3(horizontalMovement, 0, verticalMovement).normalized;
        float theta = -transform.rotation.eulerAngles.y / 180f * Mathf.PI;
        movement = new Vector3(Mathf.Cos(theta) * movement.x - Mathf.Sin(theta) * movement.z, 0f, Mathf.Sin(theta) * movement.x + Mathf.Cos(theta) * movement.z);
        Rigidbody rigidbody = GetComponent<Rigidbody>();
        rigidbody.AddForce(accelaration * Time.fixedDeltaTime * 1000 * movement);

        if (movement == Vector3.zero) rigidbody.velocity = new Vector3(0, rigidbody.velocity.y, 0);

        float rotation = 0f;
        if (Input.GetKey(KeyCode.Q)) rotation -= rotationSpeed;
        if (Input.GetKey(KeyCode.E)) rotation += rotationSpeed;
        transform.Rotate(new Vector3(0, rotation, 0));
    }
}
