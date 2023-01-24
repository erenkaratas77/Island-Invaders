using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class WeaponData
{
    public int weoponCurrentLVL;
    public bool isWeaponLocked;

    public WeaponData (WeaponManager wm)
    {
        weoponCurrentLVL = wm.weoponCurrentLVL;
        isWeaponLocked = wm.isWeaponLocked;
    }
}
