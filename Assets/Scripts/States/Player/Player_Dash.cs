using System;
using System.Collections;
using System.Threading;
using UnityEngine;

public class Player_Dash : Player_Base
{
    public Player_Dash(PlayerStateMachine stateMachine) : base(stateMachine) { }

    private float _dashSpeed = 5.0f;
    private bool isEnterEnd = false;
    float Starttime = 0;
    protected override void SetOnEnter()
    {
        Starttime = Time.time;
        Dash();
    }

    protected override void SetOnUpdate()
    {
        if (Wait(0.5f))
        {
            GetMoveInput();
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
        //ToDo Dash Logic
        PlayerStateMachine.PlayerController.PlayerRigidbody.AddForce(PlayerStateMachine.PlayerController.PlayerRigidbody.transform.forward * _dashSpeed, ForceMode.VelocityChange);
    }
}
