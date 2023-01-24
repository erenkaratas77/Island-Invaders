using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;
public class bridgeTrigger : MonoBehaviour
{
    public GameObject connectedIsland;
    public GameObject[] shortCut;
    public int unlockMoney, bridgeID;
    public bool isThisBridgeOpened;
    int loanMoney;
    float sliderValue;
    float timer,saveTimer;
    // Start is called before the first frame update
    private void Awake()
    {
        loadBridge();
    }
    void Start()
    {
        loanMoney = unlockMoney;
        transform.GetChild(2).GetChild(1).GetComponent<TextMeshPro>().text = unlockMoney.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        saveTimer += Time.deltaTime;
        if (saveTimer >= 5 && isThisBridgeOpened)
        {
            saveTimer = 0;
            SaveSystem.SaveBridge(this, bridgeID);
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
            StartCoroutine(giveMoneyToBridge());
        }
    }
    IEnumerator giveMoneyToBridge()
    {
        
        if (Player.Instance.stackedMoney >= 1)
        {
            loanMoney -= 1;
            transform.GetChild(2).GetChild(1).GetComponent<TextMeshPro>().text = loanMoney.ToString();

            if (loanMoney <= 0)
            {
                StartCoroutine(nextIsland());
            }
            GameObject money = Player.Instance.MoniesOnBack.transform.GetChild(Player.Instance.stackedMoney - 1).gameObject;
            Player.Instance.stackedMoney -= 1;
            money.transform.DOMove(transform.GetChild(2).transform.position, .5f);

            ObjectPool.Instance.pools[0].pooledObjects.Enqueue(money);
            money.transform.parent = ObjectPool.Instance.transform.GetChild(0).GetChild(0);

            yield return new WaitForSeconds(0.5f);

            money.SetActive(false);
        }



    }

    public void PlayerGone()
    {
        sliderValue = 0;
        GetComponentInChildren<Slider>().value = sliderValue;

    }
    IEnumerator nextIsland()
    {
        isThisBridgeOpened = true;
        transform.GetChild(0).transform.DOLocalRotate(Vector3.zero, 3);
        transform.GetChild(1).transform.DOLocalRotate(Vector3.zero, 3);
        transform.GetChild(2).GetComponent<Collider>().enabled = false;//köprü açıldı
        transform.GetChild(2).gameObject.SetActive(false);

        if (shortCut[0] != null)
        {
            for(int i = 0; i < shortCut.Length; i++)
            {
                shortCut[i].transform.GetChild(0).transform.DOLocalRotate(Vector3.zero, 3);
                shortCut[i].transform.GetChild(1).transform.DOLocalRotate(Vector3.zero, 3);
            }
            
        }
        yield return new WaitForSeconds(8);

        connectedIsland.GetComponentInChildren<Base>().isThisBaseTriggered = true;
        foreach (EnemySpawner spw in connectedIsland.GetComponentsInChildren<EnemySpawner>())
        {
            spw.isThisSpawnerTriggered = true;
        }
       
        
    }

    void bridgeState()
    {
        switch (isThisBridgeOpened)
        {
            case true:
                transform.GetChild(0).transform.DOLocalRotate(Vector3.zero, 3);
                transform.GetChild(1).transform.DOLocalRotate(Vector3.zero, 3);
                transform.GetChild(2).GetComponent<Collider>().enabled = false;//köprü açıldı
                transform.GetChild(2).gameObject.SetActive(false);

                if (shortCut[0] != null)
                {
                    for (int i = 0; i < shortCut.Length; i++)
                    {
                        shortCut[i].transform.GetChild(0).transform.DOLocalRotate(Vector3.zero, 3);
                        shortCut[i].transform.GetChild(1).transform.DOLocalRotate(Vector3.zero, 3);
                    }

                }
                foreach (EnemySpawner spw in connectedIsland.GetComponentsInChildren<EnemySpawner>())
                {
                    spw.isThisSpawnerTriggered = true;
                }
                break;
            case false:
                
                break;
        }
    }

    void loadBridge()
    {
        BridgeData data = SaveSystem.LoadBridge(bridgeID);
        if (data != null)
        {
            isThisBridgeOpened = data.isBridgeOpen;
            bridgeState();
        }
    }
    
}
