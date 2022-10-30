using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(AudioSource))]
public class ScoreManager : MonoBehaviour
{
    private Animator animator;

    private void Start()
    {
        GameManager.instance.audioSource.PlayOneShot(GameManager.instance.BGSoundEffect);
        animator = GetComponentInChildren<Animator>();
    }



    private void Update()
    {
        if (GameManager.instance.timerbool)
            PowerTimerFunction();

        CheckVictory();
        if (!GameManager.instance.audioSource.isPlaying && GameManager.instance.levelEnds == false)
            PlayAudioClips(4);

    }

    void OnTriggerEnter2D(Collider2D collider2D)
    {
        if (collider2D.CompareTag("NormalPellet"))
        {
            GameManager.instance.scoreText.text = "Score: " + (GameManager.instance.score += 10).ToString();
            if (GameManager.instance.score > GameManager.instance.highestScoreCount)
                GameManager.instance.highestScore.text = "Highest Score: " + GameManager.instance.score.ToString();
            //CheckVictory();
            Destroy(collider2D.gameObject);
            PlayAudioClips(0);
        }

        else if (collider2D.CompareTag("cherry"))
        {
            GameManager.instance.cherryCount = 0;
            GameManager.instance.cherryImage.SetActive(true);

            GameManager.instance.scoreText.text = "Score: " + (GameManager.instance.score += 100).ToString();
            if (GameManager.instance.score > GameManager.instance.highestScoreCount)
                GameManager.instance.highestScore.text = "Highest Score: " + GameManager.instance.score.ToString();
            //CheckVictory();

            Destroy(collider2D.gameObject);
            PlayAudioClips(0);
        }

        else if (collider2D.CompareTag("PowerPellet"))
        {
            GameManager.instance.timerbool = true;
            GameManager.instance.powwerTimerActive.SetActive(true);
            GameManager.instance.scoreText.text = "Score: " + (GameManager.instance.score += 20).ToString();
            if (GameManager.instance.score > GameManager.instance.highestScoreCount)
                GameManager.instance.highestScore.text = "Highest Score: " + GameManager.instance.score.ToString();
            //CheckVictory();
            GameManager.instance.enemiesCanDoDamage = false;
            foreach (AIEnemy aIEnemy in GameManager.instance.AIEnemies)
            {
                aIEnemy.InScareState();
                PlayAudioClips(5);

            }
            Destroy(collider2D.gameObject);
            PlayAudioClips(1);
        }

        else if (collider2D.CompareTag("Enemy"))
        {
            if (GameManager.instance.enemiesCanDoDamage)
            {
                for (int i = 0; i < GameManager.instance.lives; i++)
                {
                    GameManager.instance.livesIMG[i].SetActive(false);
                }

                GameManager.instance.lives -= 1;

                for (int i = 0; i < GameManager.instance.lives; i++)
                {
                    GameManager.instance.livesIMG[i].SetActive(true);
                }
                if (GameManager.instance.lives == 0)
                {
                    GameManager.instance.defeatPanel.SetActive(true);
                    GameManager.instance.levelEnds = true;
                    Time.timeScale = 0f;
                    PlayAudioClips(2);
                }
                else
                {
                    print("INELSE");
                    //  transform.GetChild(0).gameObject.SetActive(false);
                    //  Invoke("RespawnPlayer", 2f);
                    //  RespawnPlayer();
                    GameManager.instance.instActive();
                    //    ReduceLives();
                    this.GetComponent<CircleCollider2D>().enabled = false;
                    GameManager.instance.deadANim.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);
                    GameManager.instance.deadANim.gameObject.SetActive(true);
                    //GameManager.instance.deadANim.gameObject.transform.position = this.transform.position;
                    Destroy(this.gameObject);

                }

            }
            else
            {
                GameManager.instance.enemies++;
                Destroy(collider2D.gameObject);
                PlayAudioClips(3);

            }
        }
    }

    void ReduceLives()
    {
        GameManager.instance.lives -= 1;
        for (int i = 0; i < GameManager.instance.lives; i++)
        {
            GameManager.instance.livesIMG[i].SetActive(false);
        }
    }
    //void RespawnPlayer()
    //{
    //    print("Respawn");
    //    GameManager.instance.InS();
    //    //transform.position = new Vector3(-0.3f, 0f, 0f);
    //    //transform.GetChild(0).gameObject.SetActive(true);
    //}

    void CheckVictory()
    {
        if (GameManager.instance.palletPoints.childCount <= 0)
        {
            GameManager.instance.victoryPanel.SetActive(true);
            GameManager.instance.levelEnds = true;
            Time.timeScale = 1;
            if (GameManager.instance.timeCount > GameManager.instance.highestTimeCount) PlayerPrefs.SetInt("HighestTime", GameManager.instance.timeCount);
            if (GameManager.instance.score > GameManager.instance.highestScoreCount) PlayerPrefs.SetInt("HighestScore", GameManager.instance.score);
        }
    }


    void PlayAudioClips(int _index)
    {
        switch (_index)
        {
            case 0:
                GameManager.instance.audioSource.PlayOneShot(GameManager.instance.normalPalletAC);
                break;
            case 1:
                GameManager.instance.audioSource.PlayOneShot(GameManager.instance.powerPalletAC);
                break;
            case 2:
                GameManager.instance.audioSource.PlayOneShot(GameManager.instance.enemyKilledPlayerAC);
                break;
            case 3:
                GameManager.instance.audioSource.PlayOneShot(GameManager.instance.playerKilledEnemyAC);
                break;
            case 4:
                GameManager.instance.audioSource.PlayOneShot(GameManager.instance.walkingPac);
                break;
            case 5:
                GameManager.instance.audioSource.PlayOneShot(GameManager.instance.enemyScaredState);
                break;
            case 6:
                GameManager.instance.audioSource.PlayOneShot(GameManager.instance.enemyNormalState);
                break;

        }
    }


    void WatchOutFromEnemiesNow()
    {
        GameManager.instance.enemiesCanDoDamage = true;
    }

    void PowerTimerFunction()
    {
        GameManager.instance.powerTimer -= Time.deltaTime;

        if (GameManager.instance.powerTimer <= 0)
        {
            if (GameManager.instance.normalState == true)
            {
                GameManager.instance.audioSource.PlayOneShot(GameManager.instance.enemyNormalState);
            }
            GameManager.instance.powerTimer = 0;
            GameManager.instance.powwerTimerActive.SetActive(false);
            GameManager.instance.enemiesCanDoDamage = true;
            GameManager.instance.normalState = false;

        }

        GameManager.instance.powerPalletText.text = "Power Ends in : " + GameManager.instance.powerTimer.ToString("0");

    }
}
