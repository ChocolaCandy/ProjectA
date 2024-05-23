using UnityEngine;
using UnityEngine.SceneManagement;

public class Town1 : BaseSceneManager
{
    public Transform TempWarpPoint = null;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SceneManager.LoadScene(0);
        }        
    }

    protected override void OnSceneLoaded(Scene scene, LoadSceneMode sceneMode)
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (!player)
        {
            GameObject prefab = Resources.Load<GameObject>("Player");
            player = Instantiate(prefab, TempWarpPoint.position, TempWarpPoint.rotation);
            player.name = "Player";
        }
        player.transform.SetPositionAndRotation(TempWarpPoint.position, TempWarpPoint.rotation);
    }

    protected override void OnSceneUnLoaded(Scene scene)
    {
      
    }
}
