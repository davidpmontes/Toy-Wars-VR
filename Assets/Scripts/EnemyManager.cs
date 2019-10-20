using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
	public static EnemyManager Instance { get; private set; }

    [SerializeField] private HashSet<GameObject> AllEnemies;

    void Awake()
    {
        Instance = this;
        AllEnemies = new HashSet<GameObject>();
    }

    public void RegisterEnemy(GameObject newEnemy)
    {
        AllEnemies.Add(newEnemy);
    }

    public void DeregisterEnemy(GameObject oldEnemy)
    {
        AllEnemies.Remove(oldEnemy);
        Level1Manager.Instance.UpdateState();
    }

    public int GetEnemyCount()
    {
        return AllEnemies.Count;
    }
}
