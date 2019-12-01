using System.Collections;
using UnityEngine;

public class Level1Manager : MonoBehaviour, ILevelManager
{
    public static Level1Manager Instance { get; private set; }

    public int state;

    [SerializeField] GameObject BedroomWindow = default;

    [SerializeField] GameObject ZeppelinSpawner = default;

    [SerializeField] GameObject popUpTargetEnemySpawner1 = default;
    [SerializeField] GameObject popUpTargetEnemySpawner2 = default;
    [SerializeField] GameObject popUpTargetEnemySpawner3 = default;

    [SerializeField] GameObject attackHelicopterEnemySpawnerDolly1 = default;
    [SerializeField] GameObject attackHelicopterEnemySpawnerDolly1_1 = default;
    [SerializeField] GameObject attackHelicopterEnemySpawnerDolly2 = default;
    [SerializeField] GameObject attackHelicopterEnemySpawnerDolly2_1 = default;
    [SerializeField] GameObject attackHelicopterEnemySpawnerDolly3 = default;

    [SerializeField] GameObject spitfireEnemySpawnerDolly1 = default;
    [SerializeField] GameObject spitfireEnemySpawnerDolly2 = default;
    [SerializeField] GameObject attackHelicopterEnemySpawnerDolly4 = default;
    [SerializeField] GameObject attackHelicopterEnemySpawnerDolly5 = default;
    [SerializeField] GameObject attackHelicopterEnemySpawnerDolly6 = default;

    [SerializeField] AudioClip[] sound_effects = default;

    [SerializeField] GameObject playerStatistics = default;
    [SerializeField] GameObject thanksForPlayingOurDemo = default;

    [SerializeField] AudioClip[] NarrationSequences1 = default;
    [SerializeField] AudioClip[] NarrationSequences1_1 = default;


    [SerializeField] AudioClip PrettyEasyWhenTheyDontShootBack = default;
    [SerializeField] AudioClip YouveGotSomeSkills = default;
    [SerializeField] AudioClip YoureDownToTheFinalFour = default;

    [SerializeField] AudioClip AngelsAndMinistersOfGraceDefendUs = default;
    [SerializeField] AudioClip NotBadRecruit = default;
    [SerializeField] AudioClip YouMustPlayALotOfFortnite = default;

    [SerializeField] AudioClip[] NarrationSequences2 = default;
    [SerializeField] AudioClip[] NarrationSequences3 = default;
    [SerializeField] AudioClip[] NarrationSequences4 = default;
    [SerializeField] AudioClip[] NarrationSequences5 = default;

    [SerializeField] AudioClip BGM_PopUpTargets = default;
    [SerializeField] AudioClip BGM_Action = default;
    [SerializeField] AudioClip BGM_Boss = default;
    [SerializeField] AudioClip BGM_Win = default;

    [SerializeField] AudioClip BaseWarning = default;


    private AudioManager audioManager;

    public readonly float POPUPTIMER_TIME_LIMIT = 30;
    private float PopUpTargetEndTime;

    private void Awake()
    {
        Instance = this;
        audioManager = AudioManager.GetAudioManager();
    }

    void Start()
    {
        QualitySettings.shadowDistance = 450;
        UpdateState();
    }

    public void GetSoundEffects(out AudioClip[] fx)
    {
        fx = sound_effects;
    }

    private void PopUpTargetTimerNotification()
    {
        if (state >= 6)
            return;

        GotoState(6, 0);
    }

    private void NarrateSequenceAndNextState(AudioClip[] clips)
    {
        StartCoroutine(NarrateSequenceAndNextStateCR(clips));
    }

    IEnumerator NarrateSequenceAndNextStateCR(AudioClip[] clips)
    {
        for (int i = 0; i < clips.Length; i++)
        {
            audioManager.PlayNarration(clips[i]);
            yield return new WaitForSeconds(clips[i].length);
        }
        NextState(0);
    }

    public void UpdateState()
    {
        if (state == -1)
        {
            NextState(1);
            audioManager.ChangeBGM(BGM_PopUpTargets);
            audioManager.StartBGM();
        }
        else if (state == 0) //Opening scene, audio introduction
        {
            NarrateSequenceAndNextState(NarrationSequences1);
        }
        else if (state == 1) //Opening scene, audio introduction
        {
            TVCamera.Instance.ScreenOn();
            NarrateSequenceAndNextState(NarrationSequences1_1);
        }
        else if (state == 2) //pop up first set of 4 targets
        {
            PopUpTargetEndTime = Time.time + POPUPTIMER_TIME_LIMIT;
            TVCamera.Instance.StartTimer();
            Invoke("PopUpTargetTimerNotification", POPUPTIMER_TIME_LIMIT);

            popUpTargetEnemySpawner1.SetActive(true);
            NextState(0);
        }
        else if (state == 3) //pop up second set of 4 targets
        {
            if (EnemyManager.Instance.GetTotalEnemiesDeregistered() == 4)
            {
                popUpTargetEnemySpawner2.SetActive(true);
                NextState(0);
            }
        }
        else if (state == 4) //pop up third set of 4 targets
        {
            if (EnemyManager.Instance.GetTotalEnemiesDeregistered() == 8)
            {
                popUpTargetEnemySpawner3.SetActive(true);
                NextState(0);
            }
        }
        else if (state == 5) //All targets defeated or time expires
        {
            if (EnemyManager.Instance.GetTotalEnemiesDeregistered() == 12)
            {
                TVCamera.Instance.FreezeTimer();
                NextState(0);
            }
        }
        else if (state == 6) //Time fail
        {
            CancelInvoke("PopUpTargetTimerNotification");
            popUpTargetEnemySpawner1.SetActive(false);
            popUpTargetEnemySpawner2.SetActive(false);
            popUpTargetEnemySpawner3.SetActive(false);

            if (EnemyManager.Instance.GetTotalEnemiesDeregistered() <= 4)
            {
                audioManager.PlayNarration(AngelsAndMinistersOfGraceDefendUs);
                NextState(AngelsAndMinistersOfGraceDefendUs.length + 1);
            }
            else if (EnemyManager.Instance.GetTotalEnemiesDeregistered() <= 8)
            {
                audioManager.PlayNarration(NotBadRecruit);
                NextState(NotBadRecruit.length + 1);
            }
            else
            {
                audioManager.PlayNarration(YouMustPlayALotOfFortnite);
                NextState(YouMustPlayALotOfFortnite.length + 1);
            }
            
            EnemyManager.Instance.ResetTotalEnemiesDeregistered();
        }
        else if (state == 7)
        {
            NextState(1);
            TVCamera.Instance.Screenoff();
        }
        else if (state == 8)
        {
            audioManager.PlayNarration(BaseWarning);
            NextState(5);
        }
        else if (state == 9) // Wave #1: Narration
        {
            NarrateSequenceAndNextState(NarrationSequences2);
            audioManager.ChangeBGM(BGM_Action);
            audioManager.StartBGM();
        }
        else if (state == 10) // Wave #1: Attack Helicopters appear
        {
            ActivateSpawner(attackHelicopterEnemySpawnerDolly1, 0);
            ActivateSpawner(attackHelicopterEnemySpawnerDolly1_1, 2);

            ActivateSpawner(attackHelicopterEnemySpawnerDolly2, 7);
            ActivateSpawner(attackHelicopterEnemySpawnerDolly3, 11);

            ActivateSpawner(attackHelicopterEnemySpawnerDolly2_1, 12);
            NextState(0);
        }
        else if (state == 11)
        {
            if (EnemyManager.Instance.GetTotalEnemiesDeregistered() == 8)
            {
                NextState(0);
            }
        }
        else if (state == 12)
        {
            EnemyManager.Instance.ResetTotalEnemiesDeregistered();
            NarrateSequenceAndNextState(NarrationSequences3); //"Sir the enemy has regrouped..."
        }
        else if (state == 13) // Wave #2: Spitfires appear
        {
            ActivateSpawner(spitfireEnemySpawnerDolly1, 0);
            ActivateSpawner(spitfireEnemySpawnerDolly2, 1.5f);
            ActivateSpawner(attackHelicopterEnemySpawnerDolly4, 23);
            ActivateSpawner(attackHelicopterEnemySpawnerDolly5, 21);
            ActivateSpawner(attackHelicopterEnemySpawnerDolly6, 18);
            NextState(0);
        }
        else if (state == 14) // Wave #2: Waiting for the Player to defeat all the targets
        {
            if (EnemyManager.Instance.GetTotalEnemiesDeregistered() == 11)
            {
                NextState(0);
            }
        }
        else if (state == 15)
        {
            NextState(2);
        }
        else if (state == 16)
        {
            audioManager.ChangeBGM(BGM_Boss);
            audioManager.StartBGM();
            NarrateSequenceAndNextState(NarrationSequences4);
        }
        else if (state == 17)
        {
            ActivateSpawner(ZeppelinSpawner, 0);

            Invoke("DestroyWindow", 0);
            Invoke("DestroyWindow", 0.1f);
            Invoke("DestroyWindow", 0.2f);

            NextState(6);
        }
        else if (state == 18)
        {
            NarrateSequenceAndNextState(NarrationSequences5);
        }
        else if (state == 19)
        {
            //controlled by zeppelin
        }

        else if (state == 20)
        {
            playerStatistics.SetActive(true);
            thanksForPlayingOurDemo.SetActive(true);
        }
    }

    private void DestroyWindow()
    {
        BedroomWindow.SetActive(false);
        var explosion = ObjectPool.Instance.GetFromPoolInactive(Pools.YellowFireImpactV2);
        explosion.transform.localScale = Vector3.one * 50;
        explosion.GetComponent<Explosion>().Init(BedroomWindow.transform.position + Random.insideUnitSphere * 30);
        explosion.SetActive(true);
    }

    private void ActivateSpawner(GameObject spawner, float time)
    {
        StartCoroutine(ActivateSpawnerInTime(spawner, time));
    }

    IEnumerator ActivateSpawnerInTime(GameObject spawner, float time)
    {
        yield return new WaitForSeconds(time);
        spawner.SetActive(true);
        spawner.GetComponent<IEnemySpawner>().Init();
    }

    private void NextState(float time)
    {
        StartCoroutine(NextStateInTime(time));
    }

    IEnumerator NextStateInTime(float time)
    {
        yield return new WaitForSeconds(time);
        state++;
        UpdateState();
    }

    private void GotoState(int newState, float time)
    {
        StartCoroutine(NextStateInTime(newState, time));
    }

    IEnumerator NextStateInTime(int newState, float time)
    {
        yield return new WaitForSeconds(time);
        state = newState;
        UpdateState();
    }
}
