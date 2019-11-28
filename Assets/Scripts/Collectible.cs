using UnityEngine;

public class Collectible : MonoBehaviour, ICollectible
{
    private string clip;
    bool done = false;

    protected AudioManager audioManager;

    virtual protected void Awake()
    {
        audioManager = AudioManager.GetAudioManager();
        //if (Random.Range(0, 10) < 5)
        //{
            clip = "chime_bell_positive_ring_01";
        //}
        //else
        //{
        //    clip = "chime_bell_positive_ring_02";
        //}
    }

    virtual public void Init()
    {
        if (!done)
        {
            GameObject coin = ObjectPool.Instance.GetFromPoolInactive(Pools.CollectibleStar);
            coin.transform.position = transform.position;
            coin.GetComponent<CollectibleCoin>().Init();
            coin.SetActive(true);
            audioManager.PlayUI(clip);
            done = true;
        }
    }
}
