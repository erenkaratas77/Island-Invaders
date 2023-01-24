using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class FireStone : MonoBehaviour
{
    Transform targetEnemy;
    float timer;
    WeaponManager wM;

    // Start is called before the first frame update
    void Start()
    {
        wM = transform.GetComponent<WeaponManager>();

    }

    // Update is called once per frame
    
    private void OnTriggerEnter(Collider other)
    {
        if ((other.gameObject.tag == "Enemy" || other.gameObject.tag == "Boss") && targetEnemy == null)
        {
            targetEnemy = other.transform;
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if ((other.gameObject.tag == "Enemy" || other.gameObject.tag == "Boss") && wM.isWeaponLocked)
        {
            if (targetEnemy == null)
            {
                targetEnemy = other.transform;
            }
            timer += Time.deltaTime;
            if (timer >= 3)
            {
                timer = 0;
                if (targetEnemy.gameObject.tag == "Enemy") { StartCoroutine(shootEnemy(targetEnemy)); }
                else { StartCoroutine(shootBoss(targetEnemy)); }
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if ((other.gameObject.tag == "Enemy" || other.gameObject.tag == "Boss") && targetEnemy == other.transform)
        {
            targetEnemy = null;
            timer = 0;
        }

    }

    IEnumerator shootEnemy(Transform target)
    {
        Vector3 tPos = target.position;
        tPos.y += .5f;
        wM.transform.GetChild(wM.weoponCurrentLVL).GetChild(2).GetComponent<ParticleSystem>().Play();
        wM.transform.GetChild(wM.weoponCurrentLVL).GetComponentInChildren<Weapon>().GetComponent<MeshRenderer>().enabled = true;
        wM.transform.GetChild(wM.weoponCurrentLVL).GetComponentInChildren<Weapon>().GetComponentInChildren<ParticleSystem>().Play();
        wM.transform.GetChild(wM.weoponCurrentLVL).GetComponentInChildren<Weapon>().transform.DOMove(tPos, 0.4f).SetEase(Ease.Linear);
        yield return new WaitForSeconds(0.45f);

        target.GetComponent<Enemy>().enemyTookDamge(wM.weaponDamages[wM.weoponCurrentLVL-1]); //HASAR VERİYORU DÜŞMANA
        if (target.GetComponent<Enemy>().isDead) { targetEnemy = null; }

        wM.transform.GetChild(wM.weoponCurrentLVL).GetComponentInChildren<Weapon>().GetComponent<MeshRenderer>().enabled = false;
        wM.transform.GetChild(wM.weoponCurrentLVL).GetComponentInChildren<Weapon>().GetComponentInChildren<ParticleSystem>().Stop();
        wM.transform.GetChild(wM.weoponCurrentLVL).GetComponentInChildren<Weapon>().transform.localPosition = Vector3.zero;



    }
    IEnumerator shootBoss(Transform target)
    {
        Vector3 tPos = target.position;
        tPos.y += .5f;
        wM.transform.GetChild(wM.weoponCurrentLVL).GetChild(2).GetComponent<ParticleSystem>().Play();
        wM.transform.GetChild(wM.weoponCurrentLVL).GetComponentInChildren<Weapon>().GetComponent<MeshRenderer>().enabled = true;
        wM.transform.GetChild(wM.weoponCurrentLVL).GetComponentInChildren<Weapon>().GetComponentInChildren<ParticleSystem>().Play();
        wM.transform.GetChild(wM.weoponCurrentLVL).GetComponentInChildren<Weapon>().transform.DOMove(tPos, 0.4f).SetEase(Ease.Linear);
        yield return new WaitForSeconds(0.45f);

        target.GetComponent<Boss>().enemyTookDamge(wM.weaponDamages[wM.weoponCurrentLVL - 1]); //HASAR VERİYORU DÜŞMANA
        if (target.GetComponent<Boss>().isDead) { targetEnemy = null; }

        wM.transform.GetChild(wM.weoponCurrentLVL).GetComponentInChildren<Weapon>().GetComponent<MeshRenderer>().enabled = false;
        wM.transform.GetChild(wM.weoponCurrentLVL).GetComponentInChildren<Weapon>().GetComponentInChildren<ParticleSystem>().Stop();
        wM.transform.GetChild(wM.weoponCurrentLVL).GetComponentInChildren<Weapon>().transform.localPosition = Vector3.zero;



    }

}
