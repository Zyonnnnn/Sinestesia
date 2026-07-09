using UnityEngine;

public class GasCylinderBehaviour : MonoBehaviour
{
    [SerializeField] float explosionForce = 10;
    [SerializeField] float explosionRadius = 10;

    Collider[] colliders = new Collider[20];

    [SerializeField] LayerMask layerMask;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Lighter"))
        {
            //vou programar alguma coisa pro fogo que nao importe quantas vezes ative ok entao nao hao problemas nisso!!!!!
            Debug.Log("peguei fogo rs");

            ExplodeNonAlloc();
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

                    Debug.Log(rb.name);
                }
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }
}
