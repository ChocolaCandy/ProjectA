using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseManager
{
    [SerializeField]
    private int ManagerID = int.MaxValue;
    protected BaseManager(int managerId) 
    { 
        if (ManagerID == managerId) return;
        ManagerID = managerId; 
    }
}
