using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class UtilityMethod
{
    #region NonExtension Methods
    #endregion

    #region Extension Methods
    //������Ʈ�� �ڵ� �����ؼ� �������� �ͽ��ټ� �޼���
    public static T GetOrAddComponent<T>(this GameObject gameObject) where T : Component
    {
        T returnComponent = gameObject.GetComponent<T>();
        if (returnComponent == null)
            returnComponent = gameObject.AddComponent<T>();
        return returnComponent;
    }
    #endregion
}
