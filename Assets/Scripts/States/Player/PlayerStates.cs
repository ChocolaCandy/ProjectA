using System;
using System.Threading;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerStates : BaseState
{
    public PlayerStates(PlayerStateMachine stateMachine)
    {
        PlayerStateMachine = stateMachine;
    }

    public PlayerStateMachine PlayerStateMachine { get; }
    public Vector2 PlayerMoveInput { get; private set; }
    private float SmoothValue = 0.3f;
    public float MoveSpeed = 5.0f;

    private int _smoothCount = 30;
    private int _defaultSmoothCount = 30;
    private float _defaultSmoothValue = 0.3f;
    Vector3 CurrentVector = Vector3.zero;
    Vector3 NewVector = Vector3.zero;

    //public bool isJumping { get; protected set; }
    public override void OnEnter()
    {
        //PlayerStateMachine.Controller.Input.Actions.Jump.started += getJumpInput;
        if (Utility.IsDebugMode) Debug.Log($"{GetType().Name} Enter");
    }

    public override void OnUpdate()
    {
        GetMoveInput();
    }
    public override void OnPhysicsUpdate()
    {

    }

    public override void OnExit()
    {
        // PlayerStateMachine.Controller.Input.Actions.Jump.started -= getJumpInput;
        if (Utility.IsDebugMode) Debug.Log($"{GetType().Name} Exit");
    }

    public override void OnTriggerEnter(Collider other)
    {

    }

    #region Input Methods
    private void GetMoveInput()
    {
        PlayerMoveInput = PlayerStateMachine.Controller.Input.Actions.Move.ReadValue<Vector2>();
    }
    #endregion

    #region Move Methods
    protected void Move()
    {
        Vector3 inputDirectionVector = GetInputDirectionVector();
        if (IsNewVector())
            InitValue();
        float rotateAngle = GetRotateAngle(inputDirectionVector);
        RotatePlayer(rotateAngle);
        MovePlayer(rotateAngle);
    }

    private bool IsNewVector()
    {
        if (CurrentVector == NewVector || NewVector == Vector3.zero)
            return false;
        CurrentVector = NewVector;
        return true;
    }

    private void InitValue()
    {
        Debug.Log("Init");
        _smoothCount = _defaultSmoothCount;
        SmoothValue = _defaultSmoothValue;
    }

    private Vector3 GetInputDirectionVector()
    {
        Vector3 inputVector = new Vector3(PlayerMoveInput.x, 0.0f, PlayerMoveInput.y);
        NewVector = inputVector;
        return inputVector;
    }

    private float GetRotateAngle(Vector3 inputDirectionVector)
    {
        float rotateAngle = Mathf.Atan2(inputDirectionVector.x, inputDirectionVector.z) * Mathf.Rad2Deg;
        if (rotateAngle < 0) rotateAngle += 360;
        rotateAngle += PlayerStateMachine.Controller.PlayerCamera.transform.eulerAngles.y;
        if (rotateAngle > 360) rotateAngle -= 360;
        return rotateAngle;
    }

    private void RotatePlayer(float rotateAngle) 
    {
        Debug.Log(_smoothCount);
        if (PlayerStateMachine.Controller.PlayerRigidbody.rotation == Quaternion.Euler(0.0f, rotateAngle, 0.0f))
        {
            Debug.Log("same");
            return;
        }
        if (_smoothCount == 0)
        {
            SmoothValue = 1.0f;
        }
        else
        {
            PlayerStateMachine.Controller.PlayerRigidbody.rotation = Quaternion.Lerp(PlayerStateMachine.Controller.PlayerRigidbody.rotation, Quaternion.Euler(0.0f, rotateAngle, 0.0f), SmoothValue);
            _smoothCount--;
        }

    }

    private void MovePlayer(float rotateAngle)
    {
        Vector3 rotateFowardVector = RotateFowardVector(rotateAngle);
        Vector3 currentVelocity = PlayerStateMachine.Controller.PlayerRigidbody.velocity;
        //currentVelocity.y = 0f;
        PlayerStateMachine.Controller.PlayerRigidbody.AddForce(rotateFowardVector * MoveSpeed - currentVelocity, ForceMode.VelocityChange);
    }

    private Vector3 RotateFowardVector(float rotateAngle)
    {
        return Quaternion.Euler(0, rotateAngle, 0) * Vector3.forward;
    }
    #endregion


    private void getJumpInput(InputAction.CallbackContext context)
    {
        //if(!isJumping)
        //    PlayerStateMachine.OnChangeState(PlayerStateMachine.Jump);
    }

}

