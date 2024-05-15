using UnityEngine;

public abstract class BaseStateMachine<T> where T : BaseController
{
    protected BaseStateMachine(T controller)
    {
        Controller = controller;
    }
    public T Controller { get; }
    private BaseState _currentState;
    private BaseState _pastState;
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
