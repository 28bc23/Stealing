using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputReader : IInputReader
{
    InputSystem_Actions inputActions;

    public Vector2 MoveDelta {  get; private set; }

    public bool IsSprinting { get; private set; }

    public Vector2 CursorDelta { get; private set; }

    public InputReader()
    {
        inputActions = new InputSystem_Actions();
        SubscribeAll();
    }

    public void EnablePlayer() => inputActions.Player.Enable();
    public void DisablePlayer() => inputActions.Player.Disable();

    public void EnableUI() => inputActions.UI.Enable();
    public void DisableUI() => inputActions.UI.Disable();

    void SubscribeAll()
    {
        inputActions.Player.Move.performed += OnMoveHandle;
        inputActions.Player.Move.canceled += OnMoveHandle;

        inputActions.Player.Sprint.performed += OnSprintHandle;
        inputActions.Player.Sprint.canceled += OnSprintHandle;

        inputActions.Player.Look.performed += OnLookHandle;
        inputActions.Player.Look.canceled += OnLookHandle;
    }

    void UnsubscribeAll()
    {
        inputActions.Player.Move.performed -= OnMoveHandle;
        inputActions.Player.Move.canceled -= OnMoveHandle;

        inputActions.Player.Sprint.performed -= OnSprintHandle;
        inputActions.Player.Sprint.canceled -= OnSprintHandle;


        inputActions.Player.Look.performed -= OnLookHandle;
        inputActions.Player.Look.canceled -= OnLookHandle;
    }

    private void OnLookHandle(InputAction.CallbackContext context) => CursorDelta = context.ReadValue<Vector2>();
    private void OnSprintHandle(InputAction.CallbackContext context) => IsSprinting = context.ReadValueAsButton();
    void OnMoveHandle(InputAction.CallbackContext context) => MoveDelta = context.ReadValue<Vector2>();
}

public interface IInputReader
{
    public Vector2 MoveDelta { get; }
    public Vector2 CursorDelta { get; }
    public bool IsSprinting { get; }

    public void EnablePlayer();
    public void DisablePlayer();

    public void EnableUI();
    public void DisableUI();
}
