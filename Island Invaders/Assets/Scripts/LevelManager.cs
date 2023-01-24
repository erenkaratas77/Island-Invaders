using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    // Start is called before the first frame update
    public RectTransform GameCanvas,StartCanvas;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void startLevel()
    {
        GameCanvas.gameObject.SetActive(true);
        GameManager.Instance.gameStarted = true;
        StartCoroutine(Camera.main.GetComponent<camFollow>().camComing());
        StartCanvas.transform.gameObject.SetActive(false);
    }
}
