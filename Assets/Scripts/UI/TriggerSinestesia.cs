using System;
using UnityEngine;
using UnityEngine.UI;

public class TriggerSinestesia : MonoBehaviour
{
    private static readonly int Sinestesia = Animator.StringToHash("Sinestesia");
    [SerializeField] private GameObject olhoSinestesia;
    [SerializeField] private Animator animator;


    private void Awake()
    {
        olhoSinestesia = GameObject.FindGameObjectWithTag("Sinestesia");
        animator = olhoSinestesia.GetComponent<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            animator.SetBool("Sinestesia", true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        
        animator.SetBool("Sinestesia", false);    
    }
}
