using Unity.VisualScripting;
using UnityEngine;

public class BaseStateMachine
{
    public BaseState _currentState;
    public BaseState _pastState;
    private bool _isInit = false; 

    public void Init(BaseState initState)
    {
        if (_isInit)
        {
            if(Utility.IsDebugMode) Debug.Log("Already initialized");
            return;
        }
        if(!ChangeState(initState, false))
        {
            if (Utility.IsDebugMode) Debug.Log("Initialized failed");
            return;
        }
        if (Utility.IsDebugMode) Debug.Log("Initialized");
        _isInit = true;
    }

    public void OnUpdate()
    {
        if(!_isInit)
        {
            if (Utility.IsDebugMode) Debug.Log("No initialized");
            return;
        }
        _currentState.OnUpdate();
        Debug.Log(_currentState);
    }

    public void OnFixUpdate()
    {
        if (!_isInit)
        {
            if (Utility.IsDebugMode) Debug.Log("No initialized");
            return;
        }
        _currentState.OnFixUpdate();
    }

    public bool ChangeState(BaseState State, bool init = true)
    {
        if (!_isInit && init)
        {
            if (Utility.IsDebugMode) Debug.Log("No initialized");
            return false;
        }
        if (_currentState == State || State == null)
            return false;
        _currentState?.OnExit();
        _pastState = _currentState;
        _currentState = State;
        _currentState.OnEnter();
        return true;
    }

    public void OnTriggerEnter(Collider other)
    {
        _currentState.OnTriggerEnter(other);
    }
}
