using System.Collections;
using UnityEngine;

public class Level1Manager : MonoBehaviour, ILevelManager
{
    public static Level1Manager Instance { get; private set; }

    public int state;

    [SerializeField] GameObject popUpTargetEnemySpawner1 = default;
    [SerializeField] GameObject popUpTargetEnemySpawner2 = default;
    [SerializeField] GameObject popUpTargetEnemySpawner3 = default;

    [SerializeField] GameObject attackHelicopterEnemySpawnerDolly1 = default;
    [SerializeField] GameObject attackHelicopterEnemySpawnerDolly2 = default;
    [SerializeField] GameObject attackHelicopterEnemySpawnerDolly3 = default;

    [SerializeField] GameObject spitfireEnemySpawnerDolly1 = default;
    [SerializeField] GameObject spitfireEnemySpawnerDolly2 = default;
    [SerializeField] GameObject attackHelicopterEnemySpawnerDolly4 = default;
    [SerializeField] GameObject attackHelicopterEnemySpawnerDolly5 = default;
    [SerializeField] GameObject attackHelicopterEnemySpawnerDolly6 = default;





    [SerializeField] AudioClip audioClipBackgroundMusic = default;
    [SerializeField] AudioClip[] sound_effects = default;

    [SerializeField] GameObject playerStatistics = default;
    [SerializeField] GameObject thanksForPlayingOurDemo = default;

    [SerializeField] AudioClip[] NarrationSequences1 = default;
    [SerializeField] AudioClip[] NarrationSequences2 = default;
    [SerializeField] AudioClip[] NarrationSequences3 = default;
    [SerializeField] AudioClip[] NarrationSequences4 = default;


    private AudioManager audioManager;

    private readonly float POPUPTIMER_TIME_LIMIT = 10;
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
        UpdateState();
    }

    private void NarrateSequenceAndNextState(AudioClip[] clips, float delay, int nextState)
    {
        StartCoroutine(NarrateSequenceAndNextStateCR(clips, delay, nextState));
    }

    IEnumerator NarrateSequenceAndNextStateCR(AudioClip[] clips, float delay, int nextState)
    {
        int length = clips.Length;
        for (int i = 0; i < clips.Length; i++)
        {
            audioManager.PlayNarration(clips[i]);
            if (i != length - 1)
            {
                yield return new WaitForSeconds(clips[i].length);
            }
            else
            {
                yield return new WaitForSeconds(clips[i].length + delay);
            }
        }
        NextState(nextState);
    }

    public void UpdateState()
    {
        if (state == -1)
        {
            NextState(1);
        }
        else if (state == 0) //Opening scene, audio introduction
        {
            NarrateSequenceAndNextState(NarrationSequences1, 0.2f, state + 1);
        }
        else if (state == 1) //pop up first set of 4 targets
        {
            PopUpTargetEndTime = Time.time + POPUPTIMER_TIME_LIMIT;
            Invoke("PopUpTargetTimerNotification", POPUPTIMER_TIME_LIMIT);

            popUpTargetEnemySpawner1.SetActive(true);
            NextState(0);
        }
        else if (state == 2) //pop up second set of 4 targets
        {
            if (PopUpTargetEndTime < Time.time)
            {
                GotoState(5, 0);
                return;
            }

            if (EnemyManager.Instance.GetTotalEnemiesDeregistered() == 4)
            {
                popUpTargetEnemySpawner2.SetActive(true);
                NextState(0);
            }
        }
        else if (state == 3) //pop up third set of 4 targets
        {
            if (PopUpTargetEndTime < Time.time)
            {
                GotoState(5, 0);
                return;
            }

            if (EnemyManager.Instance.GetTotalEnemiesDeregistered() == 8)
            {
                popUpTargetEnemySpawner3.SetActive(true);
                NextState(0);
            }
        }
        else if (state == 4) //All targets defeated or time expires
        {
            if (PopUpTargetEndTime < Time.time)
            {
                GotoState(5, 0);
                return;
            }

            if (EnemyManager.Instance.GetTotalEnemiesDeregistered() == 12)
            {
                NarrateSequenceAndNextState(NarrationSequences2, 0.2f, state + 1);
            }
        }
        else if (state == 5) //Time fail
        {
            popUpTargetEnemySpawner1.SetActive(false);
            popUpTargetEnemySpawner2.SetActive(false);
            popUpTargetEnemySpawner3.SetActive(false);

            //Buzzer sounds
            //Display Score
            //NarrateSequence();
            NextState(0);
        }
        else if (state == 6)  //Time fail transition
        {
            GotoState(9, 0);
        }
        else if (state == 7) //Time success
        {
            popUpTargetEnemySpawner1.SetActive(false);
            popUpTargetEnemySpawner2.SetActive(false);
            popUpTargetEnemySpawner3.SetActive(false);

            //Success Sound
            //Display Score
            //NarrateSequence();
            NextState(0);
        }
        else if (state == 8) //Time success transition
        {
            GotoState(9, 0);
        }
        else if (state == 9) // Wave #1: Narration
        {
        }
        else if (state == 10) // Wave #1: Attack Helicopters appear
        {
            ActivateSpawner(attackHelicopterEnemySpawnerDolly1, 0);
            ActivateSpawner(attackHelicopterEnemySpawnerDolly2, 7);
            ActivateSpawner(attackHelicopterEnemySpawnerDolly3, 14);
            NextState(0);
        }
        else if (state == 11) // Wave #1: Waiting for the Player to defeat all the targets && Wave #2: Narration
        {
            if (EnemyManager.Instance.GetTotalEnemiesDeregistered() == 21)
            {
            }
        }
        else if (state == 12) // Wave #2: Spitfires appear
        {
            ActivateSpawner(spitfireEnemySpawnerDolly1, 0);
            ActivateSpawner(spitfireEnemySpawnerDolly2, 1);
            ActivateSpawner(attackHelicopterEnemySpawnerDolly4, 13);
            ActivateSpawner(attackHelicopterEnemySpawnerDolly5, 12);
            ActivateSpawner(attackHelicopterEnemySpawnerDolly6, 10);
            NextState(0);
        }
        else if (state == 13) // Wave #2: Waiting for the Player to defeat all the targets
        {
            if (EnemyManager.Instance.GetTotalEnemiesDeregistered() == 34)
            {
            }
        }
        else if (state == 14)
        {
            playerStatistics.SetActive(true);
            thanksForPlayingOurDemo.SetActive(true);
        }
    }

    private void ActivateSpawner(GameObject spawner, float time)
    {
        StartCoroutine(ActivateSpawnerInTime(spawner, time));
    }

    IEnumerator ActivateSpawnerInTime(GameObject spawner, float time)
    {
        yield return new WaitForSeconds(time);
        spawner.SetActive(true);
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
