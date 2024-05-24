using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : BaseController
{
    public GameObject PlayerCamera { get; private set; }
    public Rigidbody PlayerRigidbody { get; private set; }

    private PlayerStateMachine PlayerStateMachine;

    public RigidbodyData PlayerRigidbodyData;

    protected override void SetComponent()
    {
        PlayerCamera = GameObject.FindGameObjectWithTag("PlayerCamera");
        PlayerRigidbody = GetComponent<Rigidbody>();
        PlayerStateMachine = new PlayerStateMachine(this);
    }

    protected override void Initialize()
    {
        PlayerRigidbodyData.Init(PlayerRigidbody);
        PlayerStateMachine.Init(PlayerStateMachine.Idle);
    }

    protected override void OnUpdate()
    {
        PlayerStateMachine.OnUpdate();
    }

    protected override void OnFixUpdate()
    {
        PlayerStateMachine.OnFixUpdate();
    }

    protected override void TriggerEnter(Collider other)
    {
        PlayerStateMachine.OnTriggerEnter(other);
    }
}
