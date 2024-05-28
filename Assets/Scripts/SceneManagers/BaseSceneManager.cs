using System.Collections;
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
        SceneManager.sceneLoaded -= ActionSceneLoaded;
        OnSceneLoaded(scene, sceneMode);
    }

    private void ActionSceneUnLoad(Scene scene)
    {
        SceneManager.sceneUnloaded -= ActionSceneUnLoad;
        OnSceneUnLoaded(scene);
    }

    public void LoadScene(int scene, LoadSceneMode loadSceneMode = LoadSceneMode.Single)
    {
        StartCoroutine(LoadSceneCoroutine(scene, loadSceneMode));
    }

    public void LoadScene(string scene, LoadSceneMode loadSceneMode = LoadSceneMode.Single)
    {
        StartCoroutine(LoadSceneCoroutine(scene, loadSceneMode));
    }

    private IEnumerator LoadSceneCoroutine(int Scene, LoadSceneMode loadSceneMode)
    {
        AsyncOperation Load = SceneManager.LoadSceneAsync(Scene, loadSceneMode);

        while (!Load.isDone)
        {
            yield return null;
        }
    }

    private IEnumerator LoadSceneCoroutine(string Scene, LoadSceneMode loadSceneMode)
    {
        AsyncOperation Load = SceneManager.LoadSceneAsync(Scene, loadSceneMode);

        while (!Load.isDone)
        {
            yield return null;
        }
    }

    protected abstract void OnSceneLoaded(Scene scene, LoadSceneMode sceneMode);
    protected abstract void OnSceneUnLoaded(Scene scene);
}
