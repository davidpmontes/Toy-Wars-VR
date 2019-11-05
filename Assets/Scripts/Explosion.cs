using UnityEngine;

public class Explosion : MonoBehaviour
{
    public float lifespan = 2f;

    public void Init()
    {
        AudioManager.GetAudioManager().PlayOneshot("explosion_large_04", transform.position);
        Invoke("Deactivate", lifespan);
    }

    void Deactivate()
    {
        ObjectPool.Instance.DeactivateAndAddToPool(gameObject);
    }
}
