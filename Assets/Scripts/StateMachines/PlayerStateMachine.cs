using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateMachine : BaseStateMachine<PlayerStates>
{
    public PlayerStateMachine(PlayerController controller) : base(controller)
    {
        IDLE = new Player_Idle();
    }

    public Player_Idle IDLE { get; }
}
