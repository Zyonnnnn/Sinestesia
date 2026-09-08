using System;
using UnityEngine;
using UnityEngine.UI;

public class TriggerSinestesia : MonoBehaviour
{
    private static readonly int Sinestesia = Animator.StringToHash("Sinestesia");
    [SerializeField] private GameObject olhoSinestesia;
    [SerializeField] private Animator animator;
    [SerializeField] private Animator animatorP;
    public GameObject player;


    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        olhoSinestesia = GameObject.FindGameObjectWithTag("Sinestesia");
        animator = olhoSinestesia.GetComponent<Animator>();
        animatorP = player.GetComponent<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            animator.SetBool("Sinestesia", true);
            animatorP.SetBool("Sinestesia", true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            animator.SetBool("Sinestesia", false);
            animatorP.SetBool("Sinestesia", false);
        }
    }
}
