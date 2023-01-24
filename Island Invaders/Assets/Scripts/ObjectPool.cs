using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    public static ObjectPool Instance;
    [Serializable] //0 money sonra adalardaki enemyler
    public struct Pool
    {
        public Queue<GameObject> pooledObjects,pooledBoss;
        public GameObject enemy,boss;
        public int poolSize, bossPoolSize;
        public int enemyHealth, bossHealth;
        public float enemySpeed;
        public float spawnRate;

    }

    public Pool[] pools = null;

    private void Awake()
    {
        Instance = this;
        for (int j = 0; j < pools.Length; j++)
        {
            pools[j].pooledObjects = new Queue<GameObject>();
            pools[j].pooledBoss = new Queue<GameObject>();

            for (int i = 0; i < pools[j].poolSize; i++)
            {
                GameObject obj = Instantiate(pools[j].enemy);
                obj.SetActive(false);
                obj.transform.parent = transform.GetChild(j).GetChild(0);
                pools[j].pooledObjects.Enqueue(obj);
            }
            for (int i = 0; i < pools[j].bossPoolSize; i++)
            {
                GameObject obj = Instantiate(pools[j].boss);
                obj.SetActive(false);
                obj.transform.parent = transform.GetChild(j).GetChild(1);
                pools[j].pooledBoss.Enqueue(obj);
            }
        }
    }

   
    public GameObject GetPooledObject(int objectType)
    {
        if (objectType >= pools.Length)
        {
            return null;
        }

        GameObject obj = pools[objectType].pooledObjects.Dequeue();
        obj.SetActive(true);


        return obj;
    }
    
   
    
    public void deactivateEnemy(Enemy enemy)
    {
        enemy.gameObject.SetActive(false);
        pools[enemy.islandID].pooledObjects.Enqueue(enemy.gameObject);
        enemy.GetComponent<Enemy>().distanceTravelled = 0;
        enemy.transform.parent = transform.GetChild(enemy.islandID).GetChild(0);
        enemy.isDead = false;
        enemy.GetComponent<Collider>().enabled = true;
        
        
    }

    public GameObject getPooledBoss(int objectType)
    {
        if (objectType >= pools.Length)
        {
            return null;
        }

        GameObject obj = pools[objectType].pooledBoss.Dequeue();
        obj.SetActive(true);


        return obj;
    }

    public void deActivateBoss(Boss boss)
    {
        boss.gameObject.SetActive(false);
        pools[boss.islandID].pooledBoss.Enqueue(boss.gameObject);
        boss.GetComponent<Boss>().distanceTravelled = 0;
        boss.transform.parent = transform.GetChild(boss.islandID).GetChild(1);
        boss.isDead = false;
        boss.GetComponent<Collider>().enabled = true;
    }

   

}