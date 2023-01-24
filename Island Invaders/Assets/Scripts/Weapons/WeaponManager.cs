using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;
public class WeaponManager : MonoBehaviour
{
    public int[] weaponValues;
    public int[] weaponDamages;
    public bool isWeaponLocked, reachedMaxLVL;
    public int weoponCurrentLVL,loanMoney;
    public int weaponID;
    float sliderValue;
    float timer, saveTimer;


    // Start is called before the first frame update
    private void Awake()
    {
        LoadWeapon();
    }
    void Start()
    {
        loanMoney = weaponValues[0];
        transform.GetChild(4).GetChild(1).GetComponent<TextMeshPro>().text = loanMoney.ToString() + "$";
    }

    // Update is called once per frame
    void Update()
    {
        saveTimer += Time.deltaTime;
        if (saveTimer >= 5 && isWeaponLocked)
        {
            saveTimer = 0;
            SaveSystem.SaveWeapon(this,weaponID);
        }
    }
    public void PlayerCame()
    {
        sliderValue += 0.02f;
        sliderValue = Mathf.Clamp(sliderValue, 0, 1);
        GetComponentInChildren<Slider>().value = sliderValue;

        timer += Time.deltaTime;
        if (sliderValue >= 1 && timer >= 0.1f)
        {
            timer = 0;
            StartCoroutine(giveMoneyToWeapon());
        }
    }
    IEnumerator giveMoneyToWeapon()
    {
        if (!isWeaponLocked)
        {
            transform.GetChild(0).gameObject.SetActive(true);

        }
        if (Player.Instance.stackedMoney >= 1)
        {
            loanMoney -= 1;
            if (loanMoney == 0)
            {
                weoponCurrentLVL += 1;
                weponLevel();
            }
            GameObject money = Player.Instance.MoniesOnBack.transform.GetChild(Player.Instance.stackedMoney - 1).gameObject;
            Player.Instance.stackedMoney -= 1;
            money.transform.DOMove(transform.GetChild(4).transform.position, .5f);

            ObjectPool.Instance.pools[0].pooledObjects.Enqueue(money);
            money.transform.parent = ObjectPool.Instance.transform.GetChild(0).GetChild(0);
            transform.GetChild(4).GetChild(1).GetComponent<TextMeshPro>().text = loanMoney.ToString() + "$";

            yield return new WaitForSeconds(0.5f);

            money.SetActive(false);
        }



    }
    public void PlayerGone()
    {
        sliderValue = 0;
        GetComponentInChildren<Slider>().value = sliderValue;
        transform.GetChild(0).gameObject.SetActive(false);

    }

    

    void weponLevel()
    {
        switch (weoponCurrentLVL)
        {
            case 1:
                isWeaponLocked = true;
                transform.GetChild(0).gameObject.SetActive(false);
                transform.GetChild(1).gameObject.SetActive(true);
                transform.GetChild(2).gameObject.SetActive(false);
                transform.GetChild(3).gameObject.SetActive(false);
                if (transform.GetChild(1).GetComponent<Animator>() != null){transform.GetChild(1).GetComponent<Animator>().speed = 0.5f;}
                transform.GetChild(1).DOPunchScale(new Vector3(0.2f, 0.2f, 0.2f), .5f, 10, 1f);
                loanMoney = weaponValues[1];
                transform.GetChild(4).GetChild(2).GetComponent<TextMeshPro>().text = "Upgrade";
                break;
            case 2:
                transform.GetChild(0).gameObject.SetActive(false);
                transform.GetChild(1).gameObject.SetActive(false);
                transform.GetChild(2).gameObject.SetActive(true);
                transform.GetChild(3).gameObject.SetActive(false);
                if (transform.GetChild(2).GetComponent<Animator>() != null){transform.GetChild(2).GetComponent<Animator>().speed = 0.5f;}
                transform.GetChild(2).DOPunchScale(new Vector3(0.2f, 0.2f, 0.2f), .5f, 10, 1f);
                loanMoney = weaponValues[2];
                break;

            case 3:
                transform.GetChild(0).gameObject.SetActive(false);
                transform.GetChild(1).gameObject.SetActive(false);
                transform.GetChild(2).gameObject.SetActive(false);
                transform.GetChild(3).gameObject.SetActive(true);
                if (transform.GetChild(3).GetComponent<Animator>() != null){transform.GetChild(3).GetComponent<Animator>().speed = 0.5f;}
                transform.GetChild(3).DOPunchScale(new Vector3(0.2f, 0.2f, 0.2f), .5f, 10, 1f);
                transform.GetChild(4).GetComponent<Collider>().enabled = false;//son levele ulaştı silah
                transform.GetComponentInChildren<Slider>().gameObject.SetActive(false);
                transform.GetChild(4).GetChild(2).GetComponent<TextMeshPro>().text = "Max!";
                break;

        }
    }

    public void LoadWeapon()
    {
        WeaponData data = SaveSystem.LoadWeapon(weaponID);
        if (data != null)
        {
            weoponCurrentLVL = data.weoponCurrentLVL;
            isWeaponLocked = data.isWeaponLocked;
            weponLevel();
        }
        

    }
}
