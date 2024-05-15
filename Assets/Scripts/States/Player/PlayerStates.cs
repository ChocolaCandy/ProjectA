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

    private float SmoothValue = 0.3f;


    //public bool isJumping { get; protected set; }
    public override void OnEnter()
    {
        //PlayerStateMachine.Controller.Input.Actions.Jump.started += getJumpInput;
        if (Utility.IsDebugMode) Debug.Log($"{GetType().Name} Enter");
    }

    public override void OnUpdate()
    {
        GetMoveInput();
    }
    public override void OnPhysicsUpdate()
    {
        if (MovementInput == Vector2.zero)
            return;
        Move();
    }

    public override void OnExit()
    {
        // PlayerStateMachine.Controller.Input.Actions.Jump.started -= getJumpInput;
        if (Utility.IsDebugMode) Debug.Log($"{GetType().Name} Exit");
    }

    public override void OnTriggerEnter(Collider other)
    {

    }

    #region Input Methods
    private void GetMoveInput()
    {
         MovementInput = PlayerStateMachine.Controller.Input.Actions.Move.ReadValue<Vector2>();
    }
    #endregion

    #region Move Methods
    protected void Move()
    {
        Vector3 movedir = GetMoveDirectionVector();
        RotatePlayer(movedir);
        PlayerStateMachine.Controller.transform.position += 2.0f * Time.fixedDeltaTime * PlayerStateMachine.Controller.transform.forward.normalized;
    }

    private Vector3 GetMoveDirectionVector()
    {
        return new Vector3(MovementInput.x, 0.0f, MovementInput.y);
    }

    protected void RotatePlayer(Vector3 moveDir)
    {
        float targetAngle = Mathf.Atan2(moveDir.x, moveDir.z) * Mathf.Rad2Deg;
        if (targetAngle < 0) targetAngle += 360;
        targetAngle += PlayerStateMachine.Controller.PlayerCamera.transform.eulerAngles.y;
        if (targetAngle > 360) targetAngle -= 360;
        PlayerStateMachine.Controller.transform.rotation = Quaternion.Lerp(PlayerStateMachine.Controller.transform.rotation, Quaternion.Euler(0.0f, targetAngle, 0.0f), SmoothValue);
    }
    #endregion


    private void getJumpInput(InputAction.CallbackContext context)
    {
        //if(!isJumping)
        //    PlayerStateMachine.OnChangeState(PlayerStateMachine.Jump);
    }

}

