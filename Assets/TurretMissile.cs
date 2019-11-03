using UnityEngine;

public class TurretMissile : MonoBehaviour
{
    private GameObject target;
    private float speed = 150;
    private GameObject missileSmoke;

    void Start()
    {
        Invoke("DestroySelf", 10);
    }

    void Update()
    {
        MoveToTarget();
    }

    public void Init(GameObject target, Vector3 position, Vector3 forward)
    {
        this.target = target;
        transform.position = position;
        transform.rotation = Quaternion.LookRotation(forward);

        missileSmoke = ObjectPool.Instance.GetFromPoolInactive(Pools.MissileSmoke);
        missileSmoke.transform.position = transform.position;
        missileSmoke.transform.SetParent(transform);
        missileSmoke.SetActive(true);
    }

    private void MoveToTarget()
    {
        if (target != null)
        {
            Vector3 targetDir = target.transform.position - transform.position;
            Vector3 newDir = Vector3.RotateTowards(transform.forward, targetDir, Time.deltaTime * 100, 0.0f);
            transform.rotation = Quaternion.LookRotation(newDir);

            var distance = Vector3.Distance(target.transform.position, transform.position);
            if (distance < 10)
            {
                target.GetComponent<IEnemy>().DamageEnemy(transform.position);
                CancelInvoke();
                ObjectPool.Instance.DeactivateAndAddToPool(missileSmoke);
                ObjectPool.Instance.DeactivateAndAddToPool(gameObject);
            }
        }

        transform.position += transform.forward * Time.deltaTime * speed;
    }

    private void DestroySelf()
    {
        ObjectPool.Instance.DeactivateAndAddToPool(gameObject);
    }
}
