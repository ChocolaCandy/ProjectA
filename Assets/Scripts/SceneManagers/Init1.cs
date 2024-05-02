using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Init1 : SceneBaseManager
{
    public Transform TempWarpPoint = null;
    private string _name = UtilityName.Scene2;
    private void Awake()
    {
        Init(_name);
        SceneManager.sceneLoaded += OnSceneLoaded1;
        PlayerController player = FindObjectOfType<PlayerController>();
        if (player != null)
        {
            player.gameObject.transform.position = TempWarpPoint.position;
        }
    }
    public void OnSceneLoaded1(Scene scene, LoadSceneMode loadSceneMode)
    {
        if (!GameObject.FindWithTag("Player"))
        {
            GameObject a = Resources.Load<GameObject>("Player");
            GameObject prfabs = Instantiate(a);
            prfabs.name = "Player";
        }
    }
}
