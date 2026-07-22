using System;
using UnityEngine;
using UnityEngine.UI;

public class TriggerSinestesia : MonoBehaviour
{
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
            animator.Play("Eye_Opening");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        
        animator.Play("Eye_Closing"); 
    }
}
