using System.Collections;
using UnityEngine;
using Cinemachine;

public class AttackHelicopterSpawnerDolly : MonoBehaviour, IEnemySpawner
{
    [SerializeField] CinemachineSmoothPath path = default;
    public int numberOfEnemies;
    private GameObject newAttackHelicopterDolly;
    public void Init()
    {
        for (int i = 0; i < numberOfEnemies; i++)
        {
            StartCoroutine(Spawn(i * 2));
        }
    }

    IEnumerator Spawn(float delay)
    {
        yield return new WaitForSeconds(delay);

        newAttackHelicopterDolly = ObjectPool.Instance.GetFromPoolInactive(Pools.AttackHelicopterDolly);
        newAttackHelicopterDolly.GetComponent<Rigidbody>().isKinematic = true;
        newAttackHelicopterDolly.layer = LayerMask.NameToLayer("Enemy");
        var cart = newAttackHelicopterDolly.GetComponent<CinemachineDollyCart>();
        cart.m_Path = path;
        cart.m_PositionUnits = CinemachinePathBase.PositionUnits.Distance;
        cart.m_Position = 0;
        cart.enabled = true;
        newAttackHelicopterDolly.transform.GetChild(0).transform.localScale = Vector3.zero;
        newAttackHelicopterDolly.SetActive(true);
        newAttackHelicopterDolly.GetComponent<IEnemy>().Init();
        EnemyManager.Instance.RegisterEnemy(newAttackHelicopterDolly);
    }
}
