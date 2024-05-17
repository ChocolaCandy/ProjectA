using UnityEngine;

[RequireComponent (typeof (PlayerInput))]
public class PlayerController : BaseController
{
    public Camera PlayerCamera;
    public Rigidbody PlayerRigidbody;
    public PlayerStateMachine PlayerStateMachine;
    public PlayerInput Input { get; private set; }

    private void Awake()
    {
        PlayerCamera = Camera.main;
        PlayerRigidbody = GetComponent<Rigidbody>();
        PlayerStateMachine = new PlayerStateMachine(this);
        Input = GetComponent<PlayerInput>();
    }

    private void Start()
    {
        PlayerStateMachine.Init(PlayerStateMachine.Idle);
        DontDestroyOnLoad(gameObject);
    }

    private void Update()
    {
        PlayerStateMachine.OnUpdate();
    }

    private void FixedUpdate()
    {
        PlayerStateMachine.OnPhysicsUpdate();
    }

    private void OnTriggerEnter(Collider other)
    {
        PlayerStateMachine.OnTriggerEnter(other);
    }
}
