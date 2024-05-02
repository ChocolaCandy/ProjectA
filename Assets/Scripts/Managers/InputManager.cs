using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

[Serializable]
public class InputManager : BaseManager
{
    public InputManager(int managerId) : base(managerId) {}

    public Action KeyBoardInput = null;

    public void CheckKeyBoard()
    {
        KeyBoardInput?.Invoke();
    }
}
