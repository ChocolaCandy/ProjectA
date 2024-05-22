using System;

[Serializable]
public class InputManager : BaseManager
{
    public InputManager(int managerId) : base(managerId) {}

    private MainInputAction InputActions;
    public MainInputAction.PlayerActions PlayerInput { get; private set; }
    public MainInputAction.CameraActions CameraInput { get; private set; }

    public void Init()
    {
        if (InputActions != null)
            return;
        InputActions = new MainInputAction();
        PlayerInput = InputActions.Player;
        CameraInput = InputActions.Camera;
    }

    public void InputActionEnable()
    {
        InputActions.Enable();
    }

    public void InputActionDisable()
    {
        InputActions.Disable();
    }
}
