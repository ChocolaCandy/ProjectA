using UnityEngine;

public class BaseStateMachine
{
    public BaseState _currentState; //현재상태
    public BaseState _pastState; //이전상태

    private bool _isInit = false;  //초기화 여부

    /// <summary>
    /// 초기상태 초기화 메서드
    /// </summary>
    /// <param name="initState"></param>
    protected void Init(BaseState initState)
    {
        if (_isInit || initState == null)
            return;
        _currentState = initState;
        _currentState.OnEnter();
        _isInit = true;
    }

    /// <summary>
    /// FixedUpdate문에서의 실행 메서드
    /// </summary>
    public void OnFixUpdate()
    {
        if (!_isInit)
            return;
        _currentState.OnFixUpdate();
    }

    /// <summary>
    /// Update문에서의 실행 메서드
    /// </summary>
    public void OnUpdate()
    {
        if(!_isInit)
            return;
        Debug.Log(_currentState);
        _currentState.OnUpdate();
    }

    /// <summary>
    /// OnTriggerEnter문에서의 실행 메서드
    /// </summary>
    public void OnTrigger_Enter(Collider other)
    {
        _currentState.OnTriggerEnter(other);
    }

    /// <summary>
    /// 상태 변환 메서드
    /// </summary>
    public bool ChangeState(BaseState State)
    {
        if (!_isInit || _currentState == State || State == null)
            return false;
        _currentState.OnExit();
        _pastState = _currentState;
        _currentState = State;
        _currentState.OnEnter();
        return true;
    }

    /// <summary>
    /// 이전상태 변환 메서드
    /// </summary>
    public void ChangePastState()
    {
        ChangeState(_pastState);
    }
}
