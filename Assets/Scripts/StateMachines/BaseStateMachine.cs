using UnityEngine;

public class BaseStateMachine
{
    public BaseState _currentState; //�������
    public BaseState _pastState; //��������

    private bool _isInit = false;  //�ʱ�ȭ ����

    /// <summary>
    /// �ʱ���� �ʱ�ȭ �޼���
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
    /// FixedUpdate�������� ���� �޼���
    /// </summary>
    public void OnFixUpdate()
    {
        if (!_isInit)
            return;
        _currentState.OnFixUpdate();
    }

    /// <summary>
    /// Update�������� ���� �޼���
    /// </summary>
    public void OnUpdate()
    {
        if(!_isInit)
            return;
        Debug.Log(_currentState);
        _currentState.OnUpdate();
    }

    /// <summary>
    /// OnTriggerEnter�������� ���� �޼���
    /// </summary>
    public void OnTrigger_Enter(Collider other)
    {
        _currentState.OnTriggerEnter(other);
    }

    /// <summary>
    /// ���� ��ȯ �޼���
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
    /// �������� ��ȯ �޼���
    /// </summary>
    public void ChangePastState()
    {
        ChangeState(_pastState);
    }
}
