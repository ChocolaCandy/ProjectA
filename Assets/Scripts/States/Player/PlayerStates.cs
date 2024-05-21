using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerStates : BaseState
{
    public PlayerStates(PlayerStateMachine stateMachine)
    {
        PlayerStateMachine = stateMachine;
       // _playerController = PlayerStateMachine.Controller;
    }

    #region Protected Fields
    //PlayerStateMachine
    protected PlayerStateMachine PlayerStateMachine { get; }
    //PlayerController
    protected PlayerController _playerController;
    //InputAction player move value
    protected Vector2 PlayerMoveInput { get; private set; }
    #endregion

    public bool isReadyToJump { get; protected set; } = false;

    public override void OnEnter()
    {
      //  _playerController.Input.Actions.Jump.started += ChangeJump;
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
      //  _playerController.Input.Actions.Jump.started -= ChangeJump;
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
       // PlayerMoveInput = _playerController.Input.Actions.Move.ReadValue<Vector2>();
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
        if(isReadyToJump)
            PlayerStateMachine.OnChangeState(PlayerStateMachine.Jump);
    }

    protected void ChangePastState()
    {
        PlayerStateMachine.OnChangeState(PlayerStateMachine._pastState);
    }
}

