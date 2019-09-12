using UnityEngine;

public class PlayerTurret : MonoBehaviour, IRotateable
{
    private float horizontal;
    private float vertical;

    [SerializeField] private GameObject Rotateable;
    [SerializeField] private GameObject Tiltable;

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
        //GetInput();
        //MakeSound();
        //Rotate();
        //Tilt();
    }

    private void VRTrack()
    {
        GameObject targetPoint = VRAimer.Instance.GetTargetPoint();

        if (targetPoint.activeSelf)
        {
            var direction = (targetPoint.transform.position - transform.position).normalized;
            var lookRotation = Quaternion.LookRotation(direction);
            Rotateable.transform.rotation = Quaternion.Slerp(Rotateable.transform.rotation, lookRotation, Time.deltaTime * 10);
        }
    }

    private void GetInput()
    {
        horizontal = InputController.Horizontal();
        vertical = InputController.Vertical();
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

    private void Rotate()
    {
        Rotateable.transform.Rotate(Vector3.up, horizontal * Time.deltaTime * ROTATION_SPEED);
    }

    private void Tilt()
    {
        rotationX -= vertical * Time.deltaTime * TILT_SPEED;
        rotationX = Mathf.Clamp(rotationX, -55, 55);

        Tiltable.transform.localEulerAngles = new Vector3(rotationX - 15, Tiltable.transform.localEulerAngles.y, Tiltable.transform.localEulerAngles.z);
    }

    public GameObject GetRotateable()
    {
        return Rotateable;
    }
}
