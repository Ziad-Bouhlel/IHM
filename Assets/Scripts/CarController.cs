using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarMovement : MonoBehaviour
{
    public Rigidbody rg;
    public float steerForce = 5f;
    public float moveSpeed = 5f;

    private Vector2 input;

    void FixedUpdate()
    {
        rg.AddForce(input.y * this.transform.forward * moveSpeed, ForceMode.Acceleration);

        float rotation = input.x * steerForce * Time.fixedDeltaTime * input.y;
        transform.Rotate(0, rotation, 0, Space.World);
    }

    public void SetInputs(Vector2 input)
    {
        this.input = input;
    }
}
