using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public PlayerAction InputAction { get; private set; }
    public PlayerAction.PlayerActions Actions { get; private set; }
    private void Awake()
    {
        InputAction = new PlayerAction();
        Actions = InputAction.Player;
    }

    private void OnEnable()
    {
        InputAction.Enable();
    }

    private void OnDisable()
    {
        InputAction.Disable();
    }
}
