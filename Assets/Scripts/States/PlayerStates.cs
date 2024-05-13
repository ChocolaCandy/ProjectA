using UnityEngine;

public class PlayerStates : BaseState
{
    public override void OnEnter()
    {
        Debug.Log($"{GetType().Name} Enter");
    }
    public override void OnUpdate()
    {
        Debug.Log($"{GetType().Name} Update");
    }

    public override void OnExit()
    {
        Debug.Log($"{GetType().Name} Exit");
    }
}

