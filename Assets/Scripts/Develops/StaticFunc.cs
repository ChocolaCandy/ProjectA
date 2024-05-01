using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class StaticFunc
{
    #region NonExtension Method
    #endregion

    #region Extension Method
    //������Ʈ�� �ڵ� �����ؼ� �������� �ͽ��ټ� �޼���
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
