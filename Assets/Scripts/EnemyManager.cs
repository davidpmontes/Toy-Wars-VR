using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
	public static EnemyManager Instance { get; private set; }

    private List<GameObject> AllEnemies;
    private AudioManager audioManager;
    private int totalEnemiesDeregistered = 0;

    void Awake()
    {
        Instance = this;
        audioManager = AudioManager.GetAudioManager();
        AllEnemies = new List<GameObject>();
    }

    public void RegisterEnemy(GameObject newEnemy)
    {
        AllEnemies.Add(newEnemy);
    }

    public void DeregisterEnemyNoPoints(GameObject oldEnemy)
    {
        totalEnemiesDeregistered++;
        AllEnemies.Remove(oldEnemy);
        Level1Manager.Instance.UpdateState();
    }

    public void DeregisterEnemyWithPoints(GameObject oldEnemy)
    {
        totalEnemiesDeregistered++;
        ShowFloatingText(oldEnemy.transform.position);
        AllEnemies.Remove(oldEnemy);
        Level1Manager.Instance.UpdateState();
        ScoreScript.Instance.AddFinalScore(1000);
        audioManager.PlayUI("collect_coin_01");
    }

    public GameObject GetAEnemy()
    {
        if (AllEnemies.Count > 0)
        {
            return AllEnemies[0];
        }
        return null;
    }

    private void ShowFloatingText(Vector3 position)
    {
        var ScoreText = ObjectPool.Instance.GetFromPoolInactive(Pools.ScoreText);
        ScoreText.transform.position = position;
        Vector3 direction = ScoreText.transform.position - Camera.main.transform.position;
        ScoreText.transform.LookAt(Camera.main.transform.position);
        ScoreText.transform.Rotate(0, 180, 0);
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

    public List<GameObject> GetAllEnemyPositions()
    {
        return AllEnemies;
    }

    public int GetEnemyCount()
    {
        return AllEnemies.Count;
    }

    public int GetTotalEnemiesDeregistered()
    {
        return totalEnemiesDeregistered;
    }
}
