using UnityEngine;

public class PlayerTurret : MonoBehaviour, IRotateable, ICameraRelocate
{
    public static PlayerTurret Instance { get; private set; }

    [SerializeField] private GameObject Rotateable = default;
    [SerializeField] private GameObject Tiltable = default;
    [SerializeField] private Transform cameraPosition = default;
    [SerializeField] private TurretCannonV2 pingpong = default;
    [SerializeField] private LaserCannon laserCannon = default;

    private void Awake()
    {
        Instance = this;
    }

    void Update()
    {
        VRTrack();
    }

    private void VRTrack()
    {
        GameObject targetPoint = VRAimer.Instance.GetTargetPoint();


        if (targetPoint.activeSelf)
        {
            var direction = (targetPoint.transform.position - transform.position).normalized;
            var rotateableLookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
            var tiltableLookRotation = Quaternion.LookRotation(new Vector3(direction.x,
                                                                           direction.y,
                                                                           direction.z));

            Rotateable.transform.rotation = Quaternion.Slerp(Rotateable.transform.rotation,
                                                             rotateableLookRotation,
                                                             Time.deltaTime * 10);
            Tiltable.transform.rotation = Quaternion.Slerp(Tiltable.transform.rotation,
                                                           tiltableLookRotation,
                                                           Time.deltaTime * 10);
        }
    }

    public void ToggleWeapon()
    {
        if (pingpong.enabled)
        {
            pingpong.enabled = false;
            laserCannon.enabled = true;
        }
        else
        {
            pingpong.enabled = true;
            laserCannon.enabled = false;
        }
    }

    public GameObject GetRotateable()
    {
        return Rotateable;
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
