using System.Collections;
using UnityEngine;
using Cinemachine;

public class AttackHelicopterSpawnerDolly : MonoBehaviour
{
    [SerializeField] CinemachineSmoothPath path = default;
    public int numberOfEnemies;
    void Start()
    {
        for (int i = 0; i < numberOfEnemies; i++)
        {
            StartCoroutine(Spawn(i * 2));
        }
    }

    IEnumerator Spawn(float delay)
    {
        yield return new WaitForSeconds(delay);

        var newAttackHelicopterDolly = ObjectPool.Instance.GetFromPoolActiveSetTransform(Pools.AttackHelicopterDolly, transform);
        newAttackHelicopterDolly.GetComponent<Rigidbody>().isKinematic = true;
        var cart = newAttackHelicopterDolly.GetComponent<CinemachineDollyCart>();
        cart.m_Path = path;
        newAttackHelicopterDolly.SetActive(true);
        cart.enabled = true;
        EnemyManager.Instance.RegisterEnemy(newAttackHelicopterDolly);
    }
}
