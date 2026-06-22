using Unity.VisualScripting;
using UnityEngine;

public class AreaTriggerSceneChanger : MonoBehaviour, IHitable
{
    public void Execute(Transform executionSoruce, Rigidbody rb)
    {
        SceneChanger.SceneChange("PuzzlesScene");
    }
}