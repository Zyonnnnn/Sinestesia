using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.Switch;

public class BreakableGround : MonoBehaviour
{
    GameObject area;
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
        area = GameObject.FindGameObjectWithTag("1to2level");
    }

    void Update()
    {
        if (isBroken)
        {
            foreach (Rigidbody rb in childrenRb)
            { 
                rb.isKinematic = false;
            }
            area.SetActive(true);
        }
        else
        {
            foreach (Rigidbody rb in childrenRb)
            {
                rb.isKinematic = true;
            }
            area.SetActive(false);
        }

        Debug.Log(area);
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("EyeJump"))
        {
            isBroken = true;
        }
    }
}