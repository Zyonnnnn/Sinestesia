using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class GasCylinderBehaviour : MonoBehaviour
{
    [SerializeField] float explosionForce = 10, explosionRadius = 10, explosionDelay = 3f, baseDistanceX, baseDistanceZ, grabRadius = 3f;

    Vector3 playerVel;

    Collider[] colliders = new Collider[20];

    [SerializeField] LayerMask layerMask;
    [SerializeField] Collider parentTriggerCollider;
    InputManager inputManager;
    AreaTriggerSnapGas areaTriggerSnapGas;

    ParticleSystem ps;
    Rigidbody rb;
    SpriteRenderer sp;

    GameObject player, wall;
    [SerializeField] GameObject light;

    [SerializeField] List<GameObject> explosionPs = new();

    bool exploded, picked, fogDiminish, snapped;

    Vector3 holdOffset;

    private void Awake()
    {
        inputManager = new InputManager();

        ps = GetComponent<ParticleSystem>();
        rb = GetComponent<Rigidbody>();
        sp = GetComponent<SpriteRenderer>();
        parentTriggerCollider = GetComponent<Collider>();

        player = GameObject.FindGameObjectWithTag("Player");
        wall = GameObject.FindGameObjectWithTag("BreakWall");
        light = GameObject.FindGameObjectWithTag("LightFire");
        areaTriggerSnapGas = GameObject.FindGameObjectWithTag("AreaSnap").GetComponent<AreaTriggerSnapGas>();
    }

    void Start()
    {
        PlayerBehaviour.OnPicked += HandlePicked;
        playerVel = player.GetComponent<Rigidbody>().linearVelocity;

        ps.Stop();
        light.SetActive(false);
    }

    private void Update()
    {
        snapped = areaTriggerSnapGas.snapped;

        if (snapped)
        {
            Debug.Log("Snapped");
            rb.constraints = RigidbodyConstraints.FreezeAll;
            picked = false;
        }

        if (picked)
        {
            if (Vector3.Distance(player.transform.position, transform.position) < grabRadius)
            {
                player.GetComponent<Rigidbody>().linearVelocity -= playerVel * 0.5f;
                player.GetComponent<Animator>().SetBool("BujaoWalk", true);

                holdOffset = new Vector3(baseDistanceX, 0f, baseDistanceZ);
                transform.position = player.transform.position + holdOffset;
                sp.enabled = false;

            }
            else
            {
                picked = false;
            }
        }
        else
        {
            player.GetComponent<Rigidbody>().linearVelocity = playerVel;
            player.GetComponent<Animator>().SetBool("BujaoWalk", false);
            sp.enabled = true;
        }
    }

    private void FixedUpdate()
    {
        if (fogDiminish && RenderSettings.fogDensity >= 0f)
        {
            RenderSettings.fogDensity -= 0.002f;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (exploded || !IsParentTriggerTouching(other))
        {
            return;
        }

        if (other.CompareTag("Lighter") && other.GetComponent<LighterBehaviour>().canFire)
        {
            fogDiminish = true;
            Destroy(other.gameObject);
            exploded = true;
            StartCoroutine(Explode());
        }

    }

    bool IsParentTriggerTouching(Collider other)
    {
        if (parentTriggerCollider == null)
        {
            return true;
        }

        return Physics.ComputePenetration(parentTriggerCollider, parentTriggerCollider.transform.position,
            parentTriggerCollider.transform.rotation, other, other.transform.position, other.transform.rotation, out _,
            out _);
    }

    IEnumerator Explode()
    {
        var fogPs = GameObject.FindGameObjectWithTag("Fog");
        if (fogPs != null)
        {
            Destroy(fogPs);
        }

        ps.Play();
        light.SetActive(true);

        yield return new WaitForSeconds(explosionDelay);

        SpawnExplosion();

        ExplodeNonAlloc();

        Destroy(wall);
        Destroy(gameObject);
    }

    private void SpawnExplosion()
    {
        foreach (var explosion in explosionPs)
        {
            Instantiate(explosion, transform.position, Quaternion.identity);
        }

    }

    void ExplodeNonAlloc()
    {
        int numColliders = Physics.OverlapSphereNonAlloc(transform.position, explosionRadius, colliders, layerMask);

        if (numColliders > 0)
        {
            for (int i = 0; i < numColliders; i++)
            {
                if (colliders[i].TryGetComponent(out Rigidbody rb))
                {
                    rb.AddExplosionForce(explosionForce * 1000, transform.position, explosionRadius);

                    if (UnityEngine.Random.Range(0, 2) == 0)
                    {
                        Destroy(rb.gameObject);
                    }
                }
            }
        }
    }
    private void HandlePicked()
    {
        if (!snapped)
        {
            picked = !picked;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }
}