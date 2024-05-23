using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Init1 : BaseSceneManager
{
    public Transform TempWarpPoint = null;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            SceneManager.LoadScene(1);
        }
    }

    protected override void OnSceneLoaded(Scene scene, LoadSceneMode sceneMode)
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (!player)
        {
            GameObject prefab = Resources.Load<GameObject>("Player");
            player = Instantiate(prefab);
            player.name = "Player";
        }
        player.transform.SetPositionAndRotation(TempWarpPoint.position, TempWarpPoint.rotation);
    }

    protected override void OnSceneUnLoaded(Scene scene)
    {
        
    }
}
