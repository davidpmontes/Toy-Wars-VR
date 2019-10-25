using UnityEngine;

public class AttackHelicopterSpawner : MonoBehaviour
{
    private AudioManager audioManager;
    void Start()
    {
        audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();

        for (int i = 0; i < 10; i++)
        {
            var newAttackHelicopter = ObjectPool.Instance.GetFromPoolActiveSetTransform(Pools.AttackHelicopter, transform);
            newAttackHelicopter.transform.position = transform.position + new Vector3(Random.Range(-50, 50), Random.Range(20, 70), Random.Range(-50, 50));
            newAttackHelicopter.transform.SetParent(transform);
            EnemyManager.Instance.RegisterEnemy(newAttackHelicopter);
        }
    }
}
