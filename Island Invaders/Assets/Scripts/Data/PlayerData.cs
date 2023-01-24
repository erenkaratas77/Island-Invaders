using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    public int swordLVL, speedLVL, capacityLVL;
    public int swordDamage, capacity;
    public float playerSpeed,playerLvl;
    public float[] position;

    public PlayerData()
    {
        swordLVL = GameManager.Instance.swordLVL;
        speedLVL = GameManager.Instance.speedLVL;
        capacityLVL = GameManager.Instance.capacityLVL;
        playerLvl = GameManager.Instance.playerLvl;

        swordDamage = GameManager.Instance.swordDamage;
        capacity = GameManager.Instance.capacity;
        playerSpeed = GameManager.Instance.playerSpeed;

        position = new float[3];
        position[0] = Player.Instance.transform.position.x;
        position[1] = Player.Instance.transform.position.y;
        position[2] = Player.Instance.transform.position.z;



    }
}
