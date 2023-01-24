using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class Player : MonoBehaviour
{
    public static Player Instance;
    public GameObject MoniesOnBack;
    public int stackedMoney;


    public bool amICollectingMoneyFromBaseRN;

    private void Awake()
    {
        Instance = this;
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy" || other.gameObject.tag=="Boss")
        {
            StartCoroutine(swingSword());
        }

        if (other.gameObject.tag == "Money")
        {
            collectMoneyFromGround(other.transform.parent.gameObject);            
        }

        if (other.gameObject.tag == "UpgradeArea")
        {
            GameManager.Instance.upgradePanel.SetBool("PlayerOnUpgradeArea", true);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "MoneyStackArea")
        {
            amICollectingMoneyFromBaseRN = false;
        }
        if (other.gameObject.tag == "UpgradeArea")
        {
            GameManager.Instance.upgradePanel.SetBool("PlayerOnUpgradeArea", false);
        }
        if (other.gameObject.tag == "WeaponUUArea")
        {
            other.GetComponentInParent<WeaponManager>().PlayerGone();
        }
        if (other.gameObject.tag == "bridgeTrigger")
        {
            other.GetComponentInParent<bridgeTrigger>().PlayerGone();
        }
        if (other.gameObject.tag == "MoneyStackArea")
        {
            other.GetComponentInParent<Base>().PlayerGone();
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "WeaponUUArea")
        {
            other.GetComponentInParent<WeaponManager>().PlayerCame();
            
        }
        if (other.gameObject.tag == "bridgeTrigger")
        {
            other.GetComponentInParent<bridgeTrigger>().PlayerCame();
        }
        if (other.gameObject.tag == "MoneyStackArea")
        {
            other.GetComponentInParent<Base>().PlayerCame();
        }
    }



    public IEnumerator swingSword()
    {
        GetComponent<Animator>().SetBool("hit", true);
        yield return new WaitForSeconds(0.2f);
        GetComponent<Animator>().SetBool("hit", false);


        GetComponentInChildren<Sword>().GetComponent<Collider>().enabled = true;
        yield return new WaitForSeconds(.6f);
        GetComponentInChildren<Sword>().GetComponent<Collider>().enabled = false;

    }

    void collectMoneyFromGround(GameObject money)
    {
        if (stackedMoney < GameManager.Instance.capacity)
        {
            money.GetComponent<MoneyTriggerActivate>().enabled = false;
            money.transform.GetChild(0).gameObject.SetActive(false);
            money.GetComponent<Collider>().enabled = false;
            money.GetComponent<Rigidbody>().useGravity = false;
            money.GetComponent<Rigidbody>().isKinematic = true;
            money.transform.parent = MoniesOnBack.transform;
            money.transform.DOLocalMove(new Vector3(0, ((float)stackedMoney / 10) + .01f, 0), 0.5f);
            money.transform.DOLocalRotate(new Vector3(0, -90, 0), 0.5f);
           
            stackedMoney += 1;

        }
       
    }


   

}
