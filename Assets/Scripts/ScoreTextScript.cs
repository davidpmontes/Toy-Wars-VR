using UnityEngine;

public class ScoreTextScript : MonoBehaviour
{
    [SerializeField] private float lifespan = default;

    void OnEnable()
    {
        Invoke("Deactivate", lifespan);
    }

    void Deactivate()
    {
        ObjectPool.Instance.DeactivateAndAddToPool(gameObject);
    }
}
