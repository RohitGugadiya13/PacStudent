using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Manager : MonoBehaviour
{
    public GameObject player, enemy;
    public static Manager instance;

    public Text scoreText, highScoreText, powerPalletText, timer, highestTime;
    public GameObject cherryImage;
    public GameObject victoryPanel, defeatPanel, powwerTimerActive;
    public float powerTimer = 10;

    public AudioSource audioSource;
    public AudioClip BGSoundEffect, walkingPac, normalPalletAC, powerPalletAC, enemyKilledPlayerAC, playerKilledEnemyAC, enemyScaredState, enemyNormalState;


    public GhostMovement[] ghost;
    public bool timerbool, levelEnds = false;
    public bool normalState = true;
    public int lives = 3;
    public GameObject[] livesIMG;

    public int score, highestScore = 0;

    public bool enemiesCanDoDamage = true;
    private Animator animator;

    public GameObject deadANim;
    public Transform pallets;
    int timeCount = 1;
    int highestTimeCount = 0;

    [Header("Cherry")]
    public Transform wayPointers;
    public GameObject cherryPrefab;
    public int cherryCount = 0;

    [Header("Game Manger")]
    public PacStudentControllerN pacStudentControllerN;
    public GhostMovement[] ghostMovements;
    bool isGameOver = false;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        highestScore = PlayerPrefs.GetInt("HighestScore", 0);
        highScoreText.text = "Highest Score: " + highestScore.ToString();


        highestTimeCount = PlayerPrefs.GetInt("HighestTime", 0);
        highestTime.text = highestTimeCount.ToString("0:00:00");

        InvokeRepeating(nameof(InstantiateCherry), 10f, 10f);
        InvokeRepeating(nameof(StartTime), 1f, 1f);
    }

    private void StartTime()
    {
        timer.text = timeCount.ToString("0:00:00");
        if (!isGameOver)
            timeCount++;

        if (timeCount > highestTimeCount) highestTime.text = timeCount.ToString("0:00:00");
    }



    // Update is called once per frame
    void Update()
    {
    }

    public void CheckDefeatOrReinstantiate() => Invoke(nameof(ReinstantiatePlayerOrDefeat), 1f);
    public void ReinstantiatePlayerOrDefeat()
    {
        print(" REINSTI PLAYER");
        if (lives > 0)
        {
            Instantiate(player, new Vector3(0, 4.25f, 0f), Quaternion.identity);
            lives--;
            livesIMG[lives].SetActive(false);
        }
        else
        {
            defeatPanel.SetActive(true);
            GameOver();
            if (timeCount > highestTimeCount) PlayerPrefs.SetInt("HighestTime", timeCount);
            if (score > highestScore) PlayerPrefs.SetInt("HighestScore", score);
        }
    }

    private void GameOver()
    {
        pacStudentControllerN.enabled = false;
        foreach (GhostMovement gh in ghostMovements) gh.enabled = false;
        isGameOver = true;
    }

    internal void NormalPallet()
    {
        scoreText.text = "Score: " + (score += 10).ToString();
        CheckVictory();
    }

    internal void PowerPalletAvailed()
    {
        timerbool = true;
        powwerTimerActive.SetActive(true);
        scoreText.text = "Score: " + (score = score + 20).ToString();
        enemiesCanDoDamage = false;
        StartPowerTimer();
        foreach (GhostMovement ghostMovement in ghost)
        {
            ghostMovement.InScareState();
        }
        CheckVictory();
    }
    async void StartPowerTimer()
    {
        int i = 10;
        while (i > 0)
        {
            await Task.Delay(1000);
            powerPalletText.text = i.ToString("0:00:00");
            i--;
        }

    }

    internal async void ReinstantiateEnemy(GameObject gameObject)
    {
        gameObject.SetActive(false);
        await Task.Delay(1500);
        gameObject.transform.position = new Vector3(2.5f, 8f, 0);
        gameObject.SetActive(true);

    }

    internal void Cherry()
    {
        score += 100;
        cherryCount = 0;
        cherryImage.SetActive(true);
        scoreText.text = score.ToString();
    }

    internal void CheckVictory()
    {
        if (pallets.childCount <= 0)
        {
            victoryPanel.SetActive(true);
        }
    }

    public void LoadMenu()
    {
        SceneManager.LoadScene(0);
    }

    private void InstantiateCherry()
    {
        Instantiate(cherryPrefab, new Vector3(8, UnityEngine.Random.Range(-7, 7), 0), Quaternion.identity);
    }
    public void PlayAgain()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
