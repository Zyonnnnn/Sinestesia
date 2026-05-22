using UnityEngine;

internal interface IHitable
{
    public void Execute(Transform executionSoruce, Rigidbody rb);
}