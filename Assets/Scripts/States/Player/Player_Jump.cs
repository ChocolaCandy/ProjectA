using System;
using Unity.VisualScripting;
using UnityEngine;

public class Player_Jump : PlayerStates
{
    public Player_Jump(PlayerStateMachine stateMachine) : base(stateMachine) { }

    private float _jumpForce = 5.0f;

    public override void OnEnter()
    {
        Jump();
    }
    public override void OnUpdate()
    {

    }

    public override void OnExit()
    {
        
    }

    public override void OnPhysicsUpdate()
    {
        
    }

    public override void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            ChangePastState();
        }
    }

    private void Jump()
    {
        PlayerStateMachine.Controller.PlayerRigidbody.AddRelativeForce(Vector3.up * _jumpForce, ForceMode.VelocityChange);
    }
}
