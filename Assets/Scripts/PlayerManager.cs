using UnityEngine;

public enum PlayerVehicles
{
    turretVR,
    tank
}

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager Instance;
    private GameObject currentVehicle;

    [SerializeField] private GameObject turretVR;
    [SerializeField] private GameObject tank;

    private GameObject rotateable;

    void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        EnableVehicle(PlayerVehicles.tank);
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
            EnableVehicle(PlayerVehicles.turretVR);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            EnableVehicle(PlayerVehicles.tank);
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
        turretVR.SetActive(false);
        tank.SetActive(false);

        if (vehicle == PlayerVehicles.turretVR)
        {
            turretVR.SetActive(true);
            currentVehicle = turretVR;
        }
        else if (vehicle == PlayerVehicles.tank)
        {
            tank.SetActive(true);
            currentVehicle = tank;
        }

        SetCameraToVehicle();

        //rotateable = currentVehicle.GetComponent<IRotateable>().GetRotateable();

        //AimingCircle.Instance.SetTrueAimingTransform(currentVehicle.transform);
        //FollowTarget.Instance.SetCameraTargets(currentVehicle.transform);
    }
}
