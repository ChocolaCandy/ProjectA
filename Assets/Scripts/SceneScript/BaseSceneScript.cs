using UnityEngine;
using UnityEngine.SceneManagement;

public abstract class BaseSceneScript : MonoBehaviour
{
    private void Awake()
    {
        SceneManager.sceneLoaded += ActionSceneLoad;
        SceneManager.sceneUnloaded += ActionSceneUnLoad;
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= ActionSceneLoad;
        SceneManager.sceneUnloaded -= ActionSceneUnLoad;
    }

    /// <summary>
    /// �� �ε�� ����� �޼���
    /// </summary>
    private void ActionSceneLoad(Scene scene, LoadSceneMode sceneMode)
    {
        OnSceneLoad();
    }

    /// <summary>
    /// �� ��ε�� ����� �޼���
    /// </summary>
    private void ActionSceneUnLoad(Scene scene)
    {
        OnSceneUnLoad();
    }

    protected abstract void OnSceneLoad();
    protected abstract void OnSceneUnLoad();
}
