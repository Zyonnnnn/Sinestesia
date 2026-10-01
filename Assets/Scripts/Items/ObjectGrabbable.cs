using System;
using UnityEngine;

public class ObjectGrabbable : MonoBehaviour
{
    private Rigidbody rb;
    Transform target;
    
    [SerializeField] float lerpSpeed = 5f;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void Grab(Transform target)
    {
        this.target = target;
    }

    public void Release()
    {
        target = null;
        rb.useGravity = true;
    }

    private void FixedUpdate()
    {
        if (target != null)
        {
            Vector3 newPosition = Vector3.Lerp(rb.position, target.position, Time.deltaTime * lerpSpeed);
            rb.MovePosition(newPosition);
            rb.useGravity = false;
        }
    }
}
