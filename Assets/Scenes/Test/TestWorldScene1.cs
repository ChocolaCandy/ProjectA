using UnityEngine;
using UnityEngine.SceneManagement;

public class TestWorldScene1 : BaseSceneManager
{
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SceneManager.LoadScene("TestWorld");
        }
    }
    protected override void OnSceneLoaded(Scene scene, LoadSceneMode sceneMode)
    {
        Debug.Log("TestWorld1 Load");
    }

    protected override void OnSceneUnLoaded(Scene scene)
    {
        Debug.Log("TestWorld1 UnLoad");
    }

}
