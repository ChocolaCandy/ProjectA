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
    /// 씬 로드시 실행될 메서드
    /// </summary>
    private void ActionSceneLoad(Scene scene, LoadSceneMode sceneMode)
    {
        OnSceneLoad();
    }

    /// <summary>
    /// 씬 언로드시 실행될 메서드
    /// </summary>
    private void ActionSceneUnLoad(Scene scene)
    {
        OnSceneUnLoad();
    }

    protected abstract void OnSceneLoad();
    protected abstract void OnSceneUnLoad();
}
