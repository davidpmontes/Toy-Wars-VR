using UnityEngine;
using UnityEngine.Playables;

public class WaveManager : MonoBehaviour
{
    [SerializeField] private GameObject[] waves;

    public static WaveManager Instance;
    private int currWave = 0;
    private int enemiesLeft;

    void Awake()
    {
        Instance = this;
        NextWaveStart();
    }

    public void NextWaveStart()
    {
        if (currWave < waves.Length)
        {
            waves[currWave].GetComponent<PlayableDirector>().Play();
        }
        else
        {
            Debug.Log("no more waves");
        }
    }

    public void SubWaves()
    {
        enemiesLeft = waves[currWave].GetComponent<Waves>().GetEnemiesLeft();
        waves[currWave].GetComponent<Waves>().ActivateSubWaves();
    }

    public void EnemyDestroyed()
    {
        enemiesLeft--;
        if (enemiesLeft == 0)
        {
            NextWaveStart();
        }
    }
}
