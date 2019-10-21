using UnityEngine;
using Valve.VR;

public class TurretCannon : MonoBehaviour, ICameraRelocate
{
    [SerializeField] private Transform LeftAim = default;
    [SerializeField] private Transform RightAim = default;
    [SerializeField] private Transform barrelLeftTip = default;
    [SerializeField] private Transform barrelRightTip = default;
    [SerializeField] private ParticleSystem leftFlash = default;
    [SerializeField] private ParticleSystem rightFlash = default;
    [SerializeField] private Transform cameraPosition = default;

    public SteamVR_Action_Boolean fireAction;
    private AudioSource audioSource;
    [SerializeField] AudioClip bang = default;

    private Animator animator;

    private float lastTimeFired;
    private float lastTimeSound;
    
    void Awake()
    {
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        GetVRInput();
    }

    private void GetVRInput()
    {
        if (fireAction.state)
        {
            animator.SetBool("firing", true);
        } else if (Input.GetKey(KeyCode.Space))
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
        audioSource.PlayOneShot(bang);
        var turretBullet = ObjectPool.Instance.GetFromPoolInactive(Pools.PingPongBall);

        Vector3 direction = (LeftAim.position - barrelLeftTip.position).normalized;
        turretBullet.GetComponent<Projectile>().Init(barrelLeftTip, direction.normalized);

        turretBullet.SetActive(true);

        leftFlash.Play();
    }

    private void SpawnBulletRight()
    {
        audioSource.PlayOneShot(bang);
        var turretBullet = ObjectPool.Instance.GetFromPoolInactive(Pools.PingPongBall);

        Vector3 direction = (RightAim.position - barrelRightTip.position).normalized;
        turretBullet.GetComponent<Projectile>().Init(barrelRightTip, direction.normalized);

        turretBullet.SetActive(true);

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
