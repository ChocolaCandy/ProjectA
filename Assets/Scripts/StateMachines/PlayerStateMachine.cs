

public class PlayerStateMachine : BaseStateMachine<PlayerController>
{
    public PlayerStateMachine(PlayerController controller) : base(controller)
    { 
        Idle = new Player_Idle(this);
        Walk = new Player_Walk(this);
        Run = new Player_Run(this);
        Jump = new Player_Jump(this);
    }

    public Player_Idle Idle { get; }
    public Player_Walk Walk { get; }
    public Player_Run Run { get; }
    public Player_Jump Jump { get; }
}
