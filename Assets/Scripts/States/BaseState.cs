using UnityEngine;

public abstract class BaseState 
{
    public abstract void OnEnter();
    public abstract void OnUpdate();
    public abstract void OnPhysicsUpdate();
    public abstract void OnExit();
    public abstract void OnTriggerEnter(Collider other);
}
