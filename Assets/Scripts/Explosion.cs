using UnityEngine;

public class Explosion : MonoBehaviour
{
    private int lifespan = 1;


    private void OnEnable()
    {
        Invoke("Deactivate", lifespan);
    }

    void Deactivate()
    {
        ObjectPool.Instance.DeactivateAndAddToPool(gameObject);
    }

    public void Init(Vector3 p, Vector3 r)
    {
        transform.position = p;
        transform.rotation = Quaternion.Euler(r);
    }
}
