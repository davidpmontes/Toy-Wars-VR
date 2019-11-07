using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainCar : MonoBehaviour, ICollectible
{
    [SerializeField] private GameObject train_engine;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void Shot()
    {
        train_engine.GetComponent<Train>().Shot();
    }
}
