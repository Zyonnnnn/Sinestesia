using UnityEngine;

public class AreaTriggerDeath : MonoBehaviour, IHitable
{

    public void Execute(Transform executionSoruce, Rigidbody rb, int key)
    {
        Debug.LogWarning("MORREU Pelo veneno!");
    }
}
