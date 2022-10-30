using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class CountDownN : MonoBehaviour
{
    public Text countDown;
    public GameObject manager, gamePlayPanel;
    public AIEnemy[] ghostMovements;
    public ScoreManager scoreManager;
    public InputManager inputManager;
    public Tweener tweener;
    // Start is called before the first frame update
    void Start()
    {
        StartGame();
    }

    private async void StartGame()
    {
        await Task.Delay(1000);
        countDown.text = "2";
        countDown.color = Color.red;
        await Task.Delay(1000);
        countDown.text = "1";
        countDown.color = Color.yellow;
        await Task.Delay(1000);
        countDown.text = "GO";
        countDown.color = Color.green;
        await Task.Delay(500);
        manager.SetActive(true);
        gamePlayPanel.SetActive(true);
        foreach (AIEnemy gM in ghostMovements)
        {
            gM.enabled = true;
        }
        gameObject.SetActive(false);
        scoreManager.enabled = true;
        inputManager.enabled = true;
        tweener.enabled = true;

    }

    // Update is called once per frame
    void Update()
    {

    }
}
