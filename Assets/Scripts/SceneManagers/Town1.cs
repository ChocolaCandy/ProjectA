using UnityEngine;
using UnityEngine.SceneManagement;

public class Town1 : BaseSceneManager
{
    public Transform TempWarpPoint = null;

    protected override void OnSceneLoaded(Scene scene, LoadSceneMode sceneMode)
    {
        GameObject Player = GameObject.FindGameObjectWithTag("Player");
        if (!Player)
        {
            GameObject prefab = Resources.Load<GameObject>("Player");
            Player = Instantiate(prefab, TempWarpPoint.position, TempWarpPoint.rotation);
            Player.name = "Player";
            DontDestroyOnLoad(Player);
        }
    }

    
    protected override void OnSceneUnLoaded(Scene scene)
    {

    }
}
