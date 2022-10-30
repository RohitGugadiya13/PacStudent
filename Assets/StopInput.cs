using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopInput : MonoBehaviour
{
    public PacStudentControllerN pacStudentControllerN;
    public Manager manager;
    public ScoreHandler scoreHandler;
    public Tweener[] tweeners;
    public GhostMovement[] ghostMovements;

    void OnEnable()
    {
        pacStudentControllerN.enabled = false;
        manager.enabled = false;
        scoreHandler.enabled = false;
        foreach (Tweener tw in tweeners) tw.enabled = false;
        foreach (GhostMovement gm in ghostMovements) gm.enabled = false;
    }
}
