using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using TMPro;
public class Base : MonoBehaviour
{
    // Start is called before the first frame update
    public int baseMaxMoney, baseStackedMoney, reapirValue, baseMaxHealth;
    public bool isThisBaseCollapsed, isThisBaseTriggered;

    public List<GameObject> baseMonies = new List<GameObject>();
    List<Transform> basePieces = new List<Transform>();
    List<Vector3> basePiecesStartPos = new List<Vector3>();

    public int BaseID;

    public int baseCurrentHealth, loanMoneyForRepair;

    float timer, saveTimer;
    float waitSecond = .5f;
    float sliderValue;


    public string myState;

    private void Awake()
    {
        
        for(int i = 0; i < transform.GetChild(1).childCount-1; i++)
        {
            basePieces.Add(transform.GetChild(1).GetChild(i).transform);
            basePiecesStartPos.Add(transform.GetChild(1).GetChild(i).transform.position);


        }
        baseCurrentHealth = baseMaxHealth;
        loanMoneyForRepair = reapirValue;

        StartCoroutine(loadBase());

    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= waitSecond && baseStackedMoney < baseMaxMoney && !isThisBaseCollapsed && isThisBaseTriggered && GameManager.Instance.gameStarted)
        {
            timer = 0;
            myState = "givingMoney";
            state();
        }

        saveTimer += Time.deltaTime;
        if (saveTimer >= 5 && isThisBaseTriggered)
        {
            saveTimer = 0;
            SaveSystem.SaveBase(this,BaseID);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            ObjectPool.Instance.deactivateEnemy(other.GetComponent<Enemy>());
            baseCurrentHealth -= 20;
            transform.GetChild(4).GetChild(0).GetComponent<Slider>().value = (float)baseCurrentHealth / (float)baseMaxHealth;
            transform.GetChild(4).GetChild(1).GetComponent<TextMeshProUGUI>().text = "%"+(int)(100*(float)baseCurrentHealth / (float)baseMaxHealth);
            if (baseCurrentHealth <= 0 && !isThisBaseCollapsed)
            {
                myState = "baseCollapse";
                state();
            }

        }
        if (other.gameObject.tag == "Boss")
        {
            ObjectPool.Instance.deActivateBoss(other.GetComponent<Boss>());
            baseCurrentHealth -= 20;
            transform.GetChild(4).GetChild(0).GetComponent<Slider>().value = (float)baseCurrentHealth / (float)baseMaxHealth;
            transform.GetChild(4).GetChild(1).GetComponent<TextMeshProUGUI>().text = "%" + (int)(100 * (float)baseCurrentHealth / (float)baseMaxHealth);
            if (baseCurrentHealth <= 0 && !isThisBaseCollapsed)
            {
                myState = "baseCollapse";
                state();
            }
        }
    }

    void state()
    {
        switch (myState)
        {
            case "givingMoney":
                baseMoneySpawner();
                break;
            case "baseCollapse":
                baseCollapse();
                break;

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
            StartCoroutine(collectMoneyFromBaseMoneyArea());
            
        }
    }
    IEnumerator collectMoneyFromBaseMoneyArea( )
    {
        if (baseStackedMoney > 0 && Player.Instance.stackedMoney < GameManager.Instance.capacity)
        {
            GameObject money = baseMonies[baseStackedMoney - 1];
            baseMonies[baseStackedMoney - 1].transform.parent = Player.Instance.MoniesOnBack.transform;
            baseStackedMoney -= 1;
            money.transform.parent = Player.Instance.MoniesOnBack.transform;
            money.transform.DOLocalMove(new Vector3(0, ((float)Player.Instance.stackedMoney / 10) + .01f / 10, 0), 0.5f);
            money.transform.DOLocalRotate(new Vector3(0, -90, 0), 0.5f);
            baseMonies.Remove(money);
            Player.Instance.stackedMoney += 1;
            if (!isThisBaseCollapsed)
            {
                GetComponentInChildren<RectTransform>().GetChild(1).GetComponent<TextMeshPro>().text = baseStackedMoney + "/" + baseMaxMoney;
            }

        }
        else
        {
            if (isThisBaseCollapsed)
            {
                
                if (Player.Instance.stackedMoney >= 1)
                {
                    loanMoneyForRepair -= 1;
                   
                    GameObject money = Player.Instance.MoniesOnBack.transform.GetChild(Player.Instance.stackedMoney - 1).gameObject;
                    Player.Instance.stackedMoney -= 1;
                    money.transform.DOMove(transform.GetChild(0).transform.position, .5f);

                    ObjectPool.Instance.pools[0].pooledObjects.Enqueue(money);
                    money.transform.parent = ObjectPool.Instance.transform.GetChild(0).GetChild(0);

                    yield return new WaitForSeconds(0.5f);

                    money.SetActive(false);
                    GetComponentInChildren<RectTransform>().GetChild(2).GetComponent<TextMeshPro>().text = loanMoneyForRepair.ToString();
                    if (loanMoneyForRepair <= 0)
                    {
                        StartCoroutine(baseRepair());
                    }
                }
            }
        }

        


    }
    public void PlayerGone()
    {
        sliderValue = 0;
        GetComponentInChildren<Slider>().value = sliderValue;

    }
    void baseMoneySpawner()
    {
        GameObject newMoney = ObjectPool.Instance.GetPooledObject(0);
        baseMonies.Add(newMoney);
        newMoney.transform.position = new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.z);
        newMoney.transform.parent = transform.GetChild(2);
        newMoney.GetComponent<Rigidbody>().isKinematic = true;
        newMoney.transform.DOLocalRotate(new Vector3(0, -90, 0), 0.3f);
        newMoney.transform.DOLocalMove(new Vector3(GameManager.Instance.positionsForBaseMonies[baseStackedMoney % 8].x, .1f + (((int)baseStackedMoney / 8) * 0.1f), GameManager.Instance.positionsForBaseMonies[baseStackedMoney % 8].z), 0.5f);
        baseStackedMoney += 1;
        GetComponentInChildren<RectTransform>().GetChild(1).GetComponent<TextMeshPro>().text = baseStackedMoney + "/" + baseMaxMoney;

    }

    public void baseCollapse()
    {
        isThisBaseCollapsed = true;
        transform.GetComponent<MeshRenderer>().enabled = false;
        transform.GetChild(1).gameObject.SetActive(true);
        GetComponentInChildren<RectTransform>().GetChild(1).GetComponent<TextMeshPro>().text = "Repair";
        GetComponentInChildren<RectTransform>().GetChild(2).gameObject.SetActive(true);
        GetComponentInChildren<RectTransform>().GetChild(2).GetComponent<TextMeshPro>().text = loanMoneyForRepair.ToString() + "$";



        foreach (Transform t in basePieces)
        {
            t.GetComponent<Rigidbody>().isKinematic = false;
        }
    }
    public IEnumerator baseRepair()
    {

        isThisBaseCollapsed = false;
        int i = 0;
        baseCurrentHealth = baseMaxHealth;
        GetComponentInChildren<RectTransform>().GetChild(2).gameObject.SetActive(false);
        loanMoneyForRepair = reapirValue;
        transform.GetChild(4).GetChild(0).GetComponent<Slider>().value = (float)baseCurrentHealth / (float)baseMaxHealth;
        transform.GetChild(4).GetChild(1).GetComponent<TextMeshProUGUI>().text = "%" + (int)(100 * (float)baseCurrentHealth / (float)baseMaxHealth);
        foreach (Transform t in basePieces)
        {
            t.GetComponent<Rigidbody>().isKinematic = true;
            t.DOMove(basePiecesStartPos[i], 3);
            t.DOLocalRotate(Vector3.zero, 3);
            i++;
        }
        yield return new WaitForSeconds(3);
        transform.GetComponent<MeshRenderer>().enabled = true;
        transform.GetChild(1).gameObject.SetActive(false);


    }

    IEnumerator loadBase()
    {
        BaseData data = SaveSystem.LoadBase(BaseID);
        if (data != null)
        {
            isThisBaseCollapsed = data.isThisBaseCollapsed;
            isThisBaseTriggered = data.isThisBaseTriggered;
            myState = data.baseState;
            yield return new WaitForSeconds(0.5f);
            state();
        }
    }
}
