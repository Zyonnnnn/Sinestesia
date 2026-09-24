using UnityEngine;

public class DeathAreaTriggerBehaviour : MonoBehaviour, IHitable
{
    public void Execute(Transform executionSoruce, Rigidbody rb, int i)
    {
        SceneChanger.SceneChange("DeathScene");
    }
}