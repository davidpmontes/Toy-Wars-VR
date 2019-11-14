using System.Collections;
using UnityEngine;
using Cinemachine;

public class Spitfire : MonoBehaviour, IEnemy
{
    private float life = 3;
    private Material originalMaterial;
    private Material material;
    [SerializeField] private Material red = default;
    private MeshRenderer meshRenderer;
    private Rigidbody rigidBody;
    private GameObject smoke;
    private Vector3 pos0;
    private Vector3 pos1;
    private AudioManager audioManager;
    private CinemachineDollyCart cinemachineDollyCart;
    private int sourceKey = -1;

    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody>();
        meshRenderer = GetComponentInChildren<MeshRenderer>();
        originalMaterial = meshRenderer.material;
        audioManager = AudioManager.GetAudioManager();
        cinemachineDollyCart = GetComponent<CinemachineDollyCart>();
    }
    private void Start()
    {
        if (audioManager != null)
        {
            sourceKey = audioManager.ReserveSource("engine_generator_loop_03", occluding: true, spacial_blend: 1f, pitch: 1f, looping: true);
            audioManager.SetReservedMixer(sourceKey, 3);
            audioManager.BindReserved(sourceKey, this.transform);
            audioManager.PlayReserved(sourceKey);
        }
    }

    private void Update()
    {
        StorePositions();
        CheckReachedEndOfPath();
    }

    private void CheckReachedEndOfPath()
    {
        if (cinemachineDollyCart.m_Position > cinemachineDollyCart.m_Path.PathLength - 1)
        {
            EnemyManager.Instance.DeregisterEnemy(gameObject);
            ObjectPool.Instance.DeactivateAndAddToPool(gameObject);
        }
    }

    public void DamageEnemy(Vector3 position)
    {
        if (life <= 0)
            return;

        life--;
        var explosion = ObjectPool.Instance.GetFromPoolInactive(Pools.CFX_Explosion_B_Smoke_Text);
        explosion.transform.position = position;
        explosion.transform.GetComponent<Explosion>().Init();
        explosion.SetActive(true);

        if (life <= 0)
        {
            EnemyManager.Instance.DeregisterEnemy(gameObject);
            DestroySelf();
        }
        else
        {
            StartCoroutine(FlashRed());
        }
    }

    IEnumerator FlashRed()
    {
        meshRenderer.material = red;
        yield return new WaitForSeconds(0.05f);
        meshRenderer.material = originalMaterial;
    }

    private void StorePositions()
    {
        pos0 = pos1;
        pos1 = transform.position;
    }

    private void DestroySelf()
    {
        gameObject.layer = LayerMask.NameToLayer("DyingEnemy");
        cinemachineDollyCart.enabled = false;
        rigidBody.isKinematic = false;
        rigidBody.velocity = (pos1 - pos0) / Time.deltaTime;
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
            explosion.transform.position = transform.position;
            explosion.transform.GetComponent<Explosion>().Init();
            explosion.SetActive(true);
            ObjectPool.Instance.DeactivateAndAddToPool(smoke);
            ObjectPool.Instance.DeactivateAndAddToPool(gameObject);
        }
    }

    public void DestroyEnemy()
    {
        throw new System.NotImplementedException();
    }
}