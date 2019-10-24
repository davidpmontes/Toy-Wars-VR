using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
	public static EnemyManager Instance { get; private set; }

    private List<GameObject> AllEnemies;

    void Awake()
    {
        Instance = this;
        AllEnemies = new List<GameObject>();
    }

    public void RegisterEnemy(GameObject newEnemy)
    {
        AllEnemies.Add(newEnemy);
    }

    public void DeregisterEnemy(GameObject oldEnemy)
    {
        AllEnemies.Remove(oldEnemy);
        Level1Manager.Instance.UpdateState();
        ScoreScript.Instance.AddScore(1000);
    }

    public GameObject GetAEnemy()
    {
        if (AllEnemies.Count > 0)
        {
            return AllEnemies[0];
        }
        return null;
    }

    public GameObject GetNearestEnemy(Vector3 playerPosition)
    {
        GameObject nearestEnemy = null;
        float distance = float.MaxValue;

        foreach(GameObject enemy in AllEnemies)
        {
            var newDistance = Vector3.Distance(playerPosition, enemy.transform.position);

            if (newDistance < distance)
            {
                distance = newDistance;
                nearestEnemy = enemy;
            }
        }

        return nearestEnemy;
    }

    public int GetEnemyCount()
    {
        return AllEnemies.Count;
    }
}
