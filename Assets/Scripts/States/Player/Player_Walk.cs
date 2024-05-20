using Unity.VisualScripting;
using UnityEngine;

public class Player_Walk : PlayerStates
{
    public Player_Walk(PlayerStateMachine stateMachine) : base(stateMachine) {}

    #region Private Fields
    //Player movement value
    private float _moveSpeed = 5.0f;
    private float _rotateTime = 0.13f;
    private float _rotateAngle = 0.0f;

    //Check input vector
    private Vector3 _newInput = Vector3.zero;
    private Vector3 _currentInput = Vector3.zero;

    //SmoothDamp parameter 
    private float _currentVelocity;
    private float _elapsedTime = 0.0f;
    #endregion

    public override void OnEnter()
    {
        isReadyToJump = true;
        base.OnEnter();
    }

    public override void OnUpdate()
    {
        base.OnUpdate();
        if (PlayerMoveInput == Vector2.zero)
            ChangeIdle();
    }

    public override void OnExit()
    {
        base.OnExit();
    }

    public override void OnPhysicsUpdate()
    {
        Move();
    }

    public override void OnTriggerEnter(Collider other)
    {
       
    }

    #region Move Methods
    /// <summary>
    /// 플레이어 이동 로직 메서드
    /// </summary>
    protected void Move()
    {
        Vector3 inputDirectionVector = GetInputDirectionVector();
        if (Is_newInput())
            InitValues();
        Set_rotateAngle(inputDirectionVector);
        RotatePlayer();
        MovePlayer();
    }

    /// <summary>
    /// Move입력키에 따른 방향벡터 반환 메서드
    /// </summary>
    /// <returns>InputDirectionVector</returns>
    private Vector3 GetInputDirectionVector()
    {
        Vector3 inputVector = new Vector3(PlayerMoveInput.x, 0.0f, PlayerMoveInput.y);
        _newInput = inputVector;
        return inputVector;
    }

    /// <summary>
    /// Move입력키가 다른 값인지 판단 메서드
    /// </summary>
    /// <returns>Boolean</returns>
    private bool Is_newInput()
    {
        if (_currentInput == _newInput)
            return false;
        return true;
    }

    /// <summary>
    /// 새로운 Move입력키 발생시 변수 초기화 메서드
    /// </summary>
    private void InitValues()
    {
        _currentInput = _newInput;
    }

    /// <summary>
    /// 방향벡터로 회전하기 위한 각도계산 메서드
    /// </summary>
    /// <param name="inputDirectionVector"></param>
    private void Set_rotateAngle(Vector3 inputDirectionVector)
    {
        float rotateAngle = Mathf.Atan2(inputDirectionVector.x, inputDirectionVector.z) * Mathf.Rad2Deg;
        if (rotateAngle < 0) rotateAngle += 360;
        rotateAngle += _playerController.PlayerCamera.transform.eulerAngles.y;
        if (rotateAngle > 360) rotateAngle -= 360;
        if (_rotateAngle == rotateAngle)
            return;
        _rotateAngle = rotateAngle;
        _elapsedTime = 0.0f;
    }

    /// <summary>
    /// 플레이어 회전 메서드
    /// </summary>
    private void RotatePlayer()
    {
        if (Mathf.Round(_playerController.PlayerRigidbody.transform.eulerAngles.y) == Mathf.Round(_rotateAngle))
            return;
        float currentAngle = _playerController.PlayerRigidbody.transform.eulerAngles.y;
        float smoothAngle = Mathf.SmoothDampAngle(currentAngle, _rotateAngle, ref _currentVelocity, _rotateTime - _elapsedTime);
        _elapsedTime += Time.fixedDeltaTime;
        _playerController.PlayerRigidbody.MoveRotation(Quaternion.Euler(0.0f, smoothAngle, 0.0f));
    }

    /// <summary>
    /// 플레이어 이동 메서드
    /// </summary>
    private void MovePlayer()
    {
        Vector3 rotateFowardVector = RotateFowardVector(_rotateAngle);
        Vector3 _currentVelocity = _playerController.PlayerRigidbody.velocity;
        _currentVelocity.y = 0f;
        _playerController.PlayerRigidbody.AddForce(rotateFowardVector * _moveSpeed - _currentVelocity, ForceMode.VelocityChange);
    }

    /// <summary>
    /// 플레이어 forward벡터 회전 메서드
    /// </summary>
    /// <param name="rotateAngle"></param>
    /// <returns></returns>
    private Vector3 RotateFowardVector(float rotateAngle)
    {
        return Quaternion.Euler(0, rotateAngle, 0) * Vector3.forward;
    }
    #endregion
}
