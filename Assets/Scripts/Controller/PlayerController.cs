using UnityEditor.Android;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    public GameObject PlayerCamera { get; private set; }
    public Rigidbody PlayerRigidbody { get; private set; }

    private PlayerStateMachine PlayerStateMachine;

    public RigidbodyData PlayerRigidbodyData;

    private void Awake()
    {
        PlayerRigidbody = GetComponent<Rigidbody>();
        PlayerStateMachine = new PlayerStateMachine(this);
        Initialize();
    }

    private void OnEnable()
    {
        PlayerCamera = GameObject.FindGameObjectWithTag("PlayerCamera");
    }

    private void FixedUpdate()
    {
        PlayerStateMachine.OnFixUpdate();
    }

    private void OnTriggerEnter(Collider other)
    {
        PlayerStateMachine.OnTrigger_Enter(other);
    }

    private void Update()
    {
        PlayerStateMachine.OnUpdate();
    }

    /// <summary>
    /// 초기화 로직 메서드
    /// </summary>
    private void Initialize()
    {
        PlayerRigidbodyData.Init(PlayerRigidbody);
    }
}
