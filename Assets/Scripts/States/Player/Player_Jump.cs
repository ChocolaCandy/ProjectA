using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class Player_Jump : PlayerStates
{
    public Player_Jump(PlayerStateMachine stateMachine) : base(stateMachine) { }

    private float _jumpForce = 5.0f;
    private int jumpCount;

    public override void OnEnter()
    {
        jumpCount = 1;
    }

    public override void OnUpdate()
    {
        base.OnUpdate();
    }

    public override void OnExit()
    {

    }

    public override void OnPhysicsUpdate()
    {
        Jump();
    }

    public override void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == LayerMask.NameToLayer("Environment"))
            ChangeIdle();
    }

    public void Jump()
    {
        if (jumpCount == 0)
            return;
        if (PlayerMoveInput != Vector2.zero)
            Rotate();
        _playerController.PlayerRigidbody.velocity = Vector3.zero;
        PlayerStateMachine.Controller.PlayerRigidbody.AddForce(_playerController.transform.up * _jumpForce, ForceMode.VelocityChange);
        jumpCount--;
    }
    private void Rotate()
    {
        float rotateAngle = Mathf.Atan2(PlayerMoveInput.x, PlayerMoveInput.y) * Mathf.Rad2Deg;
        if (rotateAngle < 0) rotateAngle += 360;
        rotateAngle += _playerController.PlayerCamera.transform.eulerAngles.y;
        if (rotateAngle > 360) rotateAngle -= 360;
        _playerController.PlayerRigidbody.MoveRotation(Quaternion.Euler(0.0f, rotateAngle, 0.0f));
    }
}
