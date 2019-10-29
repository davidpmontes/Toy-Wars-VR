using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreTextScript : MonoBehaviour
{
    [SerializeField] private float lifespan;

    // Start is called before the first frame update
    void Start()
    {
            
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnEnable()
    {
        Invoke("Deactivate", lifespan);
    }

    void Deactivate()
    {
        ObjectPool.Instance.DeactivateAndAddToPool(gameObject);
    }
}
