
public class PlayerStateMachine : BaseStateMachine
{
    public PlayerController PlayerController;

    public PlayerStateMachine(PlayerController controller)
    { 
        Idle = new Player_Idle(this);
        Walk = new Player_Walk(this);
        Run = new Player_Run(this);
        Dash = new Player_Dash(this);
        Jump = new Player_Jump(this);
        PlayerController = controller;
    }

    public Player_Idle Idle { get; }
    public Player_Walk Walk { get; }
    public Player_Run Run { get; }
    public Player_Jump Jump { get; }
    public Player_Dash Dash { get; }
}
