using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingGameCamera : MonoBehaviour
{
    public Rigidbody rigidbodyOnCamera;
    public float moveSpeed;

    void FixedUpdate()
    {
        var verticalInput = Input.GetAxis("Vertical");
        var horizontalInput = Input.GetAxis("Horizontal");

        Vector3 direction = new Vector3(horizontalInput, 0, verticalInput).normalized;

        rigidbodyOnCamera.transform.Translate(direction * moveSpeed, Space.World);

    }
}
