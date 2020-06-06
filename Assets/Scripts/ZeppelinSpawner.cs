using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZeppelinSpawner : MonoBehaviour, IEnemySpawner
{
    [SerializeField] private GameObject Zeppelin = default;

    public void Init()
    {
        Spawn();
    }

    private void Spawn()
    {
        Zeppelin.SetActive(true);
        Zeppelin.GetComponent<Zeppelin>().Init();
    }
}
