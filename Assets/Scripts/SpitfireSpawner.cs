using UnityEngine;
using Cinemachine;
using System.Collections;

public class SpitfireSpawner : MonoBehaviour, IEnemySpawner
{
    [SerializeField] CinemachineSmoothPath path = default;
    public int numberOfEnemies;

    public void Init()
    {
        for (int i = 0; i < numberOfEnemies; i++)
        {
            StartCoroutine(Spawn(i * 3));
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
        newSpitfire.GetComponent<IEnemy>().Init();
        newSpitfire.SetActive(true);
        cart.enabled = true;
        EnemyManager.Instance.RegisterEnemy(newSpitfire);
    }
}
