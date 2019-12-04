using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum LevelState {Start = 0, Intro = 1, Gameplay = 2, Finish = 3};
public class TankLevelManager : MonoBehaviour, ILevelManager
{
    // Start is called before the first frame update

    [SerializeField] AudioClip[] sound_effects = default;
    [SerializeField] List<TriggerCoin> target_spawners = default;
    [SerializeField] List<GameObject> targets = default;
    [SerializeField] private LevelState state = default;
    [SerializeField] private float wait_time = default;
    [SerializeField] GameObject firework_prefab = default;
    [SerializeField] Transform firework_spawn = default;
    [SerializeField] GameObject finish_point;

    [SerializeField] AudioClip bgm = default;
    [SerializeField] AudioClip victory = default;
    [SerializeField] AudioClip narration = default;
    [SerializeField] GameObject congrats = default;

    private int targets_left = 10;

    private static TankLevelManager instance;
    public static TankLevelManager GetInstance()
    {
        if(instance == null)
        {
            instance = GameObject.Find("TankLevelManager").GetComponent<TankLevelManager>();
        }
        return instance;
    }

    public void GetSoundEffects(out AudioClip[] fx)
    {
        fx = sound_effects;
    }

    void Start()
    {
        PlayerManager.Instance.EnableVehicle(PlayerVehicles.TANK);
        UpdateState();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene("MainMenu");
        }

        if (Input.GetKeyDown(KeyCode.Y))
        {
            Finish();
        }
    }

    public void UpdateState()
    {
        print(state);
        switch (state)
        {
            case LevelState.Start:
                AudioManager.Instance.StartBGM(bgm);
                StartCoroutine(DelayUpdate(wait_time));
                break;
            case LevelState.Intro:
                AudioManager.Instance.PlayNarration(narration);
                StartCoroutine(DelayUpdate(narration.length));
                break;
            case LevelState.Gameplay:
                break;
            case LevelState.Finish:
                Finish();
                break;
        }
        state++;
    }

    private IEnumerator DelayUpdate(float time)
    {
        yield return new WaitForSeconds(time);
        UpdateState();
    }

    public void ActivateTarget(TriggerCoin coin)
    {
        int key = target_spawners.IndexOf(coin);
        targets[key].SetActive(true);
        coin.gameObject.SetActive(false);
    }

    public void TargetDestroyed()
    {
        targets_left--;
        if(targets_left == 0)
        {
            state = LevelState.Finish;
        }
    }

    public void Finish()
    {
        StartCoroutine(SpawnFirework());
        AudioManager.Instance.StartBGM(victory);
        congrats.SetActive(true);
        CameraRigSetPosition.Instance.AttachToGameobject(finish_point.transform);
        CameraRigSetPosition.Instance.Relocate(finish_point.transform.position, 180f);
        GameObject.FindGameObjectWithTag("MainCamera").transform.rotation = Quaternion.LookRotation(firework_spawn.position - finish_point.transform.position);
    }
    
    private IEnumerator SpawnFirework()
    {
        print("finishing");
        float sec = Random.Range(0.2f, 1.0f);
        yield return new WaitForSeconds(sec);

        Vector3 spawn_pos = firework_spawn.position + new Vector3(Random.Range(-20f, 20f), Random.Range(-5f, 20f), Random.Range(-20f, 20f));
        ParticleSystem fw = Instantiate(firework_prefab, firework_spawn).GetComponent<ParticleSystem>();
        ParticleSystem.MainModule mod = fw.main;
        Color new_color = Random.ColorHSV();
        new_color.a = 1;
        mod.startColor = new_color;
        
        fw.transform.localScale = new Vector3(50, 50, 50);
        fw.transform.position = spawn_pos;
        yield return SpawnFirework();
    }
}
