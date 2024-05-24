
using UnityEngine;

public abstract class BaseState 
{
    public abstract void OnEnter();
    public abstract void OnUpdate();
    public abstract void OnFixUpdate();
    public abstract void OnExit();
    public virtual void OnTriggerEnter(Collider other) {}
}
