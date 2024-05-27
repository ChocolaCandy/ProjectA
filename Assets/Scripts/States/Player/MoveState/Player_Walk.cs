using UnityEngine;

public class Player_Walk : Player_Move
{
    public Player_Walk(PlayerStateMachine stateMachine) : base(stateMachine) { }

    private float _walkSpeed = 5.0f;

    protected override void SetOnEnter()
    {
        base.SetOnEnter();
        AddDashing();
    }

    protected override void SetOnUpdate()
    {
       base.SetOnUpdate();
    }

    protected override void SetSpeed()
    {
        _moveSpeed = _walkSpeed;
    }
}
