using UnityEngine;

public class Explosion : MonoBehaviour
{
    private int lifespan = 1;
    private AudioManager audioManager;

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
        audioManager.PlayOneshot("explosion_large_01", transform, true, 1f, 1f, 0.5f);
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
