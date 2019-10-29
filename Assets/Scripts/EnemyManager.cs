using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{

    public GameObject scoreText;
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
        showFloatingText(oldEnemy.transform.position);
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

    void showFloatingText(Vector3 position)
    {
        var ScoreText = ObjectPool.Instance.GetFromPoolInactive(Pools.ScoreText);
        ScoreText.transform.position = position;
        Vector3 direction = ScoreText.transform.position - Camera.main.transform.position;
        ScoreText.transform.LookAt(direction);
        var rotation = ScoreText.transform.rotation.eulerAngles;
        rotation.x = 0;
        ScoreText.transform.rotation = Quaternion.Euler(rotation);

        ScoreText.transform.SetParent(null);
        ScoreText.SetActive(true);
        
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
