using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZeppelinBomber : MonoBehaviour
{
    [SerializeField] private List<GameObject> bombs = default;
    [SerializeField] private GameObject[] bombSpawnPoints = default;

    private void Awake()
    {
        bombs = new List<GameObject>();
    }

    private void Start()
    {
        InvokeRepeating("Cycle", 0, 10);
    }

    private void Cycle()
    {
        LoadBombs();
        Invoke("DropBombs", 2);
    }

    public void LoadBombs()
    {
        for (int i = 0; i <= 6; i++)
        {
            var bomb = ObjectPool.Instance.GetFromPoolInactive(Pools.Bomb);
            bomb.transform.SetParent(transform);
            bomb.GetComponent<Bomb>().Init();
            bomb.transform.localRotation = Quaternion.Euler(0, 90, 90);
            bomb.transform.position = bombSpawnPoints[i].transform.position;
            bomb.SetActive(true);
            bombs.Add(bomb);
        }
        bombs.Reverse();
    }

    public void DropBombs()
    {
        for (int i = 0; i < bombs.Count; i++)
        {
            StartCoroutine(DropBombDelayed(bombs[i], i * 0.2f));
        }
        bombs.Clear();
    }

    IEnumerator DropBombDelayed(GameObject bomb, float wait)
    {
        yield return new WaitForSeconds(wait);
        bomb.GetComponent<Bomb>().Drop();
    }
}
