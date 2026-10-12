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

        if (area == null)
        {
            Debug.LogWarning("BreakableGround: no GameObject found with tag '1to2level'", this);
        }
        if (areaDeath == null)
        {
            Debug.LogWarning("BreakableGround: no GameObject found with tag 'areaDeath'", this);
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
            if (area != null) area.SetActive(true);
            if (areaDeath != null) areaDeath.SetActive(false);

            if (area != null)
            {
                var ps = area.GetComponentInChildren<ParticleSystem>();
                if (ps != null) ps.Play();
            }
        }
        else
        {
            foreach (Rigidbody rb in childrenRb)
            {
                rb.isKinematic = true;
            }
            if (area != null) area.SetActive(false);
            if (areaDeath != null) areaDeath.SetActive(true);

            if (area != null)
            {
                var ps = area.GetComponentInChildren<ParticleSystem>();
                if (ps != null)
                {
                    Debug.Log("Particle System: " + ps.name);
                    ps.Stop();
                }
            }
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("EyeJump"))
        {
            var ranged = other.gameObject.GetComponentInParent<RangedEnemy>();
            if (ranged == null)
            {
                Debug.LogWarning("BreakableGround: collided EyeJump has no RangedEnemy in parents", other.gameObject);
                return;
            }

            if (ranged.inAttack)
            {
                foreach (Rigidbody rb in childrenRb)
                {
                    if (rock1 != null) Instantiate(rock1, rb.gameObject.transform);
                    if (rock2 != null) Instantiate(rock2, rb.gameObject.transform);
                    if (rock3 != null) Instantiate(rock3, rb.gameObject.transform);
                }

                isBroken = true;
                Destroy(ranged.gameObject);
            }
        }
    }
}