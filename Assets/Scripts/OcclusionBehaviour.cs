using Unity.Cinemachine;
using UnityEngine;

public class OcclusionBehaviour : MonoBehaviour
{
    [SerializeField] private float fixedYaw, fixedPitch;
    private CinemachineCamera _vcam;

    void Start()
    {
        _vcam = GetComponent<CinemachineCamera>();
        
        fixedYaw = transform.eulerAngles.y;
        fixedPitch = transform.eulerAngles.x;
    }

    void Update()
    {
        if (_vcam != null)
        {
            Vector3 euler = transform.eulerAngles;
            euler.y = fixedYaw;
            euler.x = fixedPitch;
            
            transform.eulerAngles = euler;
        }
    }
}
