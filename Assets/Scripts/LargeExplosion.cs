using UnityEngine;

public class LargeExplosion : MonoBehaviour
{
    [SerializeField] private AudioClip explosionClip;
    private AudioSource audioSource;

    private int lifespan = 2;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void OnEnable()
    {
        Invoke("Deactivate", lifespan);
        audioSource.PlayOneShot(explosionClip);
    }

    void Deactivate()
    {
        ObjectPool.Instance.DeactivateAndAddToPool(gameObject);
    }

    public void Init(Transform t)
    {
        transform.position = t.position;
        transform.rotation = t.rotation;
    }

    public void Init(Vector3 p, Vector3 r)
    {
        transform.position = p;
        transform.rotation = Quaternion.Euler(r);
    }
}
