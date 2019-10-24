using System.Collections;
using UnityEngine;

public class TurretCannonAuto : MonoBehaviour
{
    [SerializeField] private Transform LeftAim = default;
    [SerializeField] private Transform RightAim = default;
    [SerializeField] private Transform barrelLeftTip = default;
    [SerializeField] private Transform barrelRightTip = default;
    [SerializeField] private ParticleSystem leftFlash = default;
    [SerializeField] private ParticleSystem rightFlash = default;
    [SerializeField] private GameObject Rotateable = default;

    private GameObject target;
    private float targetAlignment;
    private bool fireLeft;

    private void Start()
    {
        StartCoroutine(GetNearestTarget());
        StartCoroutine(FireOnTarget());
    }

    private void Update()
    {
        TrackTarget();
    }

    IEnumerator GetNearestTarget()
    {
        while(true)
        {
            target = EnemyManager.Instance.GetNearestEnemy(transform.position);
            yield return new WaitForSeconds(3);
        }
    }

    IEnumerator FireOnTarget()
    {
        while (true)
        {
            if (target != null && targetAlignment > .9f)
            {
                if (fireLeft)
                {
                    SpawnBulletLeft();
                }
                else
                {
                    SpawnBulletRight();
                }
                fireLeft = !fireLeft;
            }

            yield return new WaitForSeconds(0.5f);
        }
    }

    private void TrackTarget()
    {
        if (target == null)
            return;

        Vector3 targetDir = target.transform.position - Rotateable.transform.position;
        Vector3 newDir = Vector3.RotateTowards(Rotateable.transform.forward, targetDir, Time.deltaTime, 0.0f);
        Rotateable.transform.rotation = Quaternion.LookRotation(newDir);

        targetAlignment = Vector3.Dot(targetDir.normalized, Rotateable.transform.forward.normalized);
    }

    private void SpawnBulletLeft()
    {
        var turretBullet = ObjectPool.Instance.GetFromPoolInactive(Pools.PingPongBall);

        Vector3 direction = (LeftAim.position - barrelLeftTip.position).normalized;
        turretBullet.GetComponent<Projectile>().Init(barrelLeftTip, direction.normalized);

        turretBullet.SetActive(true);

        leftFlash.Play();
    }

    private void SpawnBulletRight()
    {
        var turretBullet = ObjectPool.Instance.GetFromPoolInactive(Pools.PingPongBall);

        Vector3 direction = (RightAim.position - barrelRightTip.position).normalized;
        turretBullet.GetComponent<Projectile>().Init(barrelRightTip, direction.normalized);

        turretBullet.SetActive(true);

        rightFlash.Play();
    }
}
