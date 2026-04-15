using UnityEngine;

public class SoundParticlesBehaviour : MonoBehaviour
{
    void Start()
    {
        gameObject.GetComponent<ParticleSystem>().Stop();
    }
}
