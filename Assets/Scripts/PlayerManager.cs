using UnityEngine;

public enum PlayerVehicles
{
    TURRET,
    TANK
}

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager Instance { get; private set; }
    private GameObject currentVehicle;

    [SerializeField] private GameObject turret = default;
    [SerializeField] private GameObject tank = default;

    void Awake()
    {
        Instance = this;
    }

    public GameObject CurrentVehicle()
    {
        return currentVehicle;
    }

    private void Update()
    {
        //if (Input.GetKeyDown(KeyCode.Alpha1))
        //{
        //    EnableVehicle(PlayerVehicles.TURRET);
        //}
        //else if (Input.GetKeyDown(KeyCode.Alpha2))
        //{
        //    EnableVehicle(PlayerVehicles.TANK);
        //}
    }

    private void SetCameraToVehicle()
    {
        CameraRigSetPosition.Instance.Relocate(currentVehicle.GetComponent<ICameraRelocate>().GetRelocatePosition(),
                                       currentVehicle.GetComponent<ICameraRelocate>().GetRelocateRotation());
        if(currentVehicle != tank)
        {
            CameraRigSetPosition.Instance.AttachToGameobject(currentVehicle.transform);
        }
    }

    public void EnableVehicle(PlayerVehicles vehicle)
    {
        turret.SetActive(false);
        tank.SetActive(false);

        if (vehicle == PlayerVehicles.TURRET)
        {
            turret.SetActive(true);
            currentVehicle = turret;
        }
        else if (vehicle == PlayerVehicles.TANK)
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
