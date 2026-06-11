using UnityEngine;

public class BrokenPieceBehaviour : MonoBehaviour
{
    private BreakableGround bk;

    private void Awake()
    {
        bk = GetComponentInParent<BreakableGround>();
    }

    private void OnTriggerEnter(Collider other)
    {
        bk?.OnTriggerEnter(other);
    }
}
