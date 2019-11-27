using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectibleCoin : MonoBehaviour, ICollectible
{
    public float speed = 20.0f;
    private Transform target;
    public int scoreValue;
    bool isCollected;
    bool reachedPlayer;
    static Queue<GameObject> coins = new Queue<GameObject>();

    void Awake()
    {
        target = GameObject.Find("CollectibleCoin").transform;
        
    }
    void Start()
    {
        Init();
    }

    public void Init()
    {
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
                    StartCoroutine(ShowAndHideScore());
                    if (coins.Count > 1)
                    {
                        //StopAllCoroutines();
                        ObjectPool.Instance.DeactivateAndAddToPool(gameObject);
                    }
                    
                }
            }
            else
            {
                transform.position = target.position;
            }

            transform.Rotate(Vector3.up, Time.deltaTime * 150, Space.Self);
        }
    }

    IEnumerator ShowAndHideScore()
    {
        ScoreScript.Instance.AddCollectiblesCount();
        ScoreScript.Instance.UpdateCurrentCollectibleCount();
        ScoreScript.Instance.SetCurrentCollectibleCountVisibility(true);
        coins.Dequeue();
        yield return new WaitForSeconds(3);
        
        if(coins.Count == 0)
        {
            ScoreScript.Instance.SetCurrentCollectibleCountVisibility(false);
            ObjectPool.Instance.DeactivateAndAddToPool(gameObject);
        }
        

    }


}
