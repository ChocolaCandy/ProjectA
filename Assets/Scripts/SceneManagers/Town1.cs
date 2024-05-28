using UnityEngine;
using UnityEngine.SceneManagement;

public class Town1 : BaseSceneManager
{
    public Transform TempWarpPoint = null;

    protected override void OnSceneLoaded(Scene scene, LoadSceneMode sceneMode)
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (!player)
        {
            player = new GameObject("Player");
            player.AddComponent<DontDestroyOnLoadObject>().SetDontDestroyOnLoad();
            player.transform.SetPositionAndRotation(Vector3.zero, Quaternion.identity);
            player.tag = TagName.Player;
        }
        Transform player3D = player.transform.Find("Player3D");
        if (!player3D)
        {
            GameObject prefab = Resources.Load<GameObject>("Player3D");
            player3D = Instantiate(prefab, TempWarpPoint.position, TempWarpPoint.rotation, player.transform).transform;
            player3D.name = "Player3D";
            player3D.tag = TagName.Player3D;
        }
        player3D.position = TempWarpPoint.position;
        player3D.gameObject.SetActive(true);
    }

    protected override void OnSceneUnLoaded(Scene scene)
    {
        GameObject.FindGameObjectWithTag("Player").transform.Find("Player3D").gameObject.SetActive(false);
    }
}
