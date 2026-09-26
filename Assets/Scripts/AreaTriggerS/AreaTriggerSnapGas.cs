using System;
using UnityEngine;

public class AreaTriggerSnapGas : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("GasCollision"))
        {
            other.transform.position = new Vector3(37.25f, 1.5f, 20f);
        }
    }
}