using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneBaseManager : MonoBehaviour
{
    private static GameObject RootObject = null;
    private static HashSet<string> Pool = new HashSet<string>();

    protected void Init(string SceneName)
    {
        if (Pool.Contains(SceneName))
            return;
        if (RootObject == null)
        {
            RootObject = new GameObject($"{UtilityName.SceneManagers}");
            DontDestroyOnLoad(RootObject);
            Pool.Add(RootObject.name);
        }
        gameObject.name = SceneName;
        gameObject.transform.parent = RootObject.transform;
        Pool.Add(SceneName);
    }
}
