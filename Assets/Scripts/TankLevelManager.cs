using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        
    }

    // Update is called once per frame
    void Update()
    {
        
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
