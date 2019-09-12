using UnityEngine;

public class Rocket : MonoBehaviour
{
    Rigidbody rb;

    private int lifespan = 3;
    private float speed = 3;
    private GameObject target;
    private Transform turnable;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        turnable = transform.Find("Turnable");
    }

    private void Update()
    {
        if (target != null)
        {
            var distance = Vector3.Distance(target.transform.position, transform.position);
            rb.velocity = (target.transform.position - transform.position).normalized * speed;

            if (distance > 2)
            {
                turnable.LookAt(target.transform.position);
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        CancelInvoke();
        var explosion = ObjectPool.Instance.GetFromPoolActiveSetTransform(Pools.smallExplosion, transform);
        ObjectPool.Instance.DeactivateAndAddToPool(gameObject);
    }

    private void OnEnable()
    {
        Invoke("Deactivate", lifespan);
    }

    void Deactivate()
    {
        ObjectPool.Instance.DeactivateAndAddToPool(gameObject);
    }

    public void Init(Transform t, GameObject target)
    {
        this.target = target;
        transform.position = t.position;
        transform.rotation = t.rotation;
        rb.velocity = t.forward * speed;
    }
}
