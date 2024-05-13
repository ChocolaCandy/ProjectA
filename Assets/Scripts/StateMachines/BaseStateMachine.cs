using UnityEngine;

public abstract class BaseStateMachine<T> where T : BaseState
{
    public BaseStateMachine(BaseController controller)
    {
        _controller = controller;
    }

    private BaseController _controller;
    private T _currentState;
    private T _pastState;
    private bool _isInit = false; 

    public virtual void Init(T initState)
    {
        if (_isInit)
        {
            if(Utility.IsDebugMode) Debug.Log("Already initialized");
            return;
        }
        OnChangeState(initState, false);
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

    public virtual void OnChangeState(T changeState, bool init = true)
    {
        if (!_isInit && init)
        {
            if (Utility.IsDebugMode) Debug.Log("No initialized");
            return;
        }
        if (_currentState == changeState)
            return;
        _currentState?.OnExit();
        _pastState = _currentState;
        _currentState = changeState;
        _currentState.OnEnter();
    }
}
