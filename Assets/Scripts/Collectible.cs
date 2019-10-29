using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectible : MonoBehaviour
{
    public float speed = 20.0f;
    private Transform target;
    public int scoreValue;
    bool isCollected = true;
    // Start is called before the first frame update
    void Awake()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (isCollected)
        {
            // TODO: add to score
            float step = speed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, target.position, step);
            transform.Rotate(new Vector3(0, 180, 0) * Time.deltaTime);
        }

        //if(Vector3.Distance(transform.position, player.position) < 0.001f)
        // TODO: despawn the collectible
        // append to HUD to show that it has been collected
    }

    // call this function to begin the collection process
    void Shot()
    {
        isCollected = true;
    }
}
