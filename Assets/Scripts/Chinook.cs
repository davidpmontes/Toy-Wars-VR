﻿using System.Collections;
using UnityEngine;

public class Chinook : MonoBehaviour, IEnemy
{
    private float life = 3;
    private bool isShaking = false;
    private Material originalMaterial;
    private Material material;
    [SerializeField] private Material red = default;
    private MeshRenderer meshRenderer;
    private GameObject smoke;
    private AudioManager audioManager;
    private int sourceKey = -1;

    private void Awake()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        originalMaterial = GetComponent<MeshRenderer>().material;
        audioManager = AudioManager.Instance;
    }

    public void Init()
    {
        if (audioManager != null)
        {
            sourceKey = audioManager.ReserveSource("helicopter_idle", occluding: true, spacial_blend: 1f, pitch: 1f, looping: true);
            audioManager.SetReservedMixer(sourceKey, 3);
            audioManager.BindReserved(sourceKey, this.transform);
            audioManager.PlayReserved(sourceKey);
        }
    }

    public void DamageEnemy(Vector3 position)
    {
        life--;
        var explosion = ObjectPool.Instance.GetFromPoolInactive(Pools.CFX_Explosion_B_Smoke_Text);
        explosion.transform.position = position;
        explosion.SetActive(true);

        if (life <= 0)
        {
            EnemyManager.Instance.DeregisterEnemyWithPoints(gameObject);
            DestroySelf();
        }
        else
        {
            if (!isShaking)
            {
                isShaking = true;
                StartCoroutine(DamageShake());
                StartCoroutine(FlashRed());
            }
        }
    }

    IEnumerator DamageShake()
    {
        Vector3 oldPosition = transform.localPosition;
        transform.localPosition = transform.localPosition + Random.insideUnitSphere * 2;

        while (Vector3.Distance(transform.localPosition, oldPosition) > 0.1)
        {
            transform.localPosition = Vector3.MoveTowards(transform.localPosition, oldPosition, Time.deltaTime * 20);
            yield return null;
        }
        transform.localPosition = oldPosition;
        isShaking = false;
    }

    IEnumerator FlashRed()
    {
        meshRenderer.material = red;
        yield return new WaitForSeconds(0.05f);
        meshRenderer.material = originalMaterial;
    }

    private void DestroySelf()
    {
        gameObject.layer = LayerMask.NameToLayer("DyingEnemy");
        var rigidBody = gameObject.AddComponent<Rigidbody>();
        rigidBody.AddRelativeTorque(new Vector3(Random.Range(1, 3), Random.Range(-3, 3), Random.Range(1, 2)), ForceMode.Impulse);
        smoke = ObjectPool.Instance.GetFromPoolInactive(Pools.Smoke);
        smoke.transform.position = transform.position;
        smoke.transform.SetParent(transform);
        smoke.SetActive(true);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (gameObject.layer == LayerMask.NameToLayer("DyingEnemy") && collision.gameObject.layer == LayerMask.NameToLayer("Statics"))
        {
            if (audioManager != null)
            {
                audioManager.UnbindReserved(sourceKey);
                sourceKey = -1;
            }
            var explosion = ObjectPool.Instance.GetFromPoolInactive(Pools.Large_CFX_Explosion_B_Smoke_Text);
            explosion.transform.GetComponent<Explosion>().Init(transform.position);
            explosion.SetActive(true);
            ObjectPool.Instance.DeactivateAndAddToPool(smoke);
            ObjectPool.Instance.DeactivateAndAddToPool(gameObject);
        }
    }

    public void DestroyEnemy()
    {
    }

    public bool IsVulnerable()
    {
        return true;
    }

    public void SetVulnerability(bool value)
    {
    }
}
