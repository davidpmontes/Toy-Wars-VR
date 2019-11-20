using System.Collections;
using UnityEngine;

public class CollectibleCoin : MonoBehaviour, ICollectible
{
    public float speed = 20.0f;
    private Transform target;
    public int scoreValue;
    bool isCollected;
    bool reachedPlayer;

    void Awake()
    {
        target = GameObject.Find("CollectibleCoin").transform;
    }

    public void Init()
    {
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

        yield return new WaitForSeconds(3);

        ScoreScript.Instance.SetCurrentCollectibleCountVisibility(false);
        ObjectPool.Instance.DeactivateAndAddToPool(gameObject);
    }


}
