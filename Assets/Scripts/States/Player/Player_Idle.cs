using UnityEngine;

public class Player_Idle : PlayerStates
{
    public Player_Idle(PlayerStateMachine stateMachine) : base(stateMachine) {}

    public override void OnEnter()
    {
        isReadyToJump = true;
        base.OnEnter();
        _playerController.PlayerRigidbody.velocity = Vector3.zero;
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
        isReadyToJump = false;
        base.OnExit();
    }

    public override void OnTriggerEnter(Collider other)
    {
        
    }
}
