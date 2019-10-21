using UnityEngine;
using Cinemachine;

public class SpitfireSpawner : MonoBehaviour
{
    [SerializeField] CinemachineSmoothPath path;
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
        newSpitfire.SetActive(true);
        var cart = newSpitfire.GetComponent<CinemachineDollyCart>();
        cart.enabled = true;
        cart.m_Path = path;
        newSpitfire.GetComponent<Rigidbody>().isKinematic = true;
        EnemyManager.Instance.RegisterEnemy(newSpitfire);
    }
}
