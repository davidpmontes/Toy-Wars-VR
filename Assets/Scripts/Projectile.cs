using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float lifespan;

    private Rigidbody rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            var damageable = other.gameObject.GetComponent<IDamageable>();

            if (damageable != null)
            {
                var explosion = ObjectPool.Instance.GetFromPoolInactive(Pools.smallExplosion);
                explosion.GetComponent<Explosion>().Init(transform.position, Vector3.zero);
                explosion.SetActive(true);

                damageable.TakeDamage(1);

                CancelInvoke();
                ObjectPool.Instance.DeactivateAndAddToPool(gameObject);
            }
        }
    }

    private void OnEnable()
    {
        Invoke("Deactivate", lifespan);
    }

    void Deactivate()
    {
        ObjectPool.Instance.DeactivateAndAddToPool(gameObject);
    }

    public void Init(Transform t, Vector3 direction)
    {
        transform.position = t.position;
        transform.rotation = t.rotation;
        rb.velocity = direction * speed;
    }
}
