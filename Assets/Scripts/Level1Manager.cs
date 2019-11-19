using System.Collections;
using UnityEngine;

public class Level1Manager : MonoBehaviour, ILevelManager
{
    public static Level1Manager Instance { get; private set; }

    public int state;

    [SerializeField] GameObject popUpTargetEnemySpawner1 = default;
    [SerializeField] GameObject popUpTargetEnemySpawner2 = default;
    [SerializeField] GameObject popUpTargetEnemySpawner3 = default;

    [SerializeField] GameObject spitfireEnemySpawner = default;
    [SerializeField] GameObject attackHelicopterEnemySpawner = default;
    [SerializeField] GameObject attackHelicopterEnemySpawnerDolly1 = default;
    [SerializeField] GameObject attackHelicopterEnemySpawnerDolly2 = default;
    [SerializeField] GameObject attackHelicopterEnemySpawnerDolly3 = default;


    [SerializeField] AudioClip audioClipBackgroundMusic = default;
    [SerializeField] AudioClip audioClipWowGreatShot = default;
    [SerializeField] AudioClip audioClipYouGotAllTheTargets = default;
    [SerializeField] AudioClip thanksForPlaying = default;
    [SerializeField] AudioClip[] sound_effects = default;
    [SerializeField] AudioClip[] narration_sequence = default;

    /* Misc */
    [SerializeField] AudioClip SoldierOW;
    [SerializeField] AudioClip SoldierStopThat;
    [SerializeField] AudioClip SoldierWatchYourFire;

    /* Level 0 */
    [SerializeField] AudioClip CommanderYouMustBeOurNewRecruit;
    [SerializeField] AudioClip CommanderImCaptainStiffNeck;
    [SerializeField] AudioClip CommanderYouveObviouslyFoundYourTurret;
    [SerializeField] AudioClip CommanderButCanYouHitAnythingWithIt;
    [SerializeField] AudioClip CommanderLetsSeeWhatYouGot;
    [SerializeField] AudioClip CommanderSoldierBringUpTwoMoreTargets;
    [SerializeField] AudioClip CommanderPrettyEasyWhenTheyDontShootBack;
    [SerializeField] AudioClip CommanderSoldierGimmeTwoMoreTargets;
    [SerializeField] AudioClip CommanderYouveGotSomeSkillsNowFinishTheRestOff;
    [SerializeField] AudioClip CommanderNotBadRecruit;



    [SerializeField] AudioClip SoldierYesSir;
    [SerializeField] AudioClip SoldierRogerThat;

    /* Level 1 */
    [SerializeField] AudioClip SoldierSirEnemyForcesApproaching;
    [SerializeField] AudioClip CommanderAlrightThisIsTheRealDealDefendOurBase;

    /* Level 2 */
    [SerializeField] AudioClip SoldierSirTheEnemyHasRegroupedAndIsNowAttackingTheNorthBase;

    /* Level 3 */
    [SerializeField] AudioClip SoldierTheZepplenatorIsHere;
    [SerializeField] AudioClip SoldierWereDoneFor;

    [SerializeField] AudioClip CommanderGetAHoldOfYourselves;
    [SerializeField] AudioClip CommanderWeveStillGotTheSecretWeapon;
    [SerializeField] AudioClip CommanderRecruitChargeTheLaserCannon;

    [SerializeField] GameObject playerStatistics;
    [SerializeField] GameObject thanksForPlayingOurDemo;

    [SerializeField] AudioClip[] NarrationSequences0_1;
    [SerializeField] AudioClip[] NarrationSequences0_2;
    [SerializeField] AudioClip[] NarrationSequences1_1;



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
            if (i != length - 1)
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
        if (state == -1)
        {
            NextState(1);
        }
        else if (state == 0) //Opening scene, audio introduction
        {
            NarrateSequence(NarrationSequences0_1, 0.2f);
        }
        else if (state == 1) //pop up first set of 5 targets
        {
            popUpTargetEnemySpawner1.SetActive(true);
            NextState(0.5f);
        }
        else if (state == 2) //Waiting for the Player to defeat all the targets
        {
            if (EnemyManager.Instance.GetEnemyCount() <= 0)
            {
                NarrateSequence(NarrationSequences0_2, 0.2f);
            }
        }
        else if (state == 3) //pop up second set of 5 targets
        {
            popUpTargetEnemySpawner2.SetActive(true);
            NextState(0.5f);
        }
        else if (state == 4) //Waiting for the Player to defeat all the targets
        {
            if (EnemyManager.Instance.GetEnemyCount() <= 0)
            {
                audioManager.PlayNarration(CommanderYouveGotSomeSkillsNowFinishTheRestOff, 1f);
                NextState(5f);
            }
        }
        else if (state == 5) //pop up third set of 5 targets
        {
            popUpTargetEnemySpawner3.SetActive(true);
            NextState(0.5f);
        }
        else if (state == 6) //Waiting for the Player to defeat all the targets
        {
            if (EnemyManager.Instance.GetEnemyCount() <= 0)
            {
                audioManager.PlayNarration(CommanderNotBadRecruit, 1f);
                NextState(5f);
            }
        }
        else if (state == 7)    //Narration Sequence
        {
            NarrateSequence(NarrationSequences1_1, 0.2f);
        }
        else if (state == 8)    //Attack Helicopters1
        {
            ActivateSpawner(attackHelicopterEnemySpawnerDolly1, 0);
            ActivateSpawner(attackHelicopterEnemySpawnerDolly2, 7);
            ActivateSpawner(attackHelicopterEnemySpawnerDolly3, 14);
            NextState(30);
        }
        else if (state == 9)
        {
            audioManager.PlayNarration(thanksForPlaying);
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
}
