using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public abstract class BaseSceneManager : MonoBehaviour
{
    private static HashSet<string> _pool = new HashSet<string>();

    private void OnEnable()
    {
        SceneManager.sceneLoaded += ActionSceneLoaded;
        SceneManager.sceneUnloaded += ActionSceneUnLoad;
    }

    private void Awake()
    {
        Init();
    }

    private void Init()
    {
        string _sceneName = SceneManager.GetActiveScene().name;
        if (_pool.Contains(_sceneName))
            return;
        _pool.Add(_sceneName);
    }

    private void ActionSceneLoaded(Scene scene, LoadSceneMode sceneMode)
    {
        OnSceneLoaded(scene, sceneMode);
        SceneManager.sceneLoaded -= ActionSceneLoaded;
    }

    private void ActionSceneUnLoad(Scene scene)
    {
        OnSceneUnLoaded(scene);
        SceneManager.sceneUnloaded -= ActionSceneUnLoad;
    }

    protected abstract void OnSceneLoaded(Scene scene, LoadSceneMode sceneMode);
    protected abstract void OnSceneUnLoaded(Scene scene);
}
