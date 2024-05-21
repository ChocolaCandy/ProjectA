using UnityEngine;

public abstract class BaseController : MonoBehaviour
{
    private void Awake()
    {
        RunAwake();
    }

    private void Start()
    {
        RunStart();
    }

    private void Update()
    {
        RunUpdate();
    }

    private void FixedUpdate()
    {
        RunFixUpdate();
    }

    private void OnTriggerEnter(Collider other)
    {
        RunTriggerEnter(other);
    }

    protected abstract void RunAwake();
    protected abstract void RunStart();
    protected abstract void RunUpdate();
    protected abstract void RunFixUpdate();
    protected abstract void RunTriggerEnter(Collider other);
}
