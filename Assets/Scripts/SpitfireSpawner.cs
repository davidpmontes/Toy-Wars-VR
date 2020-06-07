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

        var newSpitfire = ObjectPool.Instance.GetFromPoolInactive(Pools.Spitfire);
        newSpitfire.GetComponent<Rigidbody>().isKinematic = true;
        newSpitfire.layer = LayerMask.NameToLayer("Enemy");
        var cart = newSpitfire.GetComponent<CinemachineDollyCart>();
        cart.m_Path = path;
        cart.m_PositionUnits = CinemachinePathBase.PositionUnits.Distance;
        cart.m_Position = 0;
        cart.enabled = true;
        newSpitfire.transform.GetChild(0).transform.localScale = Vector3.zero;
        newSpitfire.SetActive(true);
        newSpitfire.GetComponent<IEnemy>().Init();
        EnemyManager.Instance.RegisterEnemy(newSpitfire);
    }
}
