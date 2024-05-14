using System;
using System.Threading;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerStates : BaseState
{
    public PlayerStates(PlayerStateMachine stateMachine)
    {
        PlayerStateMachine = stateMachine;
    }

    public PlayerStateMachine PlayerStateMachine { get; }
    public Vector2 MovementInput { get; private set; }
    public bool isJumping { get; protected set; }
    public override void OnEnter()
    {
        PlayerStateMachine.Controller.Input.Actions.Jump.started += getJumpInput;
        Debug.Log($"{GetType().Name} Enter");
    }

    public override void OnUpdate()
    {
        GetMoveInput();
        Debug.Log(isJumping);
    }

    public override void OnExit()
    {
        PlayerStateMachine.Controller.Input.Actions.Jump.started -= getJumpInput;
        Debug.Log($"{GetType().Name} Exit");
    }

    private void GetMoveInput()
    {
         MovementInput = PlayerStateMachine.Controller.Input.Actions.Move.ReadValue<Vector2>();
    }

    private void getJumpInput(InputAction.CallbackContext context)
    {
        if(!isJumping)
            PlayerStateMachine.OnChangeState(PlayerStateMachine.Jump);
    }

    public override void OnPhysicsUpdate()
    {

    }

    public override void OnTriggerEnter(Collider other)
    {

    }
}

