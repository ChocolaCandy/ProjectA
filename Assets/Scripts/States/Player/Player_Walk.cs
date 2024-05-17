using UnityEngine;

public class Player_Walk : PlayerStates
{
    public Player_Walk(PlayerStateMachine stateMachine) : base(stateMachine) {}

    public override void OnUpdate()
    {
        base.OnUpdate();
        if (PlayerMoveInput == Vector2.zero)
            ChangeIdle();
    }

    public override void OnPhysicsUpdate()
    {
        base.OnPhysicsUpdate();
        Move();
    }

    private void ChangeIdle()
    {
        PlayerStateMachine.OnChangeState(PlayerStateMachine.Idle);
    }
}
