using UnityEngine;
using UnityEngine.InputSystem;

public abstract class Player_Base : BaseState
{
    public Player_Base(PlayerStateMachine stateMachine)
    {
        PlayerStateMachine = stateMachine;
    }

    protected PlayerStateMachine PlayerStateMachine { get; }
    protected Vector2 PlayerMoveInput { get; private set; }
    private bool _jumping = false;
    private bool _dashing = false;

    public override void OnEnter()
    {
        SetOnEnter();
        if (_jumping)
            Managers.InputManager.PlayerInput.Jump.started += ChangeStateToJump;
        if (_dashing)
            Managers.InputManager.PlayerInput.Dash.started += ChangeStateToDash;
    }

    public override void OnUpdate()
    {
        SetOnUpdate();
    }

    public override void OnFixUpdate()
    {
        SetOnFixUpdate();
    }

    public override void OnExit()
    {
        SetOnExit(); 
        if (_jumping)
            Managers.InputManager.PlayerInput.Jump.started -= ChangeStateToJump;
        if (_dashing)
            Managers.InputManager.PlayerInput.Dash.started -= ChangeStateToDash;
    }

    protected virtual void SetOnEnter() { }
    protected virtual void SetOnUpdate() { }
    protected virtual void SetOnFixUpdate() { }
    protected virtual void SetOnExit() { }


    /// <summary>
    /// InputAcion의 플레이어 Move값 읽는 메서드
    /// </summary>
    protected void GetMoveInput()
    {
        PlayerMoveInput = Managers.InputManager.PlayerInput.Move.ReadValue<Vector2>();
    }

    protected void AddJumping()
    {
        if(!_jumping)
            _jumping = true;
    }

    protected void AddDashing()
    {
        if (!_dashing)
            _dashing = true;
    }

    #region ChangeState
    protected void ChangeStateToIdle()
    {
        PlayerStateMachine.ChangeState(PlayerStateMachine.Idle);
    }

    protected void ChangeStateToWalk()
    {
        PlayerStateMachine.ChangeState(PlayerStateMachine.Walk);
    }
    protected void ChangeStateToRun()
    {
        PlayerStateMachine.ChangeState(PlayerStateMachine.Run);
    }
    protected void ChangeStateToPast()
    {
        PlayerStateMachine.ChangeState(PlayerStateMachine._pastState);
    }

    private void ChangeStateToDash(InputAction.CallbackContext context)
    {
        PlayerStateMachine.ChangeState(PlayerStateMachine.Dash);
    }
    private void ChangeStateToJump(InputAction.CallbackContext context)
    {
        PlayerStateMachine.ChangeState(PlayerStateMachine.Jump);
    }
    #endregion
}

