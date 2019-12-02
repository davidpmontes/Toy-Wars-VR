using UnityEngine;
using Valve.VR;
public enum DriveScheme {FreeTurret, lockedTurret}

/**List of needed assets for tank room:
 * Light Switch, Gameboy?, TV?, fireworks?, gate hinge?, chain link fence?, general metal clang?
 * **/

public class TankDrive : MonoBehaviour, ICameraRelocate
{
    public SteamVR_Action_Vector2 TouchPadPosition;
    public SteamVR_Action_Boolean click;

    [SerializeField] private Transform turret = default;
    [SerializeField] private Transform tracks = default;
    [SerializeField] private Transform cameraPosition = default;

    [SerializeField] private Transform body_transform = default;
    [SerializeField] private Rigidbody body_rb = default;
    [SerializeField] private Transform left_tread = default;
    [SerializeField] private Transform right_tread = default;

    [SerializeField] private float max_velocity = default;
    [SerializeField] private float max_rotation = default;
    [SerializeField] private float accel_scale = default;
    [SerializeField] private float torque_scale = default;
    [SerializeField] private float max_accel = default;
    [SerializeField] private float max_torque = default;

    [SerializeField] private Transform lf_raypoint = default;
    [SerializeField] private Transform lb_raypoint = default;
    [SerializeField] private Transform rf_raypoint = default;
    [SerializeField] private Transform rb_raypoint = default;

    private Vector3 left_tread_speed;
    private Vector3 right_tread_speed;
    private Vector2 pad_position;
    private bool moving = false;

    private Vector3 drag_force;
    private Vector3 drag_direction;

    private Transform cam;
    private Vector3 rel_cam_angle;
    private Vector3 target_point;

    private SteamVR_Behaviour_Pose pose = null;

    private Rigidbody rb;
    private Vector3 gravityDirection;

    private DriveScheme drive_scheme;
    private AudioManager audio_manager;
    private int key = -1;

    private RaycastHit hit;
    private int mask;

    void Awake()
    {
        cam = GameObject.FindGameObjectWithTag("MainCamera").transform;
        rb = GetComponent<Rigidbody>();
        pose = GetComponentInParent<SteamVR_Behaviour_Pose>();
        SetDriveScheme(DriveScheme.FreeTurret);
        audio_manager = AudioManager.GetAudioManager();
    }

    private void Start()
    {
        key = audio_manager.ReserveSource("engine_generator_loop_01", true, 1, 1, true);
        audio_manager.SetReservedMixer(key, 2);
        audio_manager.BindReserved(key, body_transform);
        audio_manager.PlayReserved(key);
        mask = 1<<LayerMask.NameToLayer("Statics");
    }

    private void OnEnable()
    {
        if(key != -1)
        {
            audio_manager.PlayReserved(key);
        }
        click.AddOnStateDownListener(ClickDown, SteamVR_Input_Sources.Any);
        click.AddOnStateUpListener(ClickUp, SteamVR_Input_Sources.Any);
    }

    private void Update()
    {
        GetInput();
    }

    void SetDriveScheme(DriveScheme scheme)
    {
        drive_scheme = scheme;
        if(scheme == DriveScheme.FreeTurret)
        {
            rb.constraints = RigidbodyConstraints.FreezeRotationY;
            rb.position = body_rb.position;
            body_transform.GetComponent<ConfigurableJoint>().angularYMotion = ConfigurableJointMotion.Free;
            return;
        }
        rb.freezeRotation = false;
        rb.position = body_rb.position;
        body_transform.GetComponent<ConfigurableJoint>().angularYMotion = ConfigurableJointMotion.Locked;
    }

    private void GetInput()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if(drive_scheme == DriveScheme.FreeTurret)
            {
                print("locking turret");
                SetDriveScheme(DriveScheme.lockedTurret);
            }
            else
            {
                SetDriveScheme(DriveScheme.FreeTurret);
            }
        }
    }

    void FixedUpdate ()
    {
        MoveTank();
    }

    private void MoveTank()
    {
        //assuming x and y go from -1 to 1
        //Left tread accel = (y*speed) + (0.5 * x_axis*speed)
        //Right tread accel = (y*speed) - (0.5 * x_axis*speed)
        //this angle is the angle between the tank position and the turret position
        //when let go of, the pad recalibrates, but the coordinate system is held while the pad is held regardless of head position

        if (moving)
        {
            pad_position = TouchPadPosition.GetAxis(SteamVR_Input_Sources.Any).normalized;

            if(drive_scheme == DriveScheme.FreeTurret)
            {
                FreeTurretMotion();
            }
            else
            {
                LockedTurretMotion();   
            }

            //body_rb.AddForceAtPosition(left_tread_speed, left_tread.position, ForceMode.Acceleration);
            //body_rb.AddForceAtPosition(right_tread_speed, right_tread.position, ForceMode.Acceleration);
        }   
    }

    private void FreeTurretMotion()
    {
        float angle = Vector2.SignedAngle(pad_position, Vector2.up);
        target_point = Quaternion.AngleAxis(angle, Vector3.up) * rel_cam_angle;
        target_point = target_point.normalized;
        float dot = Vector3.Dot(target_point, body_transform.forward);
        Vector3 prd = Vector3.Cross(target_point, body_transform.forward);
        float cross = prd.magnitude;
        if (Vector3.Dot(prd, body_transform.up) < 0)
        {
            cross = -cross;
        }
        drag_direction = body_rb.velocity;
        drag_force = Mathf.Min((max_velocity - drag_direction.magnitude) / max_velocity * accel_scale, 0) * drag_direction.normalized;
        if (CheckGround(lf_raypoint) && CheckGround(rf_raypoint) || CheckGround(lb_raypoint) && CheckGround(rb_raypoint))
        {
            left_tread_speed = ((Mathf.Min(dot * accel_scale, max_accel) - Mathf.Min(torque_scale * cross, max_torque))) * left_tread.forward;
            right_tread_speed = ((Mathf.Min(dot * accel_scale, max_accel) + Mathf.Min(torque_scale * cross, max_torque))) * right_tread.forward;
        }
        else
        {
            left_tread_speed = Vector3.zero;
            right_tread_speed = Vector3.zero;
        }
        print(hit.distance);

        body_rb.AddForceAtPosition(body_rb.mass * (left_tread_speed + drag_force), left_tread.position, ForceMode.Acceleration);
        body_rb.AddForceAtPosition(body_rb.mass * (right_tread_speed + drag_force), right_tread.position, ForceMode.Acceleration);
    }

    private bool CheckGround(Transform raypoint)
    {
        return Physics.Raycast(raypoint.position + raypoint.up, -body_transform.up, out hit, 1.5f, mask);
    }

    //Unused at the moment
    private void LockedTurretMotion()
    {
        drag_direction = new Vector3(body_rb.velocity.x, 0, body_rb.velocity.z);
        drag_force = Mathf.Min((max_velocity - drag_direction.magnitude) / max_velocity * accel_scale, 0) * drag_direction.normalized;
        left_tread_speed = (Mathf.Min(pad_position.y * accel_scale, max_accel) + Mathf.Min((torque_scale) * pad_position.x, max_torque)) * left_tread.forward;
        right_tread_speed = (Mathf.Min(pad_position.y * accel_scale, max_accel) - Mathf.Min((torque_scale) * pad_position.x, max_torque)) * right_tread.forward;
        body_rb.AddForceAtPosition(body_rb.mass * (left_tread_speed + drag_force), left_tread.position, ForceMode.Acceleration);
        body_rb.AddForceAtPosition(body_rb.mass * (right_tread_speed + drag_force), right_tread.position, ForceMode.Acceleration);
    }

    private void ClickDown(SteamVR_Action_Boolean action_In, SteamVR_Input_Sources source)
    {
        rel_cam_angle = cam.forward;
        rel_cam_angle.y = 0;
        rel_cam_angle = rel_cam_angle.normalized;
        moving = true;
        audio_manager.InterPitch(key, 1f, 1.3f, 0.3f);
    }

    private void ClickUp(SteamVR_Action_Boolean action_In, SteamVR_Input_Sources source)
    {
        moving = false;
        audio_manager.InterPitch(key, 1.3f, 1f, 0.3f);
    }

    public Vector3 GetRelocatePosition()
    {
        return cameraPosition.position;
    }

    public float GetRelocateRotation()
    {
        return transform.rotation.eulerAngles.y;
    }

    private void OnDisable()
    {
        click.RemoveOnStateDownListener(ClickDown, SteamVR_Input_Sources.Any);
        click.RemoveOnStateUpListener(ClickUp, SteamVR_Input_Sources.Any);
    }
}
