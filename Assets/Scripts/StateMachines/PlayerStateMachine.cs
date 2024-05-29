
public class PlayerStateMachine : BaseStateMachine
{
    public PlayerController PlayerController; //플레이어 컨트롤러

    //상태머신 생성자
    public PlayerStateMachine(PlayerController controller)
    { 
        Idle = new Player_Idle(this);
        Walk = new Player_Walk(this);
        Run = new Player_Run(this);
        Dash = new Player_Dash(this);
        Jump = new Player_Jump(this);

        PlayerController = controller;
        Init(Idle);
    }

    //상태 캐싱 변수
    public Player_Idle Idle { get; }
    public Player_Walk Walk { get; }
    public Player_Run Run { get; }
    public Player_Jump Jump { get; }
    public Player_Dash Dash { get; }
}
