using UnityEngine;

public class SpitfireSpawner : MonoBehaviour
{
    void Start()
    {
        for (int i = 0; i < 10; i++)
        {
            Invoke("Spawn", i * 1);
        }
    }

    private void Spawn()
    {
        var newSpitfire = ObjectPool.Instance.GetFromPoolActiveSetTransform(Pools.Spitfire, transform);
        newSpitfire.transform.position = transform.position;
        newSpitfire.transform.SetParent(transform);
        EnemyManager.Instance.RegisterEnemy(newSpitfire);
    }
}
