using UnityEngine;
using Valve.VR;
using System.Collections;
using System.Collections.Generic;

public class PlayerTankCannon : MonoBehaviour
{
    public SteamVR_Action_Boolean fireAction;

    [SerializeField] private Transform spawnPosition = default;
    [SerializeField] private Transform aimingPoint = default;
    [SerializeField] private GameObject flash_prefab = default;
    [SerializeField] ParticleSystem MuzzleFlash = default;
    [SerializeField] float cooldown_time = default;
    bool firing;

    private AudioManager audio_manager;
    private int key = -1;
    private bool cooldown = false;
    void Update()
    {
        GetVRInput();
    }

    private void Awake()
    {
        audio_manager = AudioManager.Instance;
    }

    private void Start()
    {
        MuzzleFlash = Instantiate(flash_prefab, spawnPosition).GetComponent<ParticleSystem>();
        MuzzleFlash.transform.position = spawnPosition.position;
        MuzzleFlash.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
        key = audio_manager.ReserveSource("big one", true, 1, 1, false);
        audio_manager.SetReservedMixer(key, 2);
        audio_manager.BindReserved(key, transform);
    }

    private void GetVRInput()
    {
        if (fireAction.state && !cooldown)
        {
            SpawnBullet();
        }
    }

    private void SpawnBullet()
    {
            cooldown = true;
            audio_manager.PlayReserved(key);
            var turretBullet = ObjectPool.Instance.GetFromPoolInactive(Pools.PingPongBall);

            Vector3 direction = (aimingPoint.position - spawnPosition.position).normalized;
            turretBullet.GetComponent<Projectile>().Init(spawnPosition, direction.normalized);

            turretBullet.SetActive(true);
            MuzzleFlash.Play();
            StartCoroutine(Cooldown());
    }

    private IEnumerator Cooldown()
    {
        yield return new WaitForSeconds(cooldown_time);
        cooldown = false;
    }
}
