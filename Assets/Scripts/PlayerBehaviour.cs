using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class PlayerBehaviour : MonoBehaviour
{
    Rigidbody rb;
    [SerializeField] float moveSpeed, jumpForce;
    InputSystem_Actions inputControls;
    private Vector2 InputDirection => inputControls.Player.Move.ReadValue<Vector2>();

    private void Awake()
    {
        inputControls = new InputSystem_Actions();
        inputControls.Enable();
    }
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        var moveX = InputDirection.x * (moveSpeed * 100) * Time.deltaTime;
        var moveZ = InputDirection.y * (moveSpeed * 100) * Time.deltaTime;

        rb.linearVelocity = new Vector3(moveX, rb.linearVelocity.y, moveZ);
    }

    private void OnDestroy()
    {
        inputControls.Disable();
    }

    private void OnDisable()
    {
        inputControls.Disable();
    }
}