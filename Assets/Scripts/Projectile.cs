using UnityEngine;

public class Projectile : MonoBehaviour, IProjectile
{
    [SerializeField] private float speed = default;
    [SerializeField] private float lifespan = default;

    private Rigidbody rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent(out IEnemy component))
        {
            if (!component.IsVulnerable())
                return;

            ScoreScript.Instance.AddNumberOfHits();
            component.DamageEnemy(transform.position);
            CancelInvoke();
            ObjectPool.Instance.DeactivateAndAddToPool(gameObject);
        }

        if (collision.gameObject.TryGetComponent(out ICollectible coinComponent))
        {
            coinComponent.Init();
        }

        if (collision.gameObject.TryGetComponent(out Soldier soldier))
        {
            soldier.Fratricide();
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
