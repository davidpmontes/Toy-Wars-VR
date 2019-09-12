using TMPro;
using UnityEngine;
using System;
using System.Collections;

public class ShootingRangeTimer : MonoBehaviour
{
    private float timer;

    public TextMeshProUGUI tm;

    public event Action TimerExpiredEvent;

    public static ShootingRangeTimer instance;
    public static ShootingRangeTimer Instance { get { return instance; } }

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    public void HideTimer()
    {
        tm.faceColor = new Color32(0, 1, 0, 0);
    }

    public void RevealTimer()
    {
        tm.faceColor = new Color32(0, 1, 0, 0);
    }

    public void SetTimer(int num)
    {
        timer = num;
        tm.text = timer.ToString("00.00");
    }

    public void StartCountdown()
    {
        tm.faceColor = Color.green;
        StartCoroutine("Countdown");
    }

    public void StartCountup()
    {
        tm.faceColor = Color.green;
        StartCoroutine("Countup");
    }

    public void PauseTimer()
    {
        StopAllCoroutines();
    }

    private IEnumerator Countup()
    {
        while (true)
        {
            timer += Time.deltaTime;
            tm.text = tm.text = timer.ToString("00.00");
            yield return null;
        }
    }

    private IEnumerator Countdown()
    {
        while (timer > 0)
        {
            timer -= Time.deltaTime;
            tm.text = tm.text = timer.ToString("00.00");
            yield return null;
        }
        tm.text = "00.00";
        tm.faceColor = Color.red;
        if (TimerExpiredEvent != null)
            TimerExpiredEvent();
    }
}
