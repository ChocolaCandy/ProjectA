using UnityEngine;

public abstract class Player_Move : Player_Base
{
    public Player_Move(PlayerStateMachine stateMachine) : base(stateMachine) { }

    //RoateValue
    private float _rotateTime = 0.02f;
    private float _rotateAngle = 0.0f;

    //Check input vector
    private Vector3 _newInput = Vector3.zero;
    private Vector3 _currentInput = Vector3.zero;

    //SmoothDamp parameter 
    private float _currentVelocity;
    private float _elapsedTime = 0.0f;
    
    //MoveSpeed
    protected float _moveSpeed = 0.0f;

    protected override void SetOnEnter()
    {
        SetSpeed();
        SetJumpable();
    }

    protected override void SetOnUpdate()
    {
        GetMoveInput();
        if (PlayerMoveInput == Vector2.zero)
            ChangeStateToIdle();
    }

    protected override void SetOnFixUpdate()
    {
        Move(_moveSpeed);
    }

    protected abstract void SetSpeed();

    /// <summary>
    /// 플레이어 이동 로직 메서드
    /// </summary>
    protected void Move(float moveSpeed)
    {
        Vector3 inputDirectionVector = GetInputDirectionVector();
        if (Is_newInput())
            InitValues();
        Set_rotateAngle(inputDirectionVector);
        RotatePlayer();
        MovePlayer(moveSpeed);
    }

    #region Move Methods
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
        rotateAngle += PlayerStateMachine.PlayerController.PlayerCamera.transform.eulerAngles.y;
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
        if (Mathf.Round(PlayerStateMachine.PlayerController.PlayerRigidbody.transform.eulerAngles.y) == Mathf.Round(_rotateAngle))
            return;
        float currentAngle = PlayerStateMachine.PlayerController.PlayerRigidbody.transform.eulerAngles.y;
        float smoothAngle = Mathf.SmoothDampAngle(currentAngle, _rotateAngle, ref _currentVelocity, _rotateTime - _elapsedTime);
        _elapsedTime += Time.fixedDeltaTime;
        PlayerStateMachine.PlayerController.PlayerRigidbody.MoveRotation(Quaternion.Euler(0.0f, smoothAngle, 0.0f));
    }

    /// <summary>
    /// 플레이어 이동 메서드
    /// </summary>
    private void MovePlayer(float moveSpeed)
    {
        Vector3 rotateFowardVector = RotateFowardVector(_rotateAngle);
        Vector3 _currentVelocity = PlayerStateMachine.PlayerController.PlayerRigidbody.velocity;
        _currentVelocity.y = 0f;
        PlayerStateMachine.PlayerController.PlayerRigidbody.AddForce(rotateFowardVector * moveSpeed - _currentVelocity, ForceMode.VelocityChange);
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
