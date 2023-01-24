using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class camFollow : MonoBehaviour
{
    public GameObject player;
    public Vector3 offset;
    bool camCame;
    void Start()
    {
        //offset = transform.position - player.transform.position;
    }

    void LateUpdate()
    {
        if (GameManager.Instance.gameStarted && camCame)
        {
            transform.position = player.transform.position + offset;
        }
    }

    public IEnumerator camComing()
    {
        transform.DOMove(player.transform.position + offset, 2f);
        yield return new WaitForSeconds(2.1f);
        camCame = true;
    }
}
