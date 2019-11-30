using UnityEngine;

public class LaserProjectile : MonoBehaviour, IProjectile
{
    [SerializeField] private float speed = default;
    [SerializeField] private float lifespan = default;
    [SerializeField] private bool isFriendlyLaser = default;

    private Rigidbody rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (isFriendlyLaser)
        {
            if (collision.gameObject.TryGetComponent(out IEnemy component))
            {
                ScoreScript.Instance.AddNumberOfHits();
                component.DamageEnemy(transform.position);
            }

            if (collision.gameObject.TryGetComponent(out ICollectible coinComponent))
            {
                coinComponent.Init();
            }
        }
        else
        {
            if (collision.gameObject.TryGetComponent(out IBaseAsset component))
            {
                component.TakeDamage(transform.position);
            }
            else
            {
                var explosion = ObjectPool.Instance.GetFromPoolInactive(Pools.CFX_Explosion_B_Smoke_Text);
                explosion.transform.GetComponent<Explosion>().Init(transform.position);
                explosion.SetActive(true);
            }
        }

        Deactivate();
    }

    void Deactivate()
    {
        CancelInvoke();
        ObjectPool.Instance.DeactivateAndAddToPool(gameObject);
    }

    public void Init(Transform t, Vector3 direction)
    {
        Invoke("Deactivate", lifespan);
        transform.position = t.position;
        transform.rotation = t.rotation;
        rb.velocity = direction * speed;
    }
}
