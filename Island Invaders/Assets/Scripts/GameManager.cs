using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public bool gameStarted;
    public GameObject moneyPref;
    public Animator upgradePanel;
    public int swordLVL, speedLVL, capacityLVL;
    public int swordDamage, capacity;
    public float playerSpeed;
    public Vector3[] positionsForBaseMonies;
    public float playerLvl;
    public TextMeshProUGUI[] playerLvlTexts;
    public Slider playerLvlSlider;
    float timer;
    private void Awake()
    {
        Instance = this;
        LoadPlayer();
    }
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= 5)
        {
            timer = 0;
            SaveSystem.SavePlayer();
        }
        playerLvlTexts[0].text = ((int)playerLvl).ToString();
        playerLvlTexts[1].text = ((int)playerLvl + 1).ToString();
    }

    public void LoadPlayer()
    {
        PlayerData data = SaveSystem.LoadPlayer();
        if (data != null)
        {
            swordLVL = data.swordLVL;
            speedLVL = data.speedLVL;
            capacityLVL = data.capacityLVL;

            swordDamage = data.swordDamage;
            capacity = data.capacity;
            playerSpeed = data.playerSpeed;
            playerLvl = data.playerLvl;

            Vector3 position;
            position.x = data.position[0];
            position.y = data.position[1];
            position.z = data.position[2];
            Player.Instance.transform.position = position;
        }
        

    }

   

   

}
