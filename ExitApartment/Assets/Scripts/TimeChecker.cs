using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeChecker
{
    private float duration;
    private float elapsed;

    public TimeChecker(float duration = 0f)
    {
        this.duration = duration;
        this.elapsed = 0f;
    }

    public void Init()
    {
        elapsed = 0f;
    }

    public void Start(float deltaTime)
    {
        elapsed += deltaTime;
    }

    public bool CheckFinish()
    {
        return elapsed >= duration;
    }

    public float RemainTime()
    {
        return Mathf.Max(0, duration - elapsed);
    }

    public float CurTime()
    {
        return elapsed;
    }

    public float NormarlizeCurTime()
    {
        if (duration == 0) return 0;
        return elapsed / duration;
    }
}
