using System.Collections.Generic;
using UnityEngine;

public class PointsContainer : MonoBehaviour
{
    public static PointsContainer Instance;

    [SerializeField]
    private GameObject pointsPrefab;

    private Queue<GameObject> availablePoints = new Queue<GameObject>();


    private void Awake()
    {
        Instance = this;
        GrowPool();
    }

    public void PointsScored(int numPoints, Vector3 position)
    {
        var point = GetFromPool();
        point.GetComponent<Points>().Init(numPoints, position);
    }

    public GameObject GetFromPool()
    {
        if (availablePoints.Count == 0)
            GrowPool();

        var instance = availablePoints.Dequeue();
        instance.SetActive(true);
        return instance;
    }

    private void GrowPool()
    {
        for (int i = 0; i < 10; i++)
        {
            var instanceToAdd = Instantiate(pointsPrefab);
            instanceToAdd.transform.SetParent(transform);
            AddToPool(instanceToAdd);
        }
    }

    public void AddToPool(GameObject instance)
    {
        instance.SetActive(false);

        availablePoints.Enqueue(instance);
    }
}