using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameInput : MonoBehaviour
{
    private PlayerInput playerInput;


    public event EventHandler<OnScrollPerformedEventArg> OnScrollPerformedEvent;

    private float ScrollValueY;

    public class OnScrollPerformedEventArg : EventArgs
    {
        public float scroll;
    }


    private void Awake()
    {
        playerInput = new PlayerInput();
        playerInput.KeyBoard.Enable();
        playerInput.KeyBoard.Scroll.performed += x => { 
            ScrollValueY = x.ReadValue<float>();
            OnScrollPerformedEvent?.Invoke(this, new OnScrollPerformedEventArg
            {
                scroll = ScrollValueY
            });
        };
    }

    public Vector2 GetMovementVectorNormalized()
    {

        Vector2 inputVector = playerInput.KeyBoard.Move.ReadValue<Vector2>();

        inputVector = inputVector.normalized;

        return inputVector;
    }


}
