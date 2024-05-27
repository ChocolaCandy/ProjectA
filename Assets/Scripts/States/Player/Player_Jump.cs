using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player_Jump : Player_Base
{
    public Player_Jump(PlayerStateMachine stateMachine) : base(stateMachine) { }

    private float _jumpForce = 5.0f;
    public int _maxJumpCount = 1;
    private int _jumpCount = 0;
    private bool _doubleJump = false;
    protected override void SetOnEnter()
    {
        if (_maxJumpCount > 1)
        {
            _doubleJump = true;
            Managers.InputManager.PlayerInput.Jump.started += DoubleJump;
        }
        _jumpCount = _maxJumpCount;
        Jump();
    }

    protected override void SetOnExit()
    {
        if (_doubleJump)
            Managers.InputManager.PlayerInput.Jump.started -= DoubleJump;
    }

    public override void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Environment"))
            ChangeStateToPast();
    }

    public void Jump()
    {
        Vector3 playerVelocity = PlayerStateMachine.PlayerController.PlayerRigidbody.velocity;
        PlayerStateMachine.PlayerController.PlayerRigidbody.velocity = new Vector3(playerVelocity.x, 0, playerVelocity.z);
        PlayerStateMachine.PlayerController.PlayerRigidbody.AddForce(PlayerStateMachine.PlayerController.transform.up * _jumpForce, ForceMode.VelocityChange);
        _jumpCount--;
    }

    private void DoubleJump(InputAction.CallbackContext context)
    {
        if (_jumpCount == 0)
            return;
        Jump();
    }
}
