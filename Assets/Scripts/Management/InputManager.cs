using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager
{
    InputControls inputControls;

    private Vector2 InputDirection => inputControls.Player.Move.ReadValue<Vector2>();

    public event Action OnJumpPressed;
    public event Action OnSinestesyPressed;
    public event Action OnPickPressed;

    public InputManager()
    {
        inputControls = new InputControls();
        inputControls.Enable();

        inputControls.Player.Jump.performed += OnJumpPerformed;
        inputControls.Player.Synesthesy.performed += OnSinestesyPerformed;
        inputControls.Player.Interact.performed += OnPickPerformed;
    }

    private void OnPickPerformed(InputAction.CallbackContext obj)
    {
        OnPickPressed?.Invoke();
    }

    private void OnJumpPerformed(InputAction.CallbackContext obj)
    {
        OnJumpPressed?.Invoke();
    }
    private void OnSinestesyPerformed(InputAction.CallbackContext obj)
    {
        OnSinestesyPressed?.Invoke();
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
