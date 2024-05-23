using UnityEngine;
using UnityEngine.SceneManagement;

public class TestWorldScene : BaseSceneManager
{
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            SceneManager.LoadScene("TestWorld1");
        }
    }

    protected override void OnSceneLoaded(Scene scene, LoadSceneMode sceneMode)
    {
        Debug.Log("TestWorld Load");
    }

    protected override void OnSceneUnLoaded(Scene scene)
    {
        Debug.Log("TestWorld UnLoad");
    }
}
