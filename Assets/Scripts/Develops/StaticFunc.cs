using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class StaticFunc
{
    #region NonExtension Method
    #endregion

    #region Extension Method
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
