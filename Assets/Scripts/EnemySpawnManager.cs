using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnManager : MonoBehaviour
{
    //public PlayerHealth playerHealth;       // Reference to the player's heatlh.
    public GameObject enemy;                // The enemy prefab to be spawned.
    public float spawnTimeInterval;            // How long between each spawn.
    //public Transform spawnPoint;         // An array of the spawn points this enemy can spawn from.
    public int amountOfEnemies;

    //Start is called before the first frame update
    /*void Start()
    {
        // Call the Spawn function after a delay of the spawnTime and then continue to call after the same amount of time.

        //InvokeRepeating("Spawn", spawnTimeInterval, spawnTimeInterval);
        //Invoke("Spawn", spawnTimeInterval);
        //Invoke("Spawn", spawnTimeInterval + 1f);
        for (int i = 0; i < amountOfEnemies; i++)
            Invoke("Spawn", spawnTimeInterval + (spawnTimeInterval * i));
    }*/

    IEnumerator Spawn(GameObject enemy, float startTime, Vector3 spawnPoint, int amountOfEnemies)
    {
        // waits initially for an interval of time
        yield return new WaitForSeconds(startTime);

        // Instantiates an enemy
        Instantiate(enemy, spawnPoint, Quaternion.identity);
        
    }

    // lets the Level Manager call the enemy spawn manager
    public void SpawnEnemies(GameObject enemy, float interval, Vector3 spawnPoint, int amountOfEnemies)
    {
        // For the amount of enemies, spawn them after set amount of time interval
        for (int i = 0; i < amountOfEnemies; i++)
            StartCoroutine(Spawn(enemy, interval + (interval * i), spawnPoint, amountOfEnemies));
            
    }
}
