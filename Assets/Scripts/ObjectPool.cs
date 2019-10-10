using System;
using System.Collections.Generic;
using UnityEngine;

public enum Pools { CannonBullet, Rocket, smallExplosion, largeExplosion, RedTarget, PingPongBall, P51Bullet, EnemyChinook, CFX_Explosion_B_Smoke_Text };

public class ObjectPool : MonoBehaviour
{
    public GameObject[] prefabs;
    private Dictionary<string, Queue<GameObject>> dictOfPools;
    private Dictionary<string, GameObject> dictOfPrefabs;

    public static ObjectPool Instance { get; private set; }

    private void Awake()
    {
        Instance = this;

        dictOfPools = new Dictionary<string, Queue<GameObject>>();
        dictOfPrefabs = new Dictionary<string, GameObject>();

        foreach (GameObject prefab in prefabs)
        {
            dictOfPools.Add(prefab.name, new Queue<GameObject>());
            dictOfPrefabs.Add(prefab.name, prefab);
        }

        foreach (Pools item in Enum.GetValues(typeof(Pools)))
        {
            int amount = 3;
            if (item.ToString() == "CannonBullet")
                amount = 25;
            if (item.ToString() == "TurretBullet")
                amount = 25;

            GrowPool(item, amount);
        }
    }

    private void GrowPool(Pools poolName, int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            var instance = Instantiate(dictOfPrefabs[poolName.ToString()]);
            instance.name = poolName.ToString();
            instance.transform.SetParent(transform);
            DeactivateAndAddToPool(instance);
        }
    }

    public GameObject GetFromPoolActiveSetTransform(Pools poolName, Transform t)
    {
        Queue<GameObject> pool = dictOfPools[poolName.ToString()];
        if (pool.Count == 0)
            GrowPool(poolName, 3);
        GameObject instance = pool.Dequeue();
        instance.transform.position = t.position;
        instance.transform.rotation = t.rotation;
        instance.SetActive(true);
        return instance;
    }

    public GameObject GetFromPoolInactive(Pools poolName)
    {
        Queue<GameObject> pool = dictOfPools[poolName.ToString()];
        if (pool.Count == 0)
            GrowPool(poolName, 3);
        var instance = pool.Dequeue();
        return instance;
    }

    public void DeactivateAndAddToPool(GameObject instance)
    {
        if (instance == null)
            return;

        instance.SetActive(false);
        instance.transform.SetParent(transform);
        Queue<GameObject> pool = dictOfPools[instance.name];
        pool.Enqueue(instance);
    }
}
