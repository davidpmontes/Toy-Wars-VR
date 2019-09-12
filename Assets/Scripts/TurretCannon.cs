using UnityEngine;
using Valve.VR;

public class TurretCannon : MonoBehaviour, ICameraRelocate
{
    [SerializeField] private Transform LeftAim;
    [SerializeField] private Transform RightAim;
    [SerializeField] private Transform barrelLeftTip;
    [SerializeField] private Transform barrelRightTip;
    [SerializeField] private ParticleSystem leftFlash;
    [SerializeField] private ParticleSystem rightFlash;
    [SerializeField] private Transform cameraPosition;

    public SteamVR_Action_Boolean fireAction;

    [SerializeField] private AudioClip shoot;
    private AudioSource[] audiosources;

    private Animator animator;

    private float lastTimeFired;
    private float lastTimeSound;
    
    void Awake()
    {
        audiosources = GetComponents<AudioSource>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        //GetKeyboardInput();
        GetVRInput();
    }

    private void GetVRInput()
    {
        if (fireAction.state)
        {
            animator.SetBool("firing", true);
        }
        else
        {
            animator.SetBool("firing", false);
        }
    }

    private void GetKeyboardInput()
    {
        if (InputController.Button4())
        {
            animator.SetBool("firing", true);
        }
        else
        {
            animator.SetBool("firing", false);
        }
    }

    private void SpawnBulletLeft()
    {
        var turretBullet = ObjectPool.Instance.GetFromPoolInactive(Pools.PingPongBall);

        Vector3 direction = (LeftAim.position - barrelLeftTip.position).normalized;
        turretBullet.GetComponent<Projectile>().Init(barrelLeftTip, direction.normalized);

        turretBullet.SetActive(true);

        audiosources[0].PlayOneShot(shoot);
        leftFlash.Play();
    }

    private void SpawnBulletRight()
    {
        var turretBullet = ObjectPool.Instance.GetFromPoolInactive(Pools.PingPongBall);

        Vector3 direction = (RightAim.position - barrelRightTip.position).normalized;
        turretBullet.GetComponent<Projectile>().Init(barrelRightTip, direction.normalized);

        turretBullet.SetActive(true);

        audiosources[0].PlayOneShot(shoot);
        rightFlash.Play();
    }

    public Vector3 GetRelocatePosition()
    {
        return cameraPosition.position;
    }

    public float GetRelocateRotation()
    {
        return transform.rotation.eulerAngles.y;
    }
}
