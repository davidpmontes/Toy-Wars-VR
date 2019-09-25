using UnityEngine;
using Valve.VR;

public class Cannon : MonoBehaviour
{
    public SteamVR_Action_Boolean trigger;

    [SerializeField] private float delay = 0.5f;
    [SerializeField] private float soundDelay = 0.2f;
    [SerializeField] private GameObject muzzleFlash;

    private MuzzleFlash muzzleFlashScript;

    public Transform AimingLocation;

    private float lastTimeFired;
    private float lastTimeSound;

    //private int layerMask;
    private AudioSource audioSource;

    [SerializeField] private AudioClip cannonBulletAudio;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void Start()
    {
        muzzleFlashScript = muzzleFlash.GetComponent<MuzzleFlash>();
        muzzleFlashScript.TurnOffLight();
        //layerMask = //1 << LayerMask.NameToLayer("Statics") |
                    //1 << LayerMask.NameToLayer("Enemy");
    }

    void Update()
    {
        //GetInput();
        GetInputVR();
    }

    void GetInputVR()
    {
        if (trigger.state)
        {
            if (Time.time - lastTimeFired > delay)
            {
                lastTimeFired = Time.time;
                muzzleFlashScript.TurnOnLight();
                SpawnBullet();
            }

            //if (Time.time - lastTimeSound > soundDelay)
            //{
            //    lastTimeSound = Time.time;
            //    audioSource.PlayOneShot(cannonBulletAudio);
            //}
        }
    }

    void GetInput()
    {
        if (InputController.Button4())
        {
            if (Time.time - lastTimeFired > delay)
            {
                lastTimeFired = Time.time;
                muzzleFlashScript.TurnOnLight();
                SpawnBullet();
            }

            if (Time.time - lastTimeSound > soundDelay)
            {
                lastTimeSound = Time.time;
                audioSource.PlayOneShot(cannonBulletAudio);
            }
        }
    }

    void SpawnBullet()
    {
        Vector3 direction;

        //var nearestTarget = EnemyManager.Instance.GetNearestTarget();

        //if (nearestTarget == null)
        //{
            direction = (AimingLocation.position - transform.position).normalized;
        //}
        //else
        //{
        //    direction = (nearestTarget.transform.position - transform.position).normalized;
        //}

        var cannonBullet = ObjectPool.Instance.GetFromPoolInactive(Pools.CannonBullet);
        cannonBullet.GetComponent<Projectile>().Init(transform, direction.normalized);
        cannonBullet.SetActive(true);
        Debug.Log("cannonBulletActive");
    }
}
