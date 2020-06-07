using UnityEngine;

public class Explosion : MonoBehaviour
{
    public float lifespan = 2f;
    public string audiofileName;

    public void Init(Vector3 position)
    {
        transform.position = position;
        AudioManager.Instance.PlayOneshot(audiofileName, transform.position);
        Invoke("Deactivate", lifespan);
    }

    void Deactivate()
    {
        ObjectPool.Instance.DeactivateAndAddToPool(gameObject);
    }
}
