using UnityEngine;

public class Explosion : MonoBehaviour
{
    public float lifespan = 2f;
    public string audiofileName;

    public void Init()
    {
        AudioManager.GetAudioManager().PlayOneshot(audiofileName, transform.position);
        Invoke("Deactivate", lifespan);
    }

    void Deactivate()
    {
        ObjectPool.Instance.DeactivateAndAddToPool(gameObject);
    }
}
