using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tween
{
    public Transform Target;
    private Vector3 StartPos;
    private Vector3 EndPos;
    private float StartTime;
    private float Duration;

    public Tween(Transform target, Vector3 startPos, Vector3 endPos, float startTime, float duration)
    {
        this.Target = target;
        this.StartPos = startPos;
        this.EndPos = endPos;
        this.StartTime = startTime;
        this.Duration = duration;
    }

    public Transform _Target
    {
        get => Target;
    }

    public Vector3 _StartPos
    {
        get => StartPos;
    }

    public Vector3 _EndPos
    {
        get => EndPos;
    }

    public float _StartTime
    {
        get => StartTime;
    }

    public float _Duration
    {
        get => Duration;
    }
}


