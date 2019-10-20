using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnManager : MonoBehaviour
{
    //public PlayerHealth playerHealth;       // Reference to the player's heatlh.
    public GameObject enemy;                // The enemy prefab to be spawned.
    public float spawnTimeInterval = 3f;            // How long between each spawn.
    public Transform spawnPoint;         // An array of the spawn points this enemy can spawn from.
    
    // Start is called before the first frame update
    void Start()
    {
        // Call the Spawn function after a delay of the spawnTime and then continue to call after the same amount of time.
        InvokeRepeating("Spawn", spawnTimeInterval, spawnTimeInterval);
    }

    void Spawn()
    {
        // player health
        // Find a random index between zero and one less than the number of spawn points.
        

        // Create an instance of the enemy prefab at the randomly selected spawn point's position and rotation.
        Instantiate(enemy, spawnPoint.position, spawnPoint.rotation);
    }
}
