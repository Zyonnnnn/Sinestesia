using Unity.VisualScripting;
using UnityEngine;

public class AreaTriggerSceneChanger : MonoBehaviour, IHitable
{
    public void Execute(Transform executionSoruce, Rigidbody rb, int i)
    {
        if (i == 1)
        {
            SceneChanger.SceneChange("PuzzlesScene");
        }
        else if (i == 2)
        {
            SceneChanger.SceneChange("BossScene");
        }
    }
}