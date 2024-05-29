using System;
using System.Collections;
using System.Threading;
using UnityEngine;

public class Player_Dash : Player_Base
{
    public Player_Dash(PlayerStateMachine stateMachine) : base(stateMachine) { }

    private float _dashSpeed = 10.0f;
    float Starttime = 0;

    protected override void SetOnEnter()
    {
        Starttime = Time.time;
        AddMoving();
        Dash();
    }

    protected override void SetOnUpdate()
    {
        if (Wait(0.5f))
        {
            if (PlayerMoveInput != Vector2.zero)
                ChangeStateToRun();
            else
                ChangeStateToIdle();
        }
    }

    private bool Wait(float requestTime)
    {
        float pastTime = Time.time - Starttime;
        if (requestTime <= pastTime)
            return true;
        return false;
    }

    private void Dash()
    {
        PlayerStateMachine.PlayerController.PlayerRigidbody.velocity = PlayerStateMachine.PlayerController.PlayerRigidbody.transform.forward * _dashSpeed;
    }
}
