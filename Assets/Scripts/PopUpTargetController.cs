using UnityEngine;
using System;

public class PopUpTargetController : MonoBehaviour
{
    float overalltimer;
    float intervalTimer;

    public event Action FiveSecondEvent;

    void Start()
    {
        intervalTimer = Time.time;
        overalltimer = Time.time;
    }

    private void Update()
    {
        if (Time.time > intervalTimer)
        {
            intervalTimer = Time.time + 5;
            if (FiveSecondEvent != null) FiveSecondEvent();
        }
    }
}
