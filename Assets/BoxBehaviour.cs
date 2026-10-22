using UnityEngine;

public class BoxBehaviour : MonoBehaviour
{
    ParticleSystem ps;
    void Awake()
    {
        ps = GetComponent<ParticleSystem>();        
    }
    private void Start()
    {
        ps.Stop();
    }

    public void Fire()
    {
        ps.Play();
    }
}
