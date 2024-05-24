using UnityEngine;

public class Player_Dash : Player_Base
{
    public Player_Dash(PlayerStateMachine stateMachine) : base(stateMachine) { }

    private float _dashSpeed = 5.0f;

    protected override void SetOnEnter()
    {
        Dash();
    }


    protected override void SetOnUpdate()
    {
        ChangeStateToIdle();
    }

    private void Dash()
    {
        //ToDo Dash Logic
        PlayerStateMachine.PlayerController.PlayerRigidbody.AddForce(PlayerStateMachine.PlayerController.PlayerRigidbody.transform.forward * _dashSpeed, ForceMode.VelocityChange);
    }
}
