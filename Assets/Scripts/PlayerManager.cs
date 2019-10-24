using UnityEngine;

public enum PlayerVehicles
{
    turretVR_A,
    turretVR_B
}

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager Instance;
    private GameObject currentVehicle;
    private AudioManager audioManager;

    [SerializeField] private GameObject turretVR_A;
    [SerializeField] private GameObject turretVR_B;

    private GameObject rotateable;

    void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
        audioManager.LoadClip(@"Audio\SFX\explosion_large_01");
        EnableVehicle(PlayerVehicles.turretVR_A);
    }

    public GameObject CurrentVehicle()
    {
        return currentVehicle;
    }

    public GameObject CurrentRotateable()
    {
        return rotateable;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            EnableVehicle(PlayerVehicles.turretVR_A);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            EnableVehicle(PlayerVehicles.turretVR_B);
        }
    }

    private void SetCameraToVehicle()
    {
        CameraRigSetPosition.Instance.Relocate(currentVehicle.GetComponent<ICameraRelocate>().GetRelocatePosition(),
                                       currentVehicle.GetComponent<ICameraRelocate>().GetRelocateRotation());
        CameraRigSetPosition.Instance.AttachToGameobject(currentVehicle.transform);
    }

    public void EnableVehicle(PlayerVehicles vehicle)
    {
        turretVR_A.SetActive(false);
        turretVR_B.SetActive(false);

        if (vehicle == PlayerVehicles.turretVR_A)
        {
            turretVR_A.SetActive(true);
            currentVehicle = turretVR_A;
        }
        else if (vehicle == PlayerVehicles.turretVR_B)
        {
            turretVR_B.SetActive(true);
            currentVehicle = turretVR_B;
        }

        SetCameraToVehicle();

        //rotateable = currentVehicle.GetComponent<IRotateable>().GetRotateable();

        //AimingCircle.Instance.SetTrueAimingTransform(currentVehicle.transform);
        //FollowTarget.Instance.SetCameraTargets(currentVehicle.transform);
    }
}
