using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BaseData
{
    public bool isThisBaseTriggered, isThisBaseCollapsed;
    public string baseState;

    public BaseData(Base baseSc)
    {
        isThisBaseTriggered = baseSc.isThisBaseTriggered;
        isThisBaseCollapsed = baseSc.isThisBaseCollapsed;
        baseState = baseSc.myState;
    }
}
