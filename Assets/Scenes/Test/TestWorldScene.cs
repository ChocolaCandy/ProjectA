using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TestWorldScene : MonoBehaviour
{
    private void Awake()
    {
        SceneManager.sceneLoaded -= a;
    }
    private void a(Scene arg0, LoadSceneMode arg1)
    {
        Debug.Log("2");
    }
}
