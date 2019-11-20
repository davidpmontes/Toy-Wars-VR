using UnityEngine;
using Cinemachine;
using System.Collections;

public class SpitfireSpawner : MonoBehaviour
{
    [SerializeField] CinemachineSmoothPath path = default;
    void Start()
    {
        for (int i = 0; i < 5; i++)
        {
            StartCoroutine(Spawn(i * 2));
        }
    }

    IEnumerator Spawn(float delay)
    {
        yield return new WaitForSeconds(delay);

        var newSpitfire = ObjectPool.Instance.GetFromPoolActiveSetTransform(Pools.Spitfire, transform);
        newSpitfire.GetComponent<Rigidbody>().isKinematic = true;
        newSpitfire.layer = LayerMask.NameToLayer("Enemy");
        var cart = newSpitfire.GetComponent<CinemachineDollyCart>();
        cart.m_Path = path;
        cart.m_Position = 0;
        newSpitfire.SetActive(true);
        cart.enabled = true;
        newSpitfire.GetComponent<IEnemy>().Init();
        EnemyManager.Instance.RegisterEnemy(newSpitfire);
    }
}
