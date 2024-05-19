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

    public override void OnPhysicsUpdate()
    {

    }


    public override void OnExit()
    {
        base.OnExit();
    }

    public override void OnTriggerEnter(Collider other)
    {
        
    }
}
