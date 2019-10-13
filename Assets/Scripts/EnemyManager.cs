using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
	public static EnemyManager Instance { get; private set; }

    private int enemyCount;

    [SerializeField] private GameObject AllEnemies;

    void Awake()
    {
        Instance = this;
    }

    public void RegisterEnemy()
    {
        enemyCount++;
    }

    public void DeregisterEnemy()
    {
        enemyCount--;
        Level1Manager.Instance.UpdateState();
    }

    public int GetEnemyCount()
    {
        return enemyCount;
    }
}
