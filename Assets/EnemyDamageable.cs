using System.Collections;
using UnityEngine;

public class EnemyDamageable : MonoBehaviour, IEnemy
{
    [SerializeField] private MeshRenderer[] meshRenderers;
    [SerializeField] private Material[] originalMaterials;
    [SerializeField] private Material flashRed;

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
        StartCoroutine(DamageFlash());
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
}
