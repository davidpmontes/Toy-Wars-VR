using System.Collections.Generic;
using UnityEngine;

public class BaseAssetManager : MonoBehaviour
{
    public static BaseAssetManager Instance { get; private set; }
    private int baseAssetCount;
    [SerializeField] private List<GameObject> baseAssets;

    private void Awake()
    {
        Instance = this;
    }

    public GameObject GetTopBaseAsset()
    {
        if (baseAssets.Count > 0)
            return baseAssets[0];

        return null;
    }

    public void RegisterBaseAsset()
    {
        baseAssetCount++;
    }

    public void DeregisterEnemy()
    {
        baseAssetCount--;
        Level1Manager.Instance.UpdateState();
    }
}
