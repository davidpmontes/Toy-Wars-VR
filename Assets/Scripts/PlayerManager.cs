using UnityEngine;

public enum PlayerVehicles
{
    turretVR_A,
    turretVR_B,
    TankA
}

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager Instance;
    private GameObject currentVehicle;

    [SerializeField] private GameObject turretVR_A = default;
    [SerializeField] private GameObject turretVR_B = default;
    [SerializeField] private GameObject tankVR_A = default;

    void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        EnableVehicle(PlayerVehicles.turretVR_A);
        EnableVehicle(PlayerVehicles.TankA);
    }

    public GameObject CurrentVehicle()
    {
        return currentVehicle;
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
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            EnableVehicle(PlayerVehicles.TankA);
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
        else if (vehicle == PlayerVehicles.TankA)
        {
            tankVR_A.SetActive(true);
            currentVehicle = tankVR_A;
        }

        SetCameraToVehicle();

        //rotateable = currentVehicle.GetComponent<IRotateable>().GetRotateable();

        //AimingCircle.Instance.SetTrueAimingTransform(currentVehicle.transform);
        //FollowTarget.Instance.SetCameraTargets(currentVehicle.transform);
    }
}
