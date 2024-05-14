using System;
using Unity.VisualScripting;
using UnityEngine;

public class Player_Idle : PlayerStates
{
    public Player_Idle(PlayerStateMachine stateMachine) : base(stateMachine) {}

    public override void OnEnter()
    {
        base.OnEnter();
    }
    public override void OnUpdate()
    {
        base.OnUpdate();
        if (MovementInput == Vector2.zero)
            return;
        ChangeWalk();
    }

    public override void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            isJumping = false;
        }
    }

    private void ChangeWalk()
    {
        PlayerStateMachine.OnChangeState(PlayerStateMachine.Walk);
    }
}
