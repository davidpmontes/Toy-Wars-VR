using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TankLevelManager : MonoBehaviour, ILevelManager
{
    // Start is called before the first frame update

    [SerializeField] AudioClip[] sound_effects = default;
    [SerializeField] List<TriggerCoin> target_spawners;
    [SerializeField] List<GameObject> targets;
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
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene("MainMenu");
        }
    }

    public void UpdateState()
    {

    }

    public void ActivateTarget(TriggerCoin coin)
    {
        int key = target_spawners.IndexOf(coin);
        targets[key].SetActive(true);
        coin.gameObject.SetActive(false);
    }
}
