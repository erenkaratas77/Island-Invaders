using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyTriggerActivate : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(activateTrigerer());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator activateTrigerer()
    {
        yield return new WaitForSeconds(.5f);
        transform.GetChild(0).GetComponent<Collider>().enabled = true;
    }
}
