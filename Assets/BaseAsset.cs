using System.Collections;
using UnityEngine;

public class BaseAsset : MonoBehaviour, IBaseAsset
{
    private float life = 3;
    private Material originalMaterial;
    private Material material;
    [SerializeField] private Material red;
    private MeshRenderer meshRenderer;

    private void Awake()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        originalMaterial = GetComponent<MeshRenderer>().material;
    }

    public void TakeDamage(Vector3 position)
    {
        life--;
        var explosion = ObjectPool.Instance.GetFromPoolInactive(Pools.CFX_Explosion_B_Smoke_Text);
        explosion.transform.position = position;
        explosion.SetActive(true);

        if (life <= 0)
        {
            DestroySelf();
        }
    }

    private void DestroySelf()
    {
        var smoke = ObjectPool.Instance.GetFromPoolInactive(Pools.Smoke);
        smoke.transform.position = transform.position;
        smoke.transform.SetParent(transform);
        smoke.SetActive(true);
        Destroy(gameObject);
    }
}
