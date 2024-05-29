using UnityEngine;
using UnityEngine.SceneManagement;

public class Town : MonoBehaviour
{
    public Transform TempWarpPoint = null;

    private void Awake()
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
    }
}
