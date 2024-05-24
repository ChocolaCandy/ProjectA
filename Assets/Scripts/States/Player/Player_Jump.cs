using UnityEngine;

public class Player_Jump : Player_Base
{
    public Player_Jump(PlayerStateMachine stateMachine) : base(stateMachine) { }

    private float _jumpForce = 5.0f;
    private int jumpCount;
    protected override void SetOnEnter()
    {
        jumpCount = 1;
        Jump();
    }

    public override void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Environment"))
            ChangeStateToWalk();
    }

    public void Jump()
    {
        if (jumpCount == 0)
            return;
        PlayerStateMachine.PlayerController.PlayerRigidbody.AddForce(PlayerStateMachine.PlayerController.transform.up * _jumpForce, ForceMode.VelocityChange);
        jumpCount--;
    }
}
