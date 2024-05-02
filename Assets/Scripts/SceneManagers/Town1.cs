using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Town1 : SceneBaseManager
{
    private string _name = UtilityName.Town1_SceneManager;
    public Transform CreatePoint = null;
    private void Awake()
    {
        Init(_name);
        SceneManager.sceneLoaded += OnSceneLoaded;
        PlayerController player = FindObjectOfType<PlayerController>();
        if (player != null)
        {
            player.gameObject.transform.position = CreatePoint.position;
        }
    }

    public void OnSceneLoaded(Scene scene, LoadSceneMode loadSceneMode)
    {
        if (!GameObject.FindWithTag("Player"))
        {
            GameObject a = Resources.Load<GameObject>("Player");
            GameObject prfabs = Instantiate(a, CreatePoint.position, Quaternion.identity);
            prfabs.name = "Player";
        }
    }

}
