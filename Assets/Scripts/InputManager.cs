using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager
{
    InputControls inputControls;

    private Vector2 InputDirection => inputControls.Player.Move.ReadValue<Vector2>();

    public event Action OnJumpPressed;

    public InputManager()
    {
        inputControls = new InputControls();
        inputControls.Enable();

        inputControls.Player.Jump.performed += OnJumpPerformed;
    }

    private void OnJumpPerformed(InputAction.CallbackContext obj)
    {
        OnJumpPressed?.Invoke();
    }

    public Vector2 GetInputDirection() => InputDirection;
    

    private void OnDestroy()
    {
        inputControls.Disable();
    }

    private void OnDisable()
    {
        inputControls.Disable();
    }
}
