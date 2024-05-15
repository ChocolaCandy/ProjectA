using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Utility
{
    #region Fields
    public static bool IsDebugMode = false;
    #endregion

    #region NonExtension Methods
    #endregion

    #region Extension Methods
    //컴포넌트를 자동 생성해서 가져오는 익스텐션 메서드
    public static T AutoGetComponent<T>(this GameObject gameObject) where T : Component
    {
        T tempComponent = null;
        if (gameObject.GetComponent<T>() == null)
        {
            tempComponent = gameObject.AddComponent<T>();
        }
        return tempComponent;
    }
    #endregion
}
