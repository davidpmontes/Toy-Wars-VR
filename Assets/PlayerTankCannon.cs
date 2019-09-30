using UnityEngine;
using Valve.VR;

public class PlayerTankCannon : MonoBehaviour
{
    public SteamVR_Action_Boolean fireAction;

    [SerializeField] private Transform spawnPosition;
    [SerializeField] private Transform aimingPoint;

    void Update()
    {
        GetVRInput();
    }

    private void GetVRInput()
    {
        if (fireAction.state)
        {
            SpawnBullet();
        }
    }

    private void SpawnBullet()
    {
        var bullet = ObjectPool.Instance.GetFromPoolInactive(Pools.PingPongBall);

        Vector3 direction = (aimingPoint.position - spawnPosition.position).normalized;
        bullet.GetComponent<Projectile>().Init(spawnPosition, direction.normalized);

        bullet.SetActive(true);
    }
}
