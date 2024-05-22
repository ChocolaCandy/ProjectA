using UnityEngine;

//[RequireComponent (typeof(Input))]
[RequireComponent(typeof(Rigidbody))]
public class PlayerController : BaseController
{
    public Input PlayerInput { get; private set; }
    public GameObject PlayerCamera { get; private set; }
    public Rigidbody PlayerRigidbody { get; private set; }

    private PlayerStateMachine PlayerStateMachine;

    public RigidbodyData PlayerRigidbodyData;

    protected override void RunAwake()
    {
       // PlayerInput = GetComponent<Input>();
        PlayerCamera = GameObject.FindGameObjectWithTag("PlayerCamera");
        PlayerRigidbody = GetComponent<Rigidbody>();
        PlayerStateMachine = new PlayerStateMachine(this);
        DontDestroyOnLoad(gameObject);
        Init();
    }

    protected override void RunStart()
    {
        PlayerStateMachine.Init(PlayerStateMachine.Idle);
    }

    protected override void RunUpdate()
    {
        PlayerStateMachine.OnUpdate();
    }

    protected override void RunFixUpdate()
    {
        PlayerStateMachine.OnPhysicsUpdate();
    }

    protected override void RunTriggerEnter(Collider other)
    {
        PlayerStateMachine.OnTriggerEnter(other);
    }

    private void Init()
    {
        PlayerRigidbodyData.Init(PlayerRigidbody);
    }
}
