using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject Pacman;
    public static GameManager instance;

    public Text scoreText, powerPalletText;

    public int targetScoresToAchieve, enemies;
    public GameObject victoryPanel, defeatPanel, powwerTimerActive;
   public float powerTimer = 10;

    public AudioSource audioSource;
   public AudioClip BGSoundEffect, walkingPac, normalPalletAC, powerPalletAC, enemyKilledPlayerAC, playerKilledEnemyAC, enemyScaredState, enemyNormalState;


    public AIEnemy[] AIEnemies;
    public bool timerbool, levelEnds = false;
    public bool normalState = true;
    public int lives = 3;
    public GameObject[] livesIMG;

    public int score=0;

    public bool enemiesCanDoDamage = true;
    private Animator animator;

    public GameObject deadANim;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    private void Start()
    {
        animator = GetComponentInChildren<Animator>();

    }
    public void instActive() {
       // animator.SetBool("IsDead", true);
        StartCoroutine(InvokeIns());
    }
    IEnumerator InvokeIns() {
        yield return new WaitForSeconds(1.9f);
      //  animator.SetBool("IsDead", false);
        InS();
    }
    public void InS() {
        deadANim.SetActive(false);
GameObject go= Instantiate(Pacman, new Vector3(-0.3f, 0, 0), Quaternion.identity);
        go.transform.GetChild(0).gameObject.SetActive(true);
        go.GetComponent<CircleCollider2D>().enabled = true;
    }
}
