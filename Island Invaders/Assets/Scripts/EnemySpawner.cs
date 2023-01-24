using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class EnemySpawner : MonoBehaviour
{

    public bool isThisSpawnerTriggered;
    public int islandID;
    float timer = 0;
    float waitSecond = 1;

    int bossTurn;
    void Start()
    {
        waitSecond = ObjectPool.Instance.pools[islandID].spawnRate;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= Random.Range(waitSecond-.5f,waitSecond+.5f) && isThisSpawnerTriggered && GameManager.Instance.gameStarted)
        {
            timer = 0;
            bossTurn += 1;
            if (bossTurn % 5 == 0)
            {
                spawnBoss();
            }
            else
            {
                spawnEnemy();
            }
        }
    }

    
    void spawnEnemy() // acitvate pooled enemies
    {
        GameObject newEnemy = ObjectPool.Instance.GetPooledObject(islandID);
        newEnemy.transform.parent = transform.GetChild(1);
        newEnemy.transform.position = transform.position;
        newEnemy.GetComponent<Enemy>().isDead = false;
        newEnemy.GetComponent<Enemy>().speed = ObjectPool.Instance.pools[islandID].enemySpeed;
        newEnemy.GetComponent<Enemy>().enemyHealth = ObjectPool.Instance.pools[islandID].enemyHealth;
        newEnemy.GetComponent<Enemy>().islandID = islandID;
        newEnemy.GetComponentInChildren<RectTransform>().GetChild(0).gameObject.SetActive(true);
        newEnemy.GetComponentInChildren<Slider>().value = 1;
    }
    void spawnBoss()
    {
        GameObject newEnemy = ObjectPool.Instance.getPooledBoss(islandID);
        newEnemy.transform.parent = transform.GetChild(1);
        newEnemy.transform.position = transform.position;
        newEnemy.GetComponent<Boss>().isDead = false;
        newEnemy.GetComponent<Boss>().speed = ObjectPool.Instance.pools[islandID].enemySpeed;
        newEnemy.GetComponent<Boss>().bossHealth = ObjectPool.Instance.pools[islandID].bossHealth;
        newEnemy.GetComponent<Boss>().islandID = islandID;
        newEnemy.GetComponentInChildren<RectTransform>().GetChild(0).gameObject.SetActive(true);
        newEnemy.GetComponentInChildren<Slider>().value = 1;
    }
}
