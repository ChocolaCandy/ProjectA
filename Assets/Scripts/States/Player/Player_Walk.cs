using Unity.VisualScripting;
using UnityEngine;

public class Player_Walk : PlayerStates
{
    public Player_Walk(PlayerStateMachine stateMachine) : base(stateMachine) {}

    public override void OnEnter()
    {
        base.OnEnter();
    }
    public override void OnUpdate()
    {
        base.OnUpdate();
        if (MovementInput == Vector2.zero)
            ChangeIdle();
    }

    private void ChangeIdle()
    {
        PlayerStateMachine.OnChangeState(PlayerStateMachine.Idle);
    }
}
