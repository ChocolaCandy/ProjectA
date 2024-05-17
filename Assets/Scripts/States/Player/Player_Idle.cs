using UnityEngine;

public class Player_Idle : PlayerStates
{
    public Player_Idle(PlayerStateMachine stateMachine) : base(stateMachine) {}

    public override void OnEnter()
    {
        base.OnEnter();
        PlayerStateMachine.Controller.PlayerRigidbody.velocity = Vector3.zero;
    }
    public override void OnUpdate()
    {
        base.OnUpdate();
        if (PlayerMoveInput != Vector2.zero)
            ChangeWalk();
    }

    private void ChangeWalk()
    {
        PlayerStateMachine.OnChangeState(PlayerStateMachine.Walk);
    }
}
