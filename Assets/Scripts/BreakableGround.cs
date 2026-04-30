using System;
using UnityEngine;

public class BreakableGround : MonoBehaviour
{
    Rigidbody rb;
    bool isBroken;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (isBroken)
        {
            rb.isKinematic = false;
        }
        else
        {
            rb.isKinematic = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("EyeJump"))
        {
            isBroken = true;
        }
    }
}