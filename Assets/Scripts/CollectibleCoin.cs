using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectibleCoin : MonoBehaviour, ICollectible
{
    public float speed = 20.0f;
    [SerializeField] private Transform target;
    public int scoreValue;
    bool isCollected;
    bool reachedPlayer;
    static Queue<GameObject> coins = new Queue<GameObject>();

    public void Init()
    {
        target = GameObject.Find("CollectibleCoin").transform;
        coins.Enqueue(gameObject);
        isCollected = true;
        reachedPlayer = false;
    }

    void Update()
    {
        if (isCollected)
        {
            if (!reachedPlayer)
            {
                float step = speed * Time.deltaTime;
                transform.position = Vector3.MoveTowards(transform.position, target.position, step);

                if (transform.position == target.position)
                {
                    reachedPlayer = true;

                    StartCoroutine(HideCoinAfterTime());
                }
            }
            else
            {
                transform.position = target.position;
            }

            transform.Rotate(Vector3.up, Time.deltaTime * 150, Space.Self);
        }
    }

    IEnumerator HideCoinAfterTime()
    {
        ScoreScript.Instance.AddCollectiblesCount();

        yield return new WaitForSeconds(ScoreScript.Instance.GetCollectibleCountDuration());
        
        ObjectPool.Instance.DeactivateAndAddToPool(gameObject);
    }
}
