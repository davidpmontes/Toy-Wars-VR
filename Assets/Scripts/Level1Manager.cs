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
    [SerializeField] AudioClip audioClipWowGreatShot = default;
    [SerializeField] AudioClip audioClipYouGotAllTheTargets = default;
    [SerializeField] AudioClip thanksForPlaying = default;
    [SerializeField] AudioClip[] sound_effects = default;
    [SerializeField] AudioClip[] narration_sequence = default;

    /* Misc */
    [SerializeField] AudioClip SoldierOW = default;
    [SerializeField] AudioClip SoldierStopThat = default;
    [SerializeField] AudioClip SoldierWatchYourFire = default;

    /* Level 0 */
    [SerializeField] AudioClip CommanderYouMustBeOurNewRecruit = default;
    [SerializeField] AudioClip CommanderImCaptainStiffNeck = default;
    [SerializeField] AudioClip CommanderYouveObviouslyFoundYourTurret = default;
    [SerializeField] AudioClip CommanderButCanYouHitAnythingWithIt = default;
    [SerializeField] AudioClip CommanderLetsSeeWhatYouGot = default;
    [SerializeField] AudioClip CommanderSoldierBringUpTwoMoreTargets = default;
    [SerializeField] AudioClip CommanderPrettyEasyWhenTheyDontShootBack = default;
    [SerializeField] AudioClip CommanderSoldierGimmeTwoMoreTargets = default;
    [SerializeField] AudioClip CommanderYouveGotSomeSkillsNowFinishTheRestOff = default;
    [SerializeField] AudioClip CommanderNotBadRecruit = default;



    [SerializeField] AudioClip SoldierYesSir = default;
    [SerializeField] AudioClip SoldierRogerThat = default;

    /* Level 1 */
    [SerializeField] AudioClip SoldierSirEnemyForcesApproaching = default;
    [SerializeField] AudioClip CommanderAlrightThisIsTheRealDealDefendOurBase = default;

    /* Level 2 */
    [SerializeField] AudioClip SoldierSirTheEnemyHasRegroupedAndIsNowAttackingTheNorthBase = default;

    /* Level 3 */
    [SerializeField] AudioClip SoldierTheZepplenatorIsHere = default;
    [SerializeField] AudioClip SoldierWereDoneFor = default;

    [SerializeField] AudioClip CommanderGetAHoldOfYourselves = default;
    [SerializeField] AudioClip CommanderWeveStillGotTheSecretWeapon = default;
    [SerializeField] AudioClip CommanderRecruitChargeTheLaserCannon = default;

    [SerializeField] GameObject playerStatistics = default;
    [SerializeField] GameObject thanksForPlayingOurDemo = default;

    [SerializeField] AudioClip[] NarrationSequences0_1 = default;
    [SerializeField] AudioClip[] NarrationSequences0_2 = default;
    [SerializeField] AudioClip[] NarrationSequences1_1 = default;
    [SerializeField] AudioClip[] NarrationSequences2_1 = default;




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
        Debug.Log(state);
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
            NextState(0);
        }
        else if (state == 2) //Waiting for the Player to defeat all the targets
        {
            if (EnemyManager.Instance.GetTotalEnemiesDeregistered() == 4)
            {
                NarrateSequence(NarrationSequences0_2, 0.2f);
            }
        }
        else if (state == 3) //pop up second set of 5 targets
        {
            popUpTargetEnemySpawner2.SetActive(true);
            NextState(0);
        }
        else if (state == 4) //Waiting for the Player to defeat all the targets
        {
            if (EnemyManager.Instance.GetTotalEnemiesDeregistered() == 8)
            {
                audioManager.PlayNarration(CommanderYouveGotSomeSkillsNowFinishTheRestOff, 1f);
                NextState(5f);
            }
        }
        else if (state == 5) //pop up third set of 5 targets
        {
            popUpTargetEnemySpawner3.SetActive(true);
            NextState(0);
        }
        else if (state == 6) //Waiting for the Player to defeat all the targets
        {
            if (EnemyManager.Instance.GetTotalEnemiesDeregistered() == 12)
            {
                audioManager.PlayNarration(CommanderNotBadRecruit, 1f);
                NextState(5f);
            }
        }
        else if (state == 7)    // First wave Narration Sequence
        {
            NarrateSequence(NarrationSequences1_1, 0.2f);
        }
        else if (state == 8)    //Attack Helicopters
        {
            ActivateSpawner(attackHelicopterEnemySpawnerDolly1, 0);
            ActivateSpawner(attackHelicopterEnemySpawnerDolly2, 7);
            ActivateSpawner(attackHelicopterEnemySpawnerDolly3, 14);
            NextState(0);
        }
        else if (state == 9) //Waiting for the Player to defeat all the targets
        {
            if (EnemyManager.Instance.GetTotalEnemiesDeregistered() == 21)
            {
                NarrateSequence(NarrationSequences2_1, 0.2f);
            }
        }
        else if (state == 10)    //Spitfires
        {
            ActivateSpawner(spitfireEnemySpawnerDolly1, 0);
            ActivateSpawner(spitfireEnemySpawnerDolly2, 1);
            ActivateSpawner(attackHelicopterEnemySpawnerDolly4, 13);
            ActivateSpawner(attackHelicopterEnemySpawnerDolly5, 12);
            ActivateSpawner(attackHelicopterEnemySpawnerDolly6, 10);
            NextState(0);
        }
        else if (state == 11) //Waiting for the Player to defeat all the targets
        {
            if (EnemyManager.Instance.GetTotalEnemiesDeregistered() == 34)
            {
                NarrateSequence(NarrationSequences2_1, 0.2f);
            }
        }
        else if (state == 12)
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
