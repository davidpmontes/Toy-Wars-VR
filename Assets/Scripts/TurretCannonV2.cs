using UnityEngine;
using Valve.VR;

public class TurretCannonV2 : MonoBehaviour, ICameraRelocate
{
    [SerializeField] private Transform LeftAim = default;
    [SerializeField] private Transform RightAim = default;
    [SerializeField] private Transform barrelLeftTip = default;
    [SerializeField] private Transform barrelRightTip = default;
    [SerializeField] private ParticleSystem leftFlash = default;
    [SerializeField] private ParticleSystem rightFlash = default;
    [SerializeField] private Transform cameraPosition = default;

    public SteamVR_Action_Boolean fireAction;
    private AudioManager audioManager;
    private Animator animator;

    private float lastTimeFired;
    private float lastTimeSound;

    private int leftCannonSource = -1;
    private int rightCannonSource = -1;

    void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        LoadAudio();
    }

    private void LoadAudio()
    {
        audioManager = AudioManager.GetAudioManager();
        if (leftCannonSource < 0)
        {
            leftCannonSource = audioManager.ReserveSource("big one", occluding: true, spacial_blend: 1.0f, pitch: 1.0f);
            audioManager.BindReserved(leftCannonSource, barrelLeftTip);
        }

        if (rightCannonSource < 0)
        {
            rightCannonSource = audioManager.ReserveSource("big one", occluding: true, spacial_blend: 1.0f, pitch: 1.0f);
            audioManager.BindReserved(rightCannonSource, barrelRightTip);
        }
    }

    void Update()
    {
        GetVRInput();
    }

    private void GetVRInput()
    {
        if (!enabled)
            return;

        if (fireAction.state)
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
        if (!enabled)
            return;

        audioManager.PlayReserved(leftCannonSource);
        var turretBullet = ObjectPool.Instance.GetFromPoolInactive(Pools.PingPongBall);

        Vector3 direction = (LeftAim.position - barrelLeftTip.position).normalized;
        turretBullet.GetComponent<IProjectile>().Init(barrelLeftTip, direction.normalized);

        turretBullet.SetActive(true);

        leftFlash.Play();
    }

    private void SpawnBulletRight()
    {
        if (!enabled)
            return;

        audioManager.PlayReserved(rightCannonSource);
        var turretBullet = ObjectPool.Instance.GetFromPoolInactive(Pools.PingPongBall);

        Vector3 direction = (RightAim.position - barrelRightTip.position).normalized;
        turretBullet.GetComponent<IProjectile>().Init(barrelRightTip, direction.normalized);

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

    private void OnDestroy()
    {
        if (audioManager != null)
        {
            audioManager.UnbindReserved(leftCannonSource);
            audioManager.UnbindReserved(rightCannonSource);
        }
    }
}
