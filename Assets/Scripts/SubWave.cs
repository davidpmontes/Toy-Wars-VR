using UnityEngine;

[System.Serializable]
public class SubWave : MonoBehaviour
{
    public int startTime;
    public int numRepeats;
    public float gapTime;
    public ENEMY_NAMES enemyNames;
    public Transform[] waypointLocations;
    public bool loop;

    private int repeats;

    public void Activate()
    {
        repeats = 0;
        InvokeRepeating("RepeatSubWave", startTime, gapTime);
    }

    private void RepeatSubWave()
    {
        repeats++;
        if (repeats > numRepeats)
        {
            CancelInvoke();
            return;
        }

        var enemyName = (Pools)System.Enum.Parse(typeof(Pools), enemyNames.ToString());
        var newEnemy = ObjectPool.Instance.GetFromPoolInactive(enemyName);

        for (int i = 0; i < waypointLocations.Length; i++)
        {
            newEnemy.GetComponent<RedTankAI>().AddToWaypoints(waypointLocations[i]);
        }

        newEnemy.transform.position = waypointLocations[0].position;
        EnemyManager.Instance.AddToAllEnemies(newEnemy);
        newEnemy.SetActive(true);
    }
}
