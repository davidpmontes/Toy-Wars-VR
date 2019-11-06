using System.Collections;
using UnityEngine;

public class Level1Manager : MonoBehaviour, ILevelManager
{
    public static Level1Manager Instance { get; private set; }

    public int state;

    [SerializeField] GameObject popUpTargetEnemySpawner = default;
    [SerializeField] GameObject spitfireEnemySpawner = default;
    [SerializeField] GameObject attackHelicopterEnemySpawner = default;


    [SerializeField] AudioClip audioClipBackgroundMusic = default;
    [SerializeField] AudioClip audioClipWowGreatShot = default;
    [SerializeField] AudioClip audioClipYouGotAllTheTargets = default;
    [SerializeField] AudioClip thanksForPlaying = default;
    [SerializeField] AudioClip[] sound_effects= default;
    [SerializeField] AudioClip[] narration_sequence = default;

    [SerializeField] GameObject playerStatistics;
    [SerializeField] GameObject thanksForPlayingOurDemo;


    private AudioManager audioManager;


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

    public void NarrateSequence(AudioClip[] clips, float delay = 0.0f, bool blocking = false)
    {
        StartCoroutine(NarrateSequence(clips, -1, delay));
    }

    IEnumerator NarrateSequence(AudioClip[] clips, int pos, float delay)
    {
        int length = clips.Length;
        for (int i = 0; i < clips.Length; i++)
        {
            audioManager.PlayNarration(clips[i]);
            if(i != length - 1)
            {
                yield return new WaitForSeconds(clips[i].length);
            }
            else
            {
                yield return new WaitForSeconds(clips[i].length + delay);
            }
        }
        NextState(1);
    }

    public void IntroStep()
    {

    }

    public void UpdateState()
    {
        if(state == -1)
        {
            NextState(1);
        }
        else if (state == 0) //Opening scene, audio introduction
        {
            NarrateSequence(narration_sequence, 0.2f);
        }
        else if (state == 1) //pop up targets
        {
            popUpTargetEnemySpawner.SetActive(true);
            NextState(0.5f);
        }
        else if (state == 2) //Waiting for the Player to defeat all the targets
        {
            if (EnemyManager.Instance.GetEnemyCount() == 9)
            {
                audioManager.PlayNarration(audioClipWowGreatShot);
            }

            if (EnemyManager.Instance.GetEnemyCount() <= 0)
            {
                audioManager.PlayNarration(audioClipYouGotAllTheTargets);
                NextState(3);
            }
        }
        else if (state == 3) //pause after defeating all enemies
        {
            NextState(2);
        }
        else if (state == 4)    //Attack Helicopter
        {
            attackHelicopterEnemySpawner.SetActive(true);
            NextState(0.5f);
        }
        else if (state == 5)    //Waiting for the Player to defeat all the targets
        {
            if (EnemyManager.Instance.GetEnemyCount() <= 0)
            {
                NextState(0);
            }
        }
        else if (state == 6)    //Spitfires Spawner
        {
            spitfireEnemySpawner.SetActive(true);
            NextState(0.5f);
        }
        else if (state == 7)    //Waiting for the Player to defeat all the targets
        {
            if (EnemyManager.Instance.GetEnemyCount() <= 0)
            {
                NextState(0);
            }
        }
        else if (state == 8)
        {
            audioManager.PlayNarration(thanksForPlaying);
            playerStatistics.SetActive(true);
            thanksForPlayingOurDemo.SetActive(true);
        }
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
}
