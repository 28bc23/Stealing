using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputReader : IInputReader
{
    InputSystem_Actions inputActions;

    #region Player
    public Vector2 MoveDelta {  get; private set; }

    public bool IsSprinting { get; private set; }

    public Vector2 CursorDelta { get; private set; }
    #endregion

    #region DialogUI
    public event Action OnNextDialogEvent;
    public event Action OnOptionUpEvent;
    public event Action OnOptionDownEvent;
    #endregion

    public InputReader()
    {
        inputActions = new InputSystem_Actions();
        SubscribeAll();
    }

    public void EnablePlayer() => inputActions.Player.Enable();
    public void DisablePlayer() => inputActions.Player.Disable();

    public void EnableUI() => inputActions.UI.Enable();
    public void DisableUI() => inputActions.UI.Disable();

    public void EnableDialogUI() => inputActions.DialogUI.Enable();
    public void DisableDialogUI() => inputActions.DialogUI.Disable();

    void SubscribeAll()
    {
        #region Player
        inputActions.Player.Move.performed += OnMoveHandle;
        inputActions.Player.Move.canceled += OnMoveHandle;

        inputActions.Player.Sprint.performed += OnSprintHandle;
        inputActions.Player.Sprint.canceled += OnSprintHandle;

        inputActions.Player.Look.performed += OnLookHandle;
        inputActions.Player.Look.canceled += OnLookHandle;
        #endregion

        #region DialogUI
        inputActions.DialogUI.next.performed += OnNextDialogHandle;
        inputActions.DialogUI.optionUP.performed += OnOptionUpHandle;
        inputActions.DialogUI.optionDown.performed += OnOptionDownHandle;
        #endregion
    }

    void UnsubscribeAll()
    {
        #region Player
        inputActions.Player.Move.performed -= OnMoveHandle;
        inputActions.Player.Move.canceled -= OnMoveHandle;

        inputActions.Player.Sprint.performed -= OnSprintHandle;
        inputActions.Player.Sprint.canceled -= OnSprintHandle;

        inputActions.Player.Look.performed -= OnLookHandle;
        inputActions.Player.Look.canceled -= OnLookHandle;
        #endregion

        #region DialogUI
        inputActions.DialogUI.next.performed -= OnNextDialogHandle;
        inputActions.DialogUI.optionUP.performed -= OnOptionUpHandle;
        inputActions.DialogUI.optionDown.performed -= OnOptionDownHandle;
        #endregion
    }

    #region Player
    private void OnLookHandle(InputAction.CallbackContext context) => CursorDelta = context.ReadValue<Vector2>();
    private void OnSprintHandle(InputAction.CallbackContext context) => IsSprinting = context.ReadValueAsButton();
    void OnMoveHandle(InputAction.CallbackContext context) => MoveDelta = context.ReadValue<Vector2>();
    #endregion

    #region DialogUI
    private void OnOptionDownHandle(InputAction.CallbackContext context) => OnOptionDownEvent?.Invoke();

    private void OnOptionUpHandle(InputAction.CallbackContext context) => OnOptionUpEvent?.Invoke();

    private void OnNextDialogHandle(InputAction.CallbackContext context) => OnNextDialogEvent?.Invoke();
    #endregion
}

public interface IInputReader
{
    #region Player
    public Vector2 MoveDelta { get; }
    public Vector2 CursorDelta { get; }
    public bool IsSprinting { get; }
    #endregion

    #region DialogUI
    public event Action OnNextDialogEvent;
    public event Action OnOptionUpEvent;
    public event Action OnOptionDownEvent;
    #endregion

    public void EnablePlayer();
    public void DisablePlayer();

    public void EnableUI();
    public void DisableUI();

    public void EnableDialogUI();
    public void DisableDialogUI();
}
