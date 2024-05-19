using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerStates : BaseState
{
    public PlayerStates(PlayerStateMachine stateMachine)
    {
        PlayerStateMachine = stateMachine;
    }

    #region Protected Fields
    //PlayerStateMachine
    protected PlayerStateMachine PlayerStateMachine { get; }

    //InputAction player move value
    protected Vector2 PlayerMoveInput { get; private set; }
    #endregion

    //public bool isJumping { get; protected set; }
    public override void OnEnter()
    {
        PlayerStateMachine.Controller.Input.Actions.Jump.started += ChangeJump;
        if (Utility.IsDebugMode) Debug.Log($"{GetType().Name} Enter");
    }

    public override void OnUpdate()
    {
        GetMoveInput();
    }

    public override void OnPhysicsUpdate()
    {

    }

    public override void OnExit()
    {
        PlayerStateMachine.Controller.Input.Actions.Jump.started -= ChangeJump;
        if (Utility.IsDebugMode) Debug.Log($"{GetType().Name} Exit");
    }

    public override void OnTriggerEnter(Collider other)
    {

    }

    #region Input Methods
    /// <summary>
    /// InputAcion의 플레이어 Move값 읽는 메서드
    /// </summary>
    private void GetMoveInput()
    {
        PlayerMoveInput = PlayerStateMachine.Controller.Input.Actions.Move.ReadValue<Vector2>();
    }
    #endregion

    protected void ChangeIdle()
    {
        PlayerStateMachine.OnChangeState(PlayerStateMachine.Idle);
    }

    protected void ChangeWalk()
    {
        PlayerStateMachine.OnChangeState(PlayerStateMachine.Walk);
    }

    protected void ChangeJump(InputAction.CallbackContext context)
    {
        PlayerStateMachine.OnChangeState(PlayerStateMachine.Jump);
    }

    protected void ChangePastState()
    {
        PlayerStateMachine.OnChangeState(PlayerStateMachine._pastState);
    }
}

