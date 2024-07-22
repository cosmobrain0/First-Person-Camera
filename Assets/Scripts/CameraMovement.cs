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
