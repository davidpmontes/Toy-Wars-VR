using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P51Cannon : MonoBehaviour
{
    [SerializeField] private Transform barrelTip;
    [SerializeField] private Transform aim;

    private readonly float TIME_GAP = 0.1f;
    private float currTime;

    void Start()
    {
        
    }

    void Update()
    {
        if (InputController.Button4())
        {
            if (Time.time > currTime)
            {
                currTime += TIME_GAP;
                SpawnBullet();
            }
        }
    }

    private void SpawnBullet()
    {
        var turretBullet = ObjectPool.Instance.GetFromPoolInactive(Pools.P51Bullet);

        Vector3 direction = Vector3.zero;
        direction = (aim.position - barrelTip.position).normalized;
        turretBullet.GetComponent<Projectile>().Init(barrelTip, direction.normalized);

        turretBullet.SetActive(true);

        //audiosource.PlayOneShot(shoot);
        //leftFlash.Play();
    }
}
