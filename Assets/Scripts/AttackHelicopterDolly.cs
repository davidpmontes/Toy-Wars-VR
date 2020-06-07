using System.Collections;
using UnityEngine;
using Cinemachine;

public class AttackHelicopterDolly : MonoBehaviour, IEnemy
{
    [SerializeField] private Material red = default;
    [SerializeField] private GameObject target = default;

    private float maxLife = 3;
    private float currlife;
    private MeshRenderer meshRenderer;
    private Rigidbody rigidBody;
    private CinemachineDollyCart cinemachineDollyCart;
    private Vector3 pos0;
    private Vector3 pos1;
    private GameObject smoke;
    private Material originalMaterial;
    private int sourceKey = -1;
    //private int cannonSource = -1;

    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody>();
        meshRenderer = GetComponentInChildren<MeshRenderer>();
        originalMaterial = meshRenderer.material;
        cinemachineDollyCart = GetComponent<CinemachineDollyCart>();
        cinemachineDollyCart.m_Position = 0;
    }

    private void BecomeVisible()
    {
        transform.GetChild(0).transform.localScale = Vector3.one;
    }

    public void Init()
    {
        BindAudio();
        currlife = maxLife;
        Invoke("BecomeVisible", 0.1f);
    }

    private void BindAudio()
    {
        if (AudioManager.Instance != null)
        {
            sourceKey = AudioManager.Instance.ReserveSource("helicopter_idle", occluding: true, spacial_blend: 1f, pitch: 1f, looping: true);
            AudioManager.Instance.SetReservedMixer(sourceKey, 3);
            AudioManager.Instance.BindReserved(sourceKey, transform);
            AudioManager.Instance.PlayReserved(sourceKey);
        }
    }

    private void UnbindAudio()
    {
        if (AudioManager.Instance != null && sourceKey == -1)
        {
            AudioManager.Instance.UnbindReserved(sourceKey);
            sourceKey = -1;
        }
    }

    private void Update()
    {
        StorePositions();
        CheckReachedEndOfPath();
    }

    public void DamageEnemy(Vector3 position)
    {
        if (currlife <= 0)
            return;

        currlife--;
        var explosion = ObjectPool.Instance.GetFromPoolInactive(Pools.CFX_Explosion_B_Smoke_Text);
        explosion.transform.GetComponent<Explosion>().Init(position);
        explosion.SetActive(true);

        if (currlife <= 0)
        {
            EnemyManager.Instance.DeregisterEnemyWithPoints(gameObject);
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
            UnbindAudio();
            var explosion = ObjectPool.Instance.GetFromPoolInactive(Pools.Large_CFX_Explosion_B_Smoke_Text);
            explosion.transform.GetComponent<Explosion>().Init(transform.position);
            explosion.SetActive(true);
            ObjectPool.Instance.DeactivateAndAddToPool(smoke);
            ObjectPool.Instance.DeactivateAndAddToPool(gameObject);
        }
    }

    private void StorePositions()
    {
        pos0 = pos1;
        pos1 = transform.position;
    }

    private void CheckReachedEndOfPath()
    {
        if (cinemachineDollyCart.m_Position > cinemachineDollyCart.m_Path.PathLength - 1)
        {
            UnbindAudio();
            EnemyManager.Instance.DeregisterEnemyNoPoints(gameObject);
            ObjectPool.Instance.DeactivateAndAddToPool(gameObject);
        }
    }

    public void FireWeapon()
    {
        StartCoroutine(FireWeaponInTime(0, 3));
    }

    IEnumerator FireWeaponInTime(float time, int repeat)
    {
        yield return new WaitForSeconds(time);

        for (int i = 0; i < repeat; i++)
        {
            //audioManager.PlayReserved(cannonSource);
            var enemyBullet = ObjectPool.Instance.GetFromPoolInactive(Pools.EnemyBullet);
            enemyBullet.GetComponent<EnemyBullet>().Init(transform, target.transform.position - transform.position);
            enemyBullet.SetActive(true);
            yield return new WaitForSeconds(0.2f);
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
