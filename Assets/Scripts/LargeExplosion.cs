using UnityEngine;

public class LargeExplosion : MonoBehaviour
{
    [SerializeField] private AudioClip explosionClip;
    private AudioManager audioManager;

    private int lifespan = 2;

    private void Awake()
    {
    }

    private void Start()
    {
        audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
    }

    private void OnEnable()
    {
        Invoke("Deactivate", lifespan);
        audioManager.PlayOneshot("explosion_large_01", transform, true, 1f, 1f, 1f);
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
