using UnityEngine;

public class GasCylinderBehaviour : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Lighter"))
        {
            //vou programar alguma coisa pro fogo que nao importe quantas vezes ative ok entao nao hao problemas nisso!!!!!
            Debug.Log("peguei fogo rs");
        }
    }
}
