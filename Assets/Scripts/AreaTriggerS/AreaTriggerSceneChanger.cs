using Unity.VisualScripting;
using UnityEngine;

public class AreaTriggerSceneChanger : MonoBehaviour, IHitable
{
    GameObject uiFadeOut;

    private void Start()
    {
        uiFadeOut = GameObject.FindGameObjectWithTag("Fade");
    }
    public void Execute(Transform executionSoruce, Rigidbody rb, int i)
    {
        if (uiFadeOut == null)
        {
            uiFadeOut.GetComponentInChildren<FadeOut>().enabled = true;
        }

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