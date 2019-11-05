using UnityEngine;
using Cinemachine;
using System.Collections;

public class SpitfireSpawner : MonoBehaviour
{
    [SerializeField] CinemachineSmoothPath[] path = default;
    void Start()
    {
        for (int i = 0; i < 10; i++)
        {
            StartCoroutine(Spawn(i * 1));
        }
    }

    IEnumerator Spawn(float delay)
    {
        yield return new WaitForSeconds(delay);

        var newSpitfire = ObjectPool.Instance.GetFromPoolActiveSetTransform(Pools.Spitfire, transform);
        newSpitfire.SetActive(true);
        var cart = newSpitfire.GetComponent<CinemachineDollyCart>();
        cart.enabled = true;
        cart.m_Path = path[Random.Range(0, 2)];
        newSpitfire.GetComponent<Rigidbody>().isKinematic = true;
        EnemyManager.Instance.RegisterEnemy(newSpitfire);
    }
}
