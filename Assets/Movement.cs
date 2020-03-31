using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public Rigidbody rb;
    public float forwardForce = 50f;


    void FixedUpdate()
    {
        if (Input.GetKey("w"))
        {
            rb.AddForce(forwardForce * Time.deltaTime, 0, 0, ForceMode.VelocityChange);
        }
    }
}
