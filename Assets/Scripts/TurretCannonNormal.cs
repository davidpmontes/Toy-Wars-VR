using UnityEngine;
using UnityEngine.InputSystem;

public class TurretCannonNormal : MonoBehaviour, ICameraRelocate
{
    [SerializeField] private Transform LeftAim = default;
    [SerializeField] private Transform RightAim = default;
    [SerializeField] private Transform barrelLeftTip = default;
    [SerializeField] private Transform barrelRightTip = default;
    [SerializeField] private ParticleSystem leftFlash = default;
    [SerializeField] private ParticleSystem rightFlash = default;
    [SerializeField] private Transform cameraPosition = default;
    [SerializeField] private GameObject NormalAimerHorizontal = default;
    [SerializeField] private GameObject NormalAimerVertical = default;

    private float horizontalValue = 0;
    private float verticalValue = 0;

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
        audioManager = AudioManager.Instance;
        if (leftCannonSource < 0)
        {
            leftCannonSource = audioManager.ReserveSource("big one", occluding: true, spacial_blend: 1.0f, pitch: 1.0f);
            audioManager.BindReserved(leftCannonSource, barrelLeftTip);
            audioManager.SetReservedMixer(leftCannonSource, 2);
        }

        if (rightCannonSource < 0)
        {
            rightCannonSource = audioManager.ReserveSource("big one", occluding: true, spacial_blend: 1.0f, pitch: 1.0f);
            audioManager.BindReserved(rightCannonSource, barrelRightTip);
            audioManager.SetReservedMixer(rightCannonSource, 2);

        }
    }

    void Update()
    {
        //GetVRInput();
        GetGamepadInput();
    }

    private void GetVRInput()
    {
        if (!enabled)
            return;
    }

    private void GetGamepadInput()
    {
        if (!enabled)
            return;

        var horizontal = Gamepad.current.leftStick.x.ReadValue();
        var vertical = Gamepad.current.leftStick.y.ReadValue();

        horizontalValue += horizontal * 70 * Time.deltaTime;
        verticalValue += vertical * 70 * Time.deltaTime;

        horizontalValue = Mathf.Clamp(horizontalValue, -90, 90);
        verticalValue = Mathf.Clamp(verticalValue, -90, 90);

        NormalAimerHorizontal.transform.localRotation = Quaternion.Euler(0, horizontalValue, 0);
        NormalAimerVertical.transform.localRotation = Quaternion.Euler(verticalValue, 0, 0);
        
        if (Gamepad.current.buttonSouth.isPressed)
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
