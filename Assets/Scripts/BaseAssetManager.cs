﻿using System.Collections.Generic;
using UnityEngine;

public class BaseAssetManager : MonoBehaviour
{
    public static BaseAssetManager Instance { get; private set; }
    private int baseAssetCount;
    [SerializeField] private List<GameObject> baseAssets = default;

    private void Awake()
    {
        Instance = this;
    }

    public GameObject GetRandomHiddenBaseTarget()
    {
        return baseAssets[Random.Range(0, baseAssets.Count)];
    }

    public GameObject GetTopBaseAsset()
    {
        if (baseAssets.Count > 0)
            return baseAssets[Random.Range(0, baseAssets.Count)];

        return null;
    }

    public void DeregisterBaseAsset(GameObject baseAsset)
    {
        baseAssets.Remove(baseAsset);
        Level1Manager.Instance.UpdateState();
    }
}
