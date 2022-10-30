using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuUI : MonoBehaviour
{
    public Text highestTime, highestScore;
    // Start is called before the first frame update
    void Start()
    {
        highestScore.text = "Highest Score: " + PlayerPrefs.GetInt("HighestScore", 0);
        highestTime.text = PlayerPrefs.GetInt("HighestTime", 0).ToString("0:00:00");
    }

    // Update is called once per frame
    void Update()
    {

    }


    public void LoadFirstScene()
    {
        SceneManager.LoadScene(1);
    }

    public void LoadSecondScene()
    {
        SceneManager.LoadScene(2);
    }
}
