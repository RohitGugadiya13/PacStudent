using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopMovement : MonoBehaviour
{
    public InputManager inputManager;
    public ScoreManager scoreManager;
    public Tweener[] tweeners;
    public AIEnemy[] AIEnemies;

    void OnEnable()
    {
        inputManager.enabled = false;
        scoreManager.enabled = false;
        foreach (Tweener tw in tweeners) tw.enabled = false;
        foreach (AIEnemy ai in AIEnemies) ai.enabled = false;
    }
}
