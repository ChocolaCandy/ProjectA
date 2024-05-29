using UnityEngine;
using UnityEngine.SceneManagement;

public class TestWorldScene1 : MonoBehaviour
{
    private void Awake()
    {
        SceneManager.sceneLoaded += a;
    }

    private void a(Scene arg0, LoadSceneMode arg1)
    {
        Debug.Log("2");
    }
}
