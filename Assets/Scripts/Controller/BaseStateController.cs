using UnityEngine;

public abstract class BaseController : MonoBehaviour
{
    private void Awake()
    {
        SetComponent();
    }

    private void Start()
    {
        Initialize();
    }

    private void Update()
    {
        OnUpdate();
    }

    private void FixedUpdate()
    {
        OnFixUpdate();
    }

    private void OnTriggerEnter(Collider other)
    {
        TriggerEnter(other);
    }

    protected abstract void SetComponent();
    protected abstract void Initialize();
    protected abstract void OnUpdate();
    protected abstract void OnFixUpdate();
    protected abstract void TriggerEnter(Collider other);
}
