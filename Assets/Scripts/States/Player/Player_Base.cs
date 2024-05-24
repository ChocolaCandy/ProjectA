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
    protected bool RunButtonClicked;
    private bool _jumpable = false;
    private bool _dashable = false;

    private float _rotateTime = 0.02f;
    private float _rotateAngle = 0.0f;

    //Check input vector
    private Vector3 _newInput = Vector3.zero;
    private Vector3 _currentInput = Vector3.zero;

    //SmoothDamp parameter 
    private float _currentVelocity;
    private float _elapsedTime = 0.0f;

    public override void OnEnter()
    {
        SetOnEnter();
        if (_jumpable)
            Managers.InputManager.PlayerInput.Jump.started += ChangeStateToJump;
        if (_dashable)
            Managers.InputManager.PlayerInput.Run.started += ChangeStateToDash;
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
        if (_jumpable)
            Managers.InputManager.PlayerInput.Jump.started -= ChangeStateToJump;
        if (_dashable)
            Managers.InputManager.PlayerInput.Run.started -= ChangeStateToDash;
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

    protected void GetRunPress()
    {
        RunButtonClicked =  Managers.InputManager.PlayerInput.Run.IsPressed();
    }

    protected void SetJumpable()
    {
        if(!_jumpable)
            _jumpable = true;
    }

    protected void SetDashable()
    {
        if (!_dashable)
            _dashable = true;
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

