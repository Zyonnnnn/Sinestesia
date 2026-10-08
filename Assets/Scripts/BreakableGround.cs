using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.Switch;

public class BreakableGround : MonoBehaviour
{
    GameObject area;
    GameObject areaDeath;

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
        areaDeath = GameObject.FindGameObjectWithTag("areaDeath");
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
            areaDeath.SetActive(false);

            var ps = area.GetComponentInChildren<ParticleSystem>();
            ps.Play();
        }
        else
        {
            foreach (Rigidbody rb in childrenRb)
            {
                rb.isKinematic = true;
            }
            area.SetActive(false);
            areaDeath.SetActive(true);

            var ps = area.GetComponentInChildren<ParticleSystem>();
            ps.Stop();
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("EyeJump"))
        {
            if (other.gameObject.GetComponent<RangedEnemy>().inAttack)
            {
                isBroken = true;
            }
        }
    }
}