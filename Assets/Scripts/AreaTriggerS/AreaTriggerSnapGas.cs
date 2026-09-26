using System;
using UnityEngine;

public class AreaTriggerSnapGas : MonoBehaviour
{
    public bool snapped;
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("GasCollision"))
        {
            snapped = true;
            other.transform.position = new Vector3(37.25f, 1.5f, 20f);
        }
    }
}