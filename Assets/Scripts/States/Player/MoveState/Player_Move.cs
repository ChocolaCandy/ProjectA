using UnityEngine;
using UnityEngine.InputSystem;

public abstract class Player_Move : Player_Base
{
    public Player_Move(PlayerStateMachine stateMachine) : base(stateMachine) { }

    //RoateValue
    private float _rotateTime = 0.02f;
    private float _rotateAngle = 0.0f;

    //SmoothDamp parameter 
    private float _currentVelocity;
    private float _elapsedTime = 0.0f;
    
    //MoveSpeed
    protected float _moveSpeed = 0.0f;

    protected override void SetOnEnter()
    {
        SetSpeed();
        AddJumping();
        AddMoving();
    }

    protected override void SetOnUpdate()
    {
        if (PlayerMoveInput == Vector2.zero)
            ChangeStateToIdle();
    }

    protected override void SetOnFixUpdate()
    {
        if (PlayerMoveInput != Vector2.zero)
            Move();
    }

    protected abstract void SetSpeed();

    /// <summary>
    /// �÷��̾� �̵� ���� �޼���
    /// </summary>
    protected void Move()
    {
        float rotateAngle = Set_rotateAngle();
        if (IsNewAngle(rotateAngle))
            InitValues();
        RotatePlayer();
        MovePlayer();
    }

    #region Move Methods
    /// <summary>
    /// ȸ������ ���ϴ� �޼���(�Է°��� ���� + ī�޶� ����)
    /// </summary>
    private float Set_rotateAngle()
    {
        float rotateAngle = Mathf.Atan2(PlayerMoveInput.x, PlayerMoveInput.y) * Mathf.Rad2Deg;
        if (rotateAngle < 0) rotateAngle += 360;
        rotateAngle += PlayerStateMachine.PlayerController.PlayerCamera.transform.eulerAngles.y;
        if (rotateAngle > 360) rotateAngle -= 360;
        return rotateAngle;
    }

    /// <summary>
    /// ȸ������ �ٸ��� �Ǵ� �� �����ϴ� �޼���
    /// </summary>
    private bool IsNewAngle(float angle)
    {
        if (_rotateAngle == angle)
            return false;
        _rotateAngle = angle;
        return true;
    }

    /// <summary>
    /// ���� �ʱ�ȭ �޼���
    /// </summary>
    private void InitValues()
    {
        _elapsedTime = 0.0f;
    }

    /// <summary>
    /// �÷��̾� ȸ�� �޼���
    /// </summary>
    private void RotatePlayer()
    {
        if (PlayerStateMachine.PlayerController.PlayerRigidbody.transform.eulerAngles.y == _rotateAngle)
            return;
        float currentAngle = PlayerStateMachine.PlayerController.PlayerRigidbody.transform.eulerAngles.y;
        float smoothAngle = Mathf.SmoothDampAngle(currentAngle, _rotateAngle, ref _currentVelocity, _rotateTime - _elapsedTime);
        _elapsedTime += Time.fixedDeltaTime;
        PlayerStateMachine.PlayerController.PlayerRigidbody.MoveRotation(Quaternion.Euler(0.0f, smoothAngle, 0.0f));
    }

    /// <summary>
    /// �÷��̾� �̵� �޼���
    /// </summary>
    private void MovePlayer()
    {
        Vector3 rotateFowardVector = RotateFowardVector();
        Vector3 playerVelocity = PlayerStateMachine.PlayerController.PlayerRigidbody.velocity;
        playerVelocity.y = 0f;
        PlayerStateMachine.PlayerController.PlayerRigidbody.AddForce(rotateFowardVector * _moveSpeed - playerVelocity, ForceMode.VelocityChange);
    }

    /// <summary>
    /// �÷��̾� forward���� ȸ�� �޼���
    /// </summary>
    private Vector3 RotateFowardVector()
    {
        return Quaternion.Euler(0, _rotateAngle, 0) * Vector3.forward;
    }
    #endregion
}
