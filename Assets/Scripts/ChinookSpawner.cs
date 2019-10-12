using UnityEngine;

public class ChinookSpawner : MonoBehaviour
{
    void Start()
    {
        for(int i = 0; i < 10; i++)
        {
            var newChinook = ObjectPool.Instance.GetFromPoolActiveSetTransform(Pools.EnemyChinook, transform);
            newChinook.transform.position = transform.position + new Vector3(Random.Range(-50, 50), Random.Range(20, 70), Random.Range(-50, 50));
            newChinook.transform.SetParent(transform);
            EnemyManager.Instance.RegisterEnemy();
        }
    }

    private void Update()
    {
        transform.Rotate(Vector3.up, Time.deltaTime * 20);
    }
}
