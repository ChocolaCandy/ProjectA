using UnityEngine;

public class BaseStateMachine
{
    public BaseState _currentState;
    public BaseState _pastState;
    private bool _isInit = false; 

    public virtual void Init(BaseState initState)
    {
        if (_isInit)
        {
            if(Utility.IsDebugMode) Debug.Log("Already initialized");
            return;
        }
        if(!OnChangeState(initState, false))
        {
            if (Utility.IsDebugMode) Debug.Log("Initialized failed");
            return;
        }
        if (Utility.IsDebugMode) Debug.Log("Initialized");
        _isInit = true;
    }

    public virtual void OnUpdate()
    {
        if(!_isInit)
        {
            if (Utility.IsDebugMode) Debug.Log("No initialized");
            return;
        }
        _currentState.OnUpdate();
        //Debug.Log(_currentState);
    }

    public virtual void OnPhysicsUpdate()
    {
        if (!_isInit)
        {
            if (Utility.IsDebugMode) Debug.Log("No initialized");
            return;
        }
        _currentState.OnPhysicsUpdate();
    }

    public virtual bool OnChangeState(BaseState changeState, bool init = true)
    {
        if (!_isInit && init)
        {
            if (Utility.IsDebugMode) Debug.Log("No initialized");
            return false;
        }
        if (_currentState == changeState || changeState == null)
            return false;
        _currentState?.OnExit();
        _pastState = _currentState;
        _currentState = changeState;
        _currentState.OnEnter();
        return true;
    }

    public virtual void OnTriggerEnter(Collider other)
    {
        _currentState?.OnTriggerEnter(other);
    }
}
