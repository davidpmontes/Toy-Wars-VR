using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectible : MonoBehaviour, ICollectible
{
    public GameObject coin_prefab;
    private string clip;
    bool done = false;

    protected AudioManager audioManager;
    // Start is called before the first frame update
    virtual protected void Start()
    {
    }

    virtual protected void Awake()
    {
        audioManager = AudioManager.GetAudioManager();
        if (Random.Range(0, 1) < 0.5)
        {
            clip = "chime_bell_positive_ring_01";
        }
        else
        {
            clip = "chime_bell_positive_ring_02";
        }
    }

    // Update is called once per frame
    virtual protected void Update()
    {
        
    }

    virtual public void Shot()
    {
        if (!done)
        {
            GameObject coin = Instantiate(coin_prefab, transform.position, Quaternion.identity);
            coin.GetComponent<CollectibleCoin>().Shot();
            audioManager.PlayUI(clip);
            done = true;
        }
    }
}
