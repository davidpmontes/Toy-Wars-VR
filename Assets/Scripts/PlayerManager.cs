using UnityEngine;

public enum PlayerVehicles
{
    helicopter,
    turret,
    plane,
    turretVR
}

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager Instance;
    private GameObject currentVehicle;

    [SerializeField] private GameObject helicopter;
    [SerializeField] private GameObject turret;
    [SerializeField] private GameObject plane;
    [SerializeField] private GameObject turretVR;

    private GameObject rotateable;

    void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        EnableVehicle(PlayerVehicles.turretVR);
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
            EnableVehicle(PlayerVehicles.helicopter);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            EnableVehicle(PlayerVehicles.turret);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            EnableVehicle(PlayerVehicles.plane);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            EnableVehicle(PlayerVehicles.turretVR);
        }
    }

    public void EnableVehicle(PlayerVehicles vehicle)
    {
        //helicopter.SetActive(false);
        turret.SetActive(false);
        plane.SetActive(false);
        turretVR.SetActive(false);

        if (vehicle == PlayerVehicles.helicopter)
        {
            helicopter.SetActive(true);
            currentVehicle = helicopter;
        }
        else if (vehicle == PlayerVehicles.turret)
        {
            turret.SetActive(true);
            currentVehicle = turret;
        }
        else if (vehicle == PlayerVehicles.plane)
        {
            plane.SetActive(true);
            currentVehicle = plane;
        }
        else if (vehicle == PlayerVehicles.turretVR)
        {
            turretVR.SetActive(true);
            currentVehicle = turretVR;
        }

        //CameraRigSetPosition.Instance.Relocate(currentVehicle.GetComponent<ICameraRelocate>().GetRelocatePosition(), currentVehicle.GetComponent<ICameraRelocate>().GetRelocateRotation());

        rotateable = currentVehicle.GetComponent<IRotateable>().GetRotateable();
        //AimingCircle.Instance.SetTrueAimingTransform(currentVehicle.transform);
        //FollowTarget.Instance.SetCameraTargets(currentVehicle.transform);
    }
}
