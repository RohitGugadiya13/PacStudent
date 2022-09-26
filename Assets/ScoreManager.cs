using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(AudioSource))]
public class ScoreManager : MonoBehaviour
{

    [SerializeField] Text scoreText, powerPalletText;

    [SerializeField] int targetScoresToAchieve, enemies;
    [SerializeField] GameObject victoryPanel, defeatPanel, powwerTimerActive;
    float powerTimer = 10;

    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip BGSoundEffect, walkingPac, normalPalletAC, powerPalletAC, enemyKilledPlayerAC, playerKilledEnemyAC, enemyScaredState, enemyNormalState;


    [SerializeField] AIEnemy[] AIEnemies;
    bool timerbool, levelEnds=false;
    bool normalState = true;
    private void Start()
    {
        audioSource.PlayOneShot(BGSoundEffect);
    }

    int score;

    private void Update()
    {
        if(timerbool)
        PowerTimerFunction();

        CheckVictory();
        if(!audioSource.isPlaying && levelEnds==false)
            PlayAudioClips(4);

    }
    [SerializeField] bool enemiesCanDoDamage = true;
    void OnTriggerEnter2D(Collider2D collider2D)
    {
        if (collider2D.CompareTag("NormalPellet"))
        {
            scoreText.text = (++score).ToString();
            //CheckVictory();
            Destroy(collider2D.gameObject);
            PlayAudioClips(0);
        }

        else if (collider2D.CompareTag("PowerPellet"))
        {
            timerbool = true;
            powwerTimerActive.SetActive(true);
            scoreText.text = (++score).ToString();
            //CheckVictory();
            enemiesCanDoDamage = false;
            foreach (AIEnemy aIEnemy in AIEnemies)
            {
                aIEnemy.InScareState();
                PlayAudioClips(5);
                
            }
            Destroy(collider2D.gameObject);
            PlayAudioClips(1);
        }

        else if (collider2D.CompareTag("Enemy"))
        {
            if (enemiesCanDoDamage)
            {
                defeatPanel.SetActive(true);
                levelEnds = true;
                Time.timeScale = 0f;
                PlayAudioClips(2);

            }
            else
            {
                enemies++;
                Destroy(collider2D.gameObject);
                PlayAudioClips(3);

            }
        }
    }
   


    void CheckVictory()
    {
        if (score >= targetScoresToAchieve || enemies == 4)
        {
            victoryPanel.SetActive(true);
            levelEnds = true;
            Time.timeScale = 1;
        }
    }


    void PlayAudioClips(int _index)
    {
        switch (_index)
        {
            case 0:
                audioSource.PlayOneShot(normalPalletAC);
                break;
            case 1:
                audioSource.PlayOneShot(powerPalletAC);
                break;
            case 2:
                audioSource.PlayOneShot(enemyKilledPlayerAC);
                break;
            case 3:
                audioSource.PlayOneShot(playerKilledEnemyAC);
                break;
            case 4:
                audioSource.PlayOneShot(walkingPac);
                break;
            case 5:
                audioSource.PlayOneShot(enemyScaredState);
                break;
            case 6:
                audioSource.PlayOneShot(enemyNormalState);
                break;

        }
    }
    
  
    void WatchOutFromEnemiesNow()
    {
        enemiesCanDoDamage = true;
    }

    void PowerTimerFunction()
    {
        powerTimer -= Time.deltaTime;

        if (powerTimer <= 0)
        {
            if (normalState == true)
            {
                audioSource.PlayOneShot(enemyNormalState);
            }
            powerTimer = 0;
            powwerTimerActive.SetActive(false);
            enemiesCanDoDamage = true;
            normalState = false;

        }
        
        powerPalletText.text = "Power Ends in : " + powerTimer.ToString("0");
        
    }
}
