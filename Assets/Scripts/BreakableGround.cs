using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.Switch;

public class BreakableGround : MonoBehaviour
{
    [SerializeField] GameObject rock1, rock2, rock3;
    GameObject area, areaDeath;

    bool isBroken;

    private List<Rigidbody> childrenRb;

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
            Debug.Log("Particle System: " + ps.name);
            ps.Stop();
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("EyeJump"))
        {
            if (other.gameObject.GetComponent<RangedEnemy>().inAttack)
            {
                foreach (Rigidbody rb in childrenRb)
                {
                    Instantiate(rock1, rb.gameObject.transform);
                    Instantiate(rock2, rb.gameObject.transform);
                    Instantiate(rock3, rb.gameObject.transform);
                }

                isBroken = true;
                Destroy(other.gameObject);
            }
        }
    }
}