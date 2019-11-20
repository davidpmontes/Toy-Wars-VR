using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    public void Drop()
    {
        GetComponent<Rigidbody>().isKinematic = false;
        transform.SetParent(null);
    }

    public void Init()
    {
        GetComponent<Rigidbody>().isKinematic = true;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent<IBaseAsset>(out IBaseAsset component))
        {
            component.TakeDamage(transform.position);
        }

        var explosion = ObjectPool.Instance.GetFromPoolInactive(Pools.CFX_Explosion_B_Smoke_Text);
        explosion.transform.position = transform.position;
        explosion.SetActive(true);
        ObjectPool.Instance.DeactivateAndAddToPool(gameObject);
    }
}
