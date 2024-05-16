using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent (typeof (PlayerInput))]
public class PlayerController : BaseController
{
    public Camera PlayerCamera;

    public Rigidbody PlayerRigidbody;

    //public float rotation = 50;
    public PlayerStateMachine _playerStateMachine;
    public PlayerInput Input { get; private set; }
    private void Awake()
    {
        PlayerCamera = Camera.main;
        PlayerRigidbody = GetComponent<Rigidbody>();
        _playerStateMachine = new PlayerStateMachine(this);
        Input = GetComponent<PlayerInput>();
    }

    private void Start()
    {
        _playerStateMachine.Init(_playerStateMachine.Idle);
    }

    private void Update()
    {
        _playerStateMachine.OnUpdate();
    }
    private void FixedUpdate()
    {
        _playerStateMachine.OnPhysicsUpdate();
    }
    private void OnTriggerEnter(Collider other)
    {
        _playerStateMachine.OnTriggerEnter(other);
    }





    //private void Awake()
    //{
    //    _mainCamera = GetComponent<Camera>();
    //}

    //private void Start()
    //{ 
    //    Managers.InputManager.KeyBoardInput += Key;
    //    DontDestroyOnLoad(gameObject);
    //}


    //void Key()
    //{
    //    //TODO 캐릭터 움직임 개선(테스트용)
    //    if (Input.GetKey(KeyCode.S))
    //    {
    //        gameObject.transform.position += Vector3.back * 3 * Time.deltaTime;
    //    }
    //    if (Input.GetKey(KeyCode.W))
    //    {
    //        gameObject.transform.position += Vector3.forward * 3 * Time.deltaTime;
    //    }
    //    if (Input.GetKey(KeyCode.D))
    //    {
    //        gameObject.transform.position += Vector3.right * 3 * Time.deltaTime;
    //    }
    //    if (Input.GetKey(KeyCode.A))
    //    {
    //        gameObject.transform.position += Vector3.left * 3 * Time.deltaTime;
    //    }
    //    if (Input.GetKeyDown(KeyCode.Space))
    //    {
    //        gameObject.GetComponent<Rigidbody>().AddForce(Vector3.up * 5, ForceMode.Impulse);
    //    }
    //}
}
