using UnityEngine;

public class PlayerTurret : MonoBehaviour, IRotateable, ICameraRelocate
{
    public static PlayerTurret Instance { get; private set; }

    private float horizontal;
    private float vertical; 

    [SerializeField] private GameObject Rotateable = default;
    [SerializeField] private GameObject Tiltable = default;
    [SerializeField] private Transform cameraPosition = default;

    private readonly float ROTATION_SPEED = 100f;
    private readonly float TILT_SPEED = 100f; 

    float rotationX = 15;

    private AudioSource[] audiosources;

    private void Awake()
    {
        audiosources = GetComponents<AudioSource>();
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
            var tiltableLookRotation = Quaternion.LookRotation(new Vector3(direction.x, direction.y, direction.z));

            Rotateable.transform.rotation = Quaternion.Slerp(Rotateable.transform.rotation, rotateableLookRotation, Time.deltaTime * 10);
            Tiltable.transform.rotation = Quaternion.Slerp(Tiltable.transform.rotation, tiltableLookRotation, Time.deltaTime * 10);
        }
    }

    private void MakeSound()
    {
        if (Mathf.Abs(horizontal) > 0 || Mathf.Abs(vertical) > 0)
        {
            if (!audiosources[1].isPlaying)
            {
                audiosources[1].Play();
            }
        }
        else
        {
            audiosources[1].Stop();
        }
    }

    //private void Rotate()
    //{
    //    Rotateable.transform.Rotate(Vector3.up, horizontal * Time.deltaTime * ROTATION_SPEED);
    //}

    //private void Tilt()
    //{
    //    rotationX -= vertical * Time.deltaTime * TILT_SPEED;
    //    rotationX = Mathf.Clamp(rotationX, -55, 55);

    //    Tiltable.transform.localEulerAngles = new Vector3(rotationX - 15, Tiltable.transform.localEulerAngles.y, Tiltable.transform.localEulerAngles.z);
    //}

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
