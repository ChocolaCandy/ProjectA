using UnityEngine;

public class Player_Idle : Player_Base
{
    public Player_Idle(PlayerStateMachine stateMachine) : base(stateMachine) { }

    protected override void SetOnEnter()
    {
        PlayerStateMachine.PlayerController.PlayerRigidbody.velocity = Vector3.zero;
        AddJumping();
        AddDashing();
    }

    protected override void SetOnUpdate()
    {
        GetMoveInput();
        if (PlayerMoveInput != Vector2.zero)
            ChangeStateToWalk();
    }
}
