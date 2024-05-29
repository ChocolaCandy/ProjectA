using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class UtilityMethod
{
    #region NonExtension Methods
    #endregion

    #region Extension Methods
    //컴포넌트를 자동 생성해서 가져오는 익스텐션 메서드
    public static T GetOrAddComponent<T>(this GameObject gameObject) where T : Component
    {
        T returnComponent = gameObject.GetComponent<T>();
        if (returnComponent == null)
            returnComponent = gameObject.AddComponent<T>();
        return returnComponent;
    }
    #endregion
}
