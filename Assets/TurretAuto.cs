using System.Collections;
using UnityEngine;

public class TurretAuto : MonoBehaviour
{
    [SerializeField] private GameObject Rotateable = default;
    private GameObject target;

    private void Start()
    {
        StartCoroutine(FindTarget());
    }

    IEnumerator FindTarget()
    {
        while(true)
        {
            target = EnemyManager.Instance.GetAEnemy();
            yield return new WaitForSeconds(1);
        }
    }

    void Update()
    {
        AutoTrack();
    }

    private void AutoTrack()
    {
        //GameObject targetPoint = VRAimer.Instance.GetTargetPoint();

        if (target != null)
        {
            var direction = (target.transform.position - transform.position).normalized;
            var lookRotation = Quaternion.LookRotation(direction);
            Rotateable.transform.rotation = Quaternion.Slerp(Rotateable.transform.rotation, lookRotation, Time.deltaTime * 10);
        }
    }
}
