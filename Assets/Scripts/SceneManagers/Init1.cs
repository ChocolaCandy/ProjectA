using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Init1 : BaseSceneManager
{
    public Transform TempWarpPoint = null;
    public GameObject Player = null;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            LoadScene(1);
        }
    }
    private void Start()
    {
        if (Player)
        {
            Player.transform.position = new Vector3(300, 300, 300);
        }
    }

    protected override void OnSceneLoaded(Scene scene, LoadSceneMode sceneMode)
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        if (!Player)
        {
            GameObject prefab = Resources.Load<GameObject>("Player");
            Player = Instantiate(prefab);
            DontDestroyOnLoad(Player);
        }
        Player.name = "Player.Town1";
    }

    protected override void OnSceneUnLoaded(Scene scene)
    {
        Debug.Log("Town1 UnLoad");
    }
}
