using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BridgeData
{
    public bool isBridgeOpen;

    public BridgeData(bridgeTrigger bridge)
    {
        isBridgeOpen = bridge.isThisBridgeOpened;
    }
}
