using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputReader : IInputReader
{
    InputSystem_Actions inputActions;

    public Vector2 MoveDelta {  get; private set; }

    public InputReader()
    {
        inputActions = new InputSystem_Actions();
        SubscribeAll();
    }

    void SubscribeAll()
    {
        inputActions.Player.Move.performed += OnMoveHandle;
    }

    void UnsubscribeAll()
    {
        inputActions.Player.Move.performed -= OnMoveHandle;
    }

    void OnMoveHandle(InputAction.CallbackContext context)
    {
        MoveDelta = context.ReadValue<Vector2>();
    }
}

public interface IInputReader
{
    public Vector2 MoveDelta { get; }
}
