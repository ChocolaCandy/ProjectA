using UnityEngine;

public class DontDestroyOnLoadObject : MonoBehaviour
{
    public bool IsDontDestroyOnLoad { get; private set; } = false;

    public void SetDontDestroyOnLoad()
    {
        if (IsDontDestroyOnLoad)
            return;
        IsDontDestroyOnLoad = true;
        DontDestroyOnLoad(this);
    }
}
