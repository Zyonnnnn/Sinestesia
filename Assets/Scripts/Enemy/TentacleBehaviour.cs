using UnityEngine;

public class TentacleBehaviour : MonoBehaviour
{
    SpriteRenderer spr;

    private void Start()
    {
        spr = GetComponentInChildren<SpriteRenderer>();
    }
    
    void Update()
    {

        if (BossChasingState.isMoving)
        {
            spr.enabled = true;
        }
        else
        {
            spr.enabled = false;
        }
    }
}
