using System.Collections.Generic;
using UnityEngine;

public class SinestesyDetection : MonoBehaviour
{
    private List<GameObject> soundObjectsInRange = new();
    Animator animator;

    private void Start()
    {
        animator = GameObject.FindGameObjectWithTag("Sinestesia").GetComponent<Animator>();
    }

    public ParticleSystem GetClosestParticleSystem()
    {
        if (soundObjectsInRange.Count == 0) return null;

        GameObject closest = null;
        var minDist = Mathf.Infinity;
        var playerPos = transform.parent.position; // ou transform.root

        foreach (GameObject obj in soundObjectsInRange)
        {
            if (obj == null)
            {
                continue;
            }
            
            var dist = Vector3.Distance(obj.transform.position, playerPos);
            if (dist < minDist)
            {
                minDist = dist;
                closest = obj;
            }
        }

        return closest?.GetComponent<ParticleSystem>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Sound") && !soundObjectsInRange.Contains(other.gameObject))
        {
            animator.SetBool("Sinestesia", true);
            soundObjectsInRange.Add(other.gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Sound"))
        {
            animator.SetBool("Sinestesia", false);
            soundObjectsInRange.Remove(other.gameObject);
        }
    }
}