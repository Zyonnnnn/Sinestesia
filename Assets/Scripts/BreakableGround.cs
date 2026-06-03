using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.Switch;

public class BreakableGround : MonoBehaviour
{
    bool isBroken;

    private List<Rigidbody> childrenRb;
    private List<GameObject> children;

    void Awake()
    {
        childrenRb = new List<Rigidbody>(GetComponentsInChildren<Rigidbody>());
    }

    private void Start()
    {
        foreach (Transform child in transform)
        {
            child.gameObject.AddComponent<BrokenPieceBehaviour>();
        }
    }

    void Update()
    {
        if (isBroken)
        {
            foreach (Rigidbody rb in childrenRb)
            { 
                rb.isKinematic = false;
            }
        }
        else
        {

            foreach (Rigidbody rb in childrenRb)
            {
                rb.isKinematic = true;
            }
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("EyeJump"))
        {
            isBroken = true;
        }
    }
}