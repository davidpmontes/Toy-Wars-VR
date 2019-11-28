using System.Collections;
using UnityEngine;

public class TVCamera : MonoBehaviour
{
    public static TVCamera Instance { get; private set; }

    [SerializeField] private TextMesh timeRemaining;
    [SerializeField] private TextMesh enemyCount;

    private float timer;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        Screenoff();
    }

    public void StartTimer()
    {
        StartCoroutine(Timer());
    }

    public void ScreenOn()
    {
        timeRemaining.text = "00:00";
        enemyCount.text = "0/12";
    }

    public void Screenoff()
    {
        timeRemaining.text = "";
        enemyCount.text = "";
    }

    public void TargetHit()
    {
        enemyCount.text = string.Format("{0}/12", EnemyManager.Instance.GetTotalEnemiesDeregistered());
    }

    public void StopTimer()
    {
        StopAllCoroutines();
    }

    IEnumerator Timer()
    {
        timer = Time.time + 20;
        while (timer > Time.time)
        {
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                timer = 0;
                timeRemaining.text = "00:00";
                break;
            }
            timeRemaining.text = string.Format("{0:#.00}", timer - Time.time);
            yield return new WaitForSeconds(Time.deltaTime);
        }
        yield return null;
    }
}
