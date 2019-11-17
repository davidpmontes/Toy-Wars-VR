using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectibleCoin : MonoBehaviour, ICollectible
{
    public float speed = 20.0f;
    private Transform target;
    public int scoreValue;
    bool isCollected = false; // change to false initially
    bool reachedPlayer = false;
    // Start is called before the first frame update
    void Awake()
    {
        target = GameObject.Find("CollectibleCoin").transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (isCollected)
        {
            float step = speed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, target.position, step);
            transform.Rotate(new Vector3(0, 180, 0) * Time.deltaTime);

            if (transform.position == target.position)
            {
                
                StartCoroutine(ShowScore());
            }
                
            
        }
        
    }

    IEnumerator ShowScore()
    {
        //Show score over coin here
        yield return new WaitForSeconds(1);
        this.gameObject.SetActive(false);
        ScoreScript.Instance.AddCollectiblesCount(); // add score to Collectibles text
        ScoreScript.Instance.showCollectibleCount();
    }

    // call this function to begin the collection process
    public void Shot()
    {
        isCollected = true;
    }
}
