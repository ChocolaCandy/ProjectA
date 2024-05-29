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
    private bool _moving = false;
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

    public override void OnFixUpdate()
    {
        SetOnFixUpdate();
    }

    public override void OnUpdate()
    {
        if (_moving)
            PlayerMoveInput = Managers.InputManager.PlayerInput.Move.ReadValue<Vector2>();
        SetOnUpdate();
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
    protected virtual void SetOnFixUpdate() { }
    protected virtual void SetOnUpdate() { }
    protected virtual void SetOnExit() { }

    protected void AddMoving()
    {
        if (!_moving)
            _moving = true;
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
    /// <summary>
    /// IDLE���·� ��ȯ
    /// </summary>
    protected void ChangeStateToIdle()
    {
        PlayerStateMachine.ChangeState(PlayerStateMachine.Idle);
    }

    /// <summary>
    /// Move���·� ��ȯ
    /// </summary>
    protected void ChangeStateToWalk()
    {
        PlayerStateMachine.ChangeState(PlayerStateMachine.Walk);
    }

    /// <summary>
    /// Run���·� ��ȯ
    /// </summary>
    protected void ChangeStateToRun()
    {
        PlayerStateMachine.ChangeState(PlayerStateMachine.Run);
    }

    /// <summary>
    /// �������·� ��ȯ
    /// </summary>
    protected void ChangeStateToPast()
    {
        PlayerStateMachine.ChangePastState();
    }

    /// <summary>
    /// Dash���·� ��ȯ
    /// </summary>
    private void ChangeStateToDash(InputAction.CallbackContext context)
    {
        PlayerStateMachine.ChangeState(PlayerStateMachine.Dash);
    }

    /// <summary>
    /// Jump���·� ��ȯ
    /// </summary>
    private void ChangeStateToJump(InputAction.CallbackContext context)
    {
        PlayerStateMachine.ChangeState(PlayerStateMachine.Jump);
    }
    #endregion
}

