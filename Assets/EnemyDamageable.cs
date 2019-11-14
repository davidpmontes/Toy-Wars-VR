using System.Collections;
using UnityEngine;

public class EnemyDamageable : MonoBehaviour, IEnemy
{
    private float life = 3;

    [SerializeField] private MeshRenderer[] meshRenderers;
    [SerializeField] private Material[] originalMaterials;
    [SerializeField] private Material flashRed;
    [SerializeField] private GameObject[] blownOffParts;
    [SerializeField] private GameObject firePoint;

    private void Awake()
    {
        originalMaterials = new Material[meshRenderers.Length];
        for (int i = 0; i < meshRenderers.Length; i++)
        {
            originalMaterials[i] = meshRenderers[i].sharedMaterials[0];
        }
    }

    public void DamageEnemy(Vector3 position)
    {
        if (life <= 0)
            return;

        life--;
        var explosion = ObjectPool.Instance.GetFromPoolInactive(Pools.CFX_Explosion_B_Smoke_Text);
        explosion.transform.position = position;
        explosion.SetActive(true);

        if (life <= 0)
        {
            //EnemyManager.Instance.DeregisterEnemy(gameObject);
            DestroySelf();
        }
        else
        {
            StartCoroutine(DamageFlash());
        }
    }

    IEnumerator DamageFlash()
    {
        for (int i = 0; i < meshRenderers.Length; i++)
        {
            meshRenderers[i].sharedMaterial = flashRed;
        }

        yield return new WaitForSeconds(0.1f);

        for (int i = 0; i < meshRenderers.Length; i++)
        {
            meshRenderers[i].sharedMaterial = originalMaterials[i];
        }
    }

    public void DestroyEnemy()
    {
        throw new System.NotImplementedException();
    }

    private void DestroySelf()
    {
        for (int i = 0; i < blownOffParts.Length; i++)
        {
            blownOffParts[i].SetActive(false);
        }
        var fire = ObjectPool.Instance.GetFromPoolInactive(Pools.CFX4Fire);
        fire.transform.SetParent(transform);
        fire.transform.position = transform.position;
        fire.SetActive(true);

        gameObject.layer = LayerMask.NameToLayer("DyingEnemy");
        var smoke = ObjectPool.Instance.GetFromPoolInactive(Pools.Smoke);
        smoke.transform.position = transform.position;
        smoke.transform.SetParent(transform);
        smoke.SetActive(true);
    }
}
