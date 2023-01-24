using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class UpgradeUI : MonoBehaviour
{

    // Start is called before the first frame update
    public TextMeshProUGUI myMoney;
    public TextMeshProUGUI[] lvlTexts, loanTexts;
    
    void Start()
    {
        for (int i = 0; i < 3; i++)
        {
            textChange(i);
        }
    }

    // Update is called once per frame
    void Update()
    {
        myMoney.text = Player.Instance.stackedMoney + "/" + GameManager.Instance.capacity;
    }

    public void swordUp()
    {
        int loan = GameManager.Instance.swordLVL * 10;
        if (loan <= Player.Instance.stackedMoney)
        {
            GameManager.Instance.swordDamage += 10;
            GameManager.Instance.swordLVL += 1;
            getMoney(loan);
            textChange(0);
            if (GameManager.Instance.swordLVL > 6)
            {
                Player.Instance.GetComponentInChildren<Sword>().transform.GetChild(0).gameObject.SetActive(false);
                Player.Instance.GetComponentInChildren<Sword>().transform.GetChild(1).gameObject.SetActive(false);
                Player.Instance.GetComponentInChildren<Sword>().transform.GetChild(2).gameObject.SetActive(true);

            }
            else if (GameManager.Instance.swordLVL > 3)
            {
                Player.Instance.GetComponentInChildren<Sword>().transform.GetChild(0).gameObject.SetActive(false);
                Player.Instance.GetComponentInChildren<Sword>().transform.GetChild(1).gameObject.SetActive(true);
                Player.Instance.GetComponentInChildren<Sword>().transform.GetChild(2).gameObject.SetActive(false);
            }
        }
        
    }
    public void speedUp()
    {
        int loan = GameManager.Instance.speedLVL * 10;
        if (loan <= Player.Instance.stackedMoney)
        {
            GameManager.Instance.playerSpeed += .2f;
            GameManager.Instance.speedLVL += 1;
            getMoney(loan);
            textChange(1);

        }

    }
    public void capacityUp()
    {
        int loan = GameManager.Instance.capacityLVL * 10;
        if (loan <= Player.Instance.stackedMoney)
        {
            GameManager.Instance.capacity += 10;
            GameManager.Instance.capacityLVL += 1;
            getMoney(loan);
            textChange(2);

        }
    }
    
    void getMoney(int times)
    {
        for(int i = 0; i < times; i++)
        {
            GameObject money = Player.Instance.MoniesOnBack.transform.GetChild(Player.Instance.stackedMoney - 1).gameObject;
            Player.Instance.stackedMoney -= 1;
            money.transform.parent = ObjectPool.Instance.transform.GetChild(0).GetChild(0);
            ObjectPool.Instance.pools[0].pooledObjects.Enqueue(money);
            money.SetActive(false);

        }
    }
    void textChange(int which)
    {
        switch (which)
        {
            case 0:
                lvlTexts[0].text = "Weapon <br> Lv." + GameManager.Instance.swordLVL.ToString();
                loanTexts[0].text = (GameManager.Instance.swordLVL * 10).ToString();
                break;
            case 1:
                lvlTexts[1].text = "Speed <br> Lv." + GameManager.Instance.speedLVL.ToString();
                loanTexts[1].text = (GameManager.Instance.speedLVL * 10).ToString();
                break;
            case 2:
                lvlTexts[2].text = "Capacity <br> Lv." + GameManager.Instance.capacityLVL.ToString();
                loanTexts[2].text = (GameManager.Instance.capacityLVL * 10).ToString();
                break;
        }
    }
        
}
