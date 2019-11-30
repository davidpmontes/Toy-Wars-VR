using UnityEngine;

public class BaseAsset : MonoBehaviour, IBaseAsset
{
    [SerializeField] private float life;
    //private LifeBar lifeBar;

    public void Awake()
    {
        //lifeBar = GetComponentInChildren<LifeBar>();
        //lifeBar.SetMaxLifeAndCurrLife(life);
    }

    public void TakeDamage(Vector3 position)
    {
        //life--;
        //lifeBar.ReduceLife(1);
        var explosion = ObjectPool.Instance.GetFromPoolInactive(Pools.CFX_Explosion_B_Smoke_Text);
        explosion.transform.GetComponent<Explosion>().Init(position);
        explosion.SetActive(true);

        if (life <= 0)
        {
            DestroySelf();
        }
    }

    private void DestroySelf()
    {
        var largeExplosion = ObjectPool.Instance.GetFromPoolInactive(Pools.Large_CFX_Explosion_B_Smoke_Text);
        largeExplosion.transform.position = transform.position;
        largeExplosion.SetActive(true);
        BaseAssetManager.Instance.DeregisterBaseAsset(gameObject);
        Destroy(gameObject);
    }
}
