using System;
using Unity.VisualScripting;
using UnityEngine;

public class Player_Jump : PlayerStates
{
    public Player_Jump(PlayerStateMachine stateMachine) : base(stateMachine) { }

    public override void OnEnter()
    {
        //base.OnEnter();
        //isJumping = true;
        //Jump();
    }
    public override void OnTriggerEnter(Collider other)
    {
        //if (other.gameObject.layer == LayerMask.NameToLayer("Ground"))
        //{
        //    isJumping = false;
        //    PlayerStateMachine.OnChangeState(PlayerStateMachine.Idle);
        //}
    }

    private void Jump()
    {
        PlayerStateMachine.Controller.transform.position += 2.0f * Vector3.up;
    }
}
