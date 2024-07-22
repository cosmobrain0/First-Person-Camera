using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{

    public float accelaration = 0;
    public float rotationSpeed = 0f;

    Vector2 previousMousePosition;

    void Start()
    {
        Rigidbody rigidbody = GetComponent<Rigidbody>();
        rigidbody.maxLinearVelocity = 2f;
        Vector3 mousePosition = Input.mousePosition;
        previousMousePosition = new Vector2(mousePosition.x, mousePosition.y);
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            // just pressed the left mouse button
            Vector3 mousePosition = Input.mousePosition;
            previousMousePosition = new Vector2(mousePosition.x, mousePosition.y);
        }
        if (Input.GetMouseButton(0))
        {
            Vector3 mousePosition3D = Input.mousePosition;
            Vector2 mousePosition = new Vector2(mousePosition3D.x, mousePosition3D.y);
            Vector2 mouseDelta = mousePosition - previousMousePosition;

            transform.Rotate(transform.InverseTransformDirection(Vector3.up), mouseDelta.x * rotationSpeed * Time.deltaTime);

            transform.Rotate(Vector3.right, -mouseDelta.y * rotationSpeed * Time.deltaTime);

            previousMousePosition = mousePosition;
        }
    }

    void FixedUpdate()
    {
        float horizontalMovement = Input.GetAxis("Horizontal");
        float forwardMovement = Input.GetAxis("Forward");
        float verticalMovement = Input.GetAxis("Upward");
        Vector3 movement = new Vector3(horizontalMovement, verticalMovement, forwardMovement).normalized;
        Rigidbody rigidbody = GetComponent<Rigidbody>();
        rigidbody.AddForce(transform.TransformDirection(accelaration * Time.fixedDeltaTime * 1000 * movement));

        if (movement == Vector3.zero) rigidbody.velocity *= Mathf.Exp(-14f * Time.fixedDeltaTime); ;
    }
}
