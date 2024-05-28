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
            if(UtilityField.IsDebugMode) Debug.Log("Already initialized");
            return;
        }
        if(!ChangeState(initState, false))
        {
            if (UtilityField.IsDebugMode) Debug.Log("Initialized failed");
            return;
        }
        if (UtilityField.IsDebugMode) Debug.Log("Initialized");
        _isInit = true;
    }

    public void OnUpdate()
    {
        if(!_isInit)
        {
            if (UtilityField.IsDebugMode) Debug.Log("No initialized");
            return;
        }
        Debug.Log(_currentState);
        _currentState.OnUpdate();
    }

    public void OnFixUpdate()
    {
        if (!_isInit)
        {
            if (UtilityField.IsDebugMode) Debug.Log("No initialized");
            return;
        }
        _currentState.OnFixUpdate();
    }

    public bool ChangeState(BaseState State, bool init = true)
    {
        if (!_isInit && init)
        {
            if (UtilityField.IsDebugMode) Debug.Log("No initialized");
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
