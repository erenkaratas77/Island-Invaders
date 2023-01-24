using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation;
using UnityEngine.UI;

public class Boss : MonoBehaviour
{
    // Start is called before the first frame update
    PathCreator path;
    public float distanceTravelled;

    public float speed;
    public bool isDead;
    public int islandID, bossHealth;
    int firsHealth;

    void Start()
    {
        path = GetComponentInParent<EnemySpawner>().GetComponentInChildren<PathCreator>();
        firsHealth = bossHealth;
        GetComponentInChildren<Slider>().value = 1;

    }

    // Update is called once per frame
    void Update()
    {
        if (!isDead)
        {
            distanceTravelled += speed * Time.deltaTime;
            transform.position = path.path.GetPointAtDistance(distanceTravelled);
            transform.rotation = path.path.GetRotationAtDistance(distanceTravelled);
        }

    }
    private void LateUpdate()
    {
        if (!isDead)
        {
            GetComponentInChildren<Slider>().transform.LookAt(Camera.main.transform.position);
        }
    }
    public IEnumerator killedEnemy()
    {
        GetComponent<Collider>().enabled = false;
        isDead = true;

        GetComponent<Animator>().SetBool("isDead", true);

        GameObject newMoney = ObjectPool.Instance.GetPooledObject(0); //parayı çekiyor
        newMoney.transform.position = new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.z);
        newMoney.transform.parent = GameManager.Instance.transform.GetChild(0); //MoniesOnGround
        newMoney.AddComponent<MoneyTriggerActivate>(); //yere düşen parayı toplamak için trigger'ı açıyor bu script

        yield return new WaitForSeconds(4);

        ObjectPool.Instance.deActivateBoss(this);
    }

    public void enemyTookDamge(int damage)
    {
        bossHealth -= damage;
        GetComponentInChildren<Slider>().value = (float)bossHealth / (float)firsHealth;
        if (bossHealth <= 0)
        {
            isDead = true;
            StartCoroutine(killedEnemy());
            GetComponentInChildren<Slider>().gameObject.SetActive(false);
        }
    }


}
