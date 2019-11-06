using UnityEngine;

public class Projectile : MonoBehaviour
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
            ScoreScript.Instance.AddNumberOfHits();
            component.DamageEnemy(transform.position);
            CancelInvoke();
            ObjectPool.Instance.DeactivateAndAddToPool(gameObject);
        }
        if (collision.gameObject.TryGetComponent<Collectible>(out Collectible coinComponent))
        {
            coinComponent.Shot();
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
