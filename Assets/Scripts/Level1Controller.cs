using UnityEngine;

public class Level1Controller : MonoBehaviour
{
    public GameObject helicopter;

    private int stage = 1;

    public GameObject[] popUpTargetsStage1;
    public GameObject slidingdoorStage1;

    public GameObject[] popUpTargetsStage2;
    public GameObject slidingdoorStage2;

    private void OnEnable()
    {
        StartStage0();
        Invoke("StartStage1", 1);
    }

    private void StartStage0()
    {
        helicopter.GetComponent<Helicopter2Fly>().enabled = false;
        //helicopter.GetComponent<HelicopterRocking>().enabled = false;
        ShootingRangeTimer.Instance.SetTimer(0);
    }

    private void StartStage1()
    {
        helicopter.GetComponent<Helicopter2Fly>().enabled = true;
        //helicopter.GetComponent<HelicopterRocking>().enabled = true;
        ShootingRangeTimer.Instance.StartCountup();
        slidingdoorStage1.GetComponent<IDoor>().Open();
        foreach (GameObject p in popUpTargetsStage1)
        {
            p.GetComponent<IPopUpTarget>().Appear();
        }
    }

    private void StartStage2()
    {
        foreach (GameObject p in popUpTargetsStage2)
        {
            p.GetComponent<IPopUpTarget>().Appear();
        }
    }

    private void StartStage3()
    {
        slidingdoorStage2.GetComponent<IDoor>().Open();
    }


    private void EndLevel()
    {
        ShootingRangeTimer.Instance.PauseTimer();
    }

    private void OnAllTargetsDestroyed()
    {
        if (stage == 1)
        {
            stage = 2;
            StartStage2();
        }
        else if (stage == 2)
        {
            stage = 3;
            StartStage3();
        }
        else if (stage == 3)
        {
            EndLevel();
        }
    }
}
