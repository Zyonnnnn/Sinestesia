using UnityEngine;

public class AreaTriggerDeath : MonoBehaviour, IHitable
{
    public GameObject playerObj;
    PlayerBehaviour playerBehaviour;

    public void Execute(Transform executionSoruce, Rigidbody rb, int key)
    { 
        playerBehaviour = rb.gameObject.GetComponent<PlayerBehaviour>();
        Debug.LogWarning("MORREU Pelo veneno!");
    }
}
