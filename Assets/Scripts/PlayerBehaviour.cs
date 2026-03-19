using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class PlayerBehaviour : MonoBehaviour
{
    [SerializeField] float moveSpeed, jumpForce;
    
    InputSystem_Actions inputControls;  
    
    bool canJump, jumping;
    
    Rigidbody rb;
    InputAction jump;
    
    private Vector2 InputDirection => inputControls.Player.Move.ReadValue<Vector2>();

    private void Awake()
    {
        inputControls = new InputSystem_Actions();
        inputControls.Enable();
    }
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        
        jump = InputSystem.actions.FindAction("Jump");
    }

    private void Update()
    {
        if (jump.WasPressedThisFrame() && canJump)
        {
            jumping = true;
            canJump = false;
        }
        
        Debug.Log("Pode pula? " + canJump);
    }

    private void FixedUpdate()
    {
        var moveX = InputDirection.x * (moveSpeed * 100) * Time.deltaTime;
        var moveZ = InputDirection.y * (moveSpeed * 100) * Time.deltaTime;

        rb.linearVelocity = new Vector3(moveX, rb.linearVelocity.y, moveZ);
        
        if (jumping)
        {
            Debug.Log("eita preula");
            rb.AddForce(new(0, jumpForce, 0), ForceMode.Impulse);
            jumping = false;
        }
    }

    private void OnCollisionStay(Collision other)
    {
        
        if (other.collider.CompareTag("Ground"))
        {
            canJump = true;
        }
    }

    private void OnCollisionExit(Collision other)
    {
        
        if (other.collider.CompareTag("Ground"))
        {
            canJump = false;
        }
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