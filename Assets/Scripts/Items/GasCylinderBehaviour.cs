using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GasCylinderBehaviour : MonoBehaviour
{
    [SerializeField] float explosionForce = 10;
    [SerializeField] float explosionRadius = 10;
    [SerializeField] float explosionDelay = 3f;
    [SerializeField] float baseDistanceX;
    [SerializeField] float baseDistanceZ;


    [SerializeField] float grabRadius = 3f;

    Collider[] colliders = new Collider[20];

    [SerializeField] LayerMask layerMask;
    [SerializeField] Collider parentTriggerCollider;
    InputManager inputManager;

    ParticleSystem ps;
    Rigidbody rb;
    GameObject player;

    [SerializeField] List<GameObject> explosionPs = new();

    bool exploded;
    bool picked;

    Vector3 holdOffset;

    private void Awake()
    {
        inputManager = new InputManager();

        ps = GetComponent<ParticleSystem>();
        rb = GetComponent<Rigidbody>();
        parentTriggerCollider = GetComponent<Collider>();

        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Start()
    {
        PlayerBehaviour.OnPicked += HandlePicked;

        ps.Stop();
    }

    private void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (exploded || !IsParentTriggerTouching(other))
        {
            return;
        }

        if (other.CompareTag("Lighter"))
        {
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
        ps.Play();

        yield return new WaitForSeconds(explosionDelay);

        SpawnExplosion();

        ExplodeNonAlloc();

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
        picked = !picked;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }
}