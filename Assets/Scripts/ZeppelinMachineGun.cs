using System.Collections;
using UnityEngine;

public class ZeppelinMachineGun : MonoBehaviour
{
    [SerializeField] private GameObject target = default;
    [SerializeField] private ParticleSystem flash = default;
    [SerializeField] private Transform barrel = default;
    private bool targetAcquired;
    private bool newTargetSelected;

    void Start()
    {
        target = BaseAssetManager.Instance.GetRandomHiddenBaseTarget();
        newTargetSelected = true;
    }

    void Update()
    {
        if (targetAcquired)
        {
            targetAcquired = false;
            StartCoroutine(Fire());
        }

        if (newTargetSelected)
        {
            newTargetSelected = false;
            StartCoroutine(AimAtTarget());
        }
    }

    IEnumerator AimAtTarget()
    {
        while(true)
        {
            Vector3 targetDir = target.transform.position - transform.position;

            float step = 0.5f * Time.deltaTime;

            Vector3 newDir = Vector3.RotateTowards(transform.forward, targetDir, step, 0.0f);
            transform.rotation = Quaternion.LookRotation(newDir);

            if (Vector3.Dot(transform.forward.normalized, targetDir.normalized) > 0.99f)
            {
                targetAcquired = true;
                break;
            }

            yield return null;
        }

        yield return null;
    }

    IEnumerator Fire()
    {
        float endTime = Time.time + 3;

        while (Time.time < endTime)
        {
            flash.Play();
            AudioManager.GetAudioManager().PlayOneshot("big one", transform.position);

            var laser = ObjectPool.Instance.GetFromPoolInactive(Pools.RedLaserMissile);
            laser.transform.rotation = Quaternion.identity;
            Vector3 direction = barrel.forward.normalized;
            laser.GetComponent<IProjectile>().Init(barrel, direction.normalized);
            laser.SetActive(true);

            yield return new WaitForSeconds(0.3f);
        }

        //idle time between cannon volleys
        yield return new WaitForSeconds(5);

        target = BaseAssetManager.Instance.GetRandomHiddenBaseTarget();
        newTargetSelected = true;

        yield return null;
    }
}
