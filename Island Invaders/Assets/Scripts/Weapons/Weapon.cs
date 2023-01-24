using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            WeaponManager wM = transform.GetComponentInParent<WeaponManager>();
            other.GetComponent<Enemy>().enemyTookDamge(wM.weaponDamages[wM.weoponCurrentLVL-1]); //HASAR VERİYORU DÜŞMANA
        }
        if (other.gameObject.tag == "Boss")
        {
            WeaponManager wM = transform.GetComponentInParent<WeaponManager>();
            other.GetComponent<Boss>().enemyTookDamge(wM.weaponDamages[wM.weoponCurrentLVL - 1]); //HASAR VERİYORU DÜŞMANA
        }
    }
}
