public class Player_Run : Player_Move
{
    public Player_Run(PlayerStateMachine stateMachine) : base(stateMachine) { }

    private float _runSpeed = 8.0f;
    protected override void SetSpeed()
    {
        _moveSpeed = _runSpeed;
    }

    protected override void SetOnUpdate()
    {
        base.SetOnUpdate();
        if (!RunButtonClicked)
            ChangeStateToWalk();
    }
}
