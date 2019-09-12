using UnityEngine;

public class Explosion : MonoBehaviour
{
    private int lifespan = 1;
    [SerializeField] private AudioClip explosionClip;
    private AudioSource audiosource;

    private void Awake()
    {
        audiosource = GetComponent<AudioSource>();
    }

    private void OnEnable()
    {
        Invoke("Deactivate", lifespan);
        audiosource.PlayOneShot(explosionClip);
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
