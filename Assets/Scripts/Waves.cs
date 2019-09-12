using UnityEngine;

public class Waves : MonoBehaviour
{
    public int GetEnemiesLeft()
    {
        var enemiesLeft = 0;

        for (int i = 0; i < transform.childCount; i++)
        {
            enemiesLeft += transform.GetChild(i).GetComponent<SubWave>().numRepeats;
        }

        return enemiesLeft;
    }

    public void ActivateSubWaves()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).GetComponent<SubWave>().Activate();
        }
    }
}
