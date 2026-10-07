using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager
{
    InputControls inputControls;

    private Vector2 InputDirection => inputControls.Player.Move.ReadValue<Vector2>();

    public event Action OnJumpPressed, OnSinestesyPressed, OnPickPressed, onShakePressed, onShakeReleased;

    public InputManager()
    {
        inputControls = new InputControls();
        inputControls.Enable();

        inputControls.Player.Jump.performed += OnJumpPerformed;
        inputControls.Player.Synesthesy.performed += OnSinestesyPerformed;
        inputControls.Player.Interact.performed += OnPickPerformed;
        inputControls.Player.FX.performed += OnShakePerformed;
        inputControls.Player.FX.canceled += OnShakeCanceled;
    }


    private void OnPickPerformed(InputAction.CallbackContext obj)
    {
        OnPickPressed?.Invoke();
    }

    private void OnShakePerformed(InputAction.CallbackContext obj)
    {
        onShakePressed?.Invoke();
    }
    private void OnShakeCanceled(InputAction.CallbackContext context)
    {
        onShakeReleased?.Invoke();
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
