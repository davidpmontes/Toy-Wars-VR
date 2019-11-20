using UnityEngine;
using Valve.VR;

public class TankDrive : MonoBehaviour, ICameraRelocate
{
    public SteamVR_Action_Vector2 TouchPadPosition;
    public SteamVR_Action_Boolean touch;

    private float strafeLeftRight;
    private float forwardReverse;

    public Vector3 desiredVelocity;
    public Vector3 forceDirection;

    [SerializeField] private float MAX_MOVE_RATE = default;
    [SerializeField] private float MAX_TURN_RATE = default;
    [SerializeField] private float MOVE_ACCELERATION = default;
    [SerializeField] private float TURN_ACCELERATION = default;
    [SerializeField] private Transform TurningPoint = default;
    [SerializeField] private Transform CameraTrackingTracks = default;

    [SerializeField] private Transform turret = default;
    [SerializeField] private Transform tracks = default;
    [SerializeField] private Transform cameraPosition = default;

    [SerializeField] private Transform left_tread = default;
    [SerializeField] private Transform right_tread = default;

    [SerializeField] private float max_velocity;
    [SerializeField] private float accel_scale;

    private Vector3 left_tread_speed;
    private Vector3 right_tread_speed;
    private Rigidbody left_tread_rb;
    private Rigidbody right_tread_rb;
    private Vector2 pad_position;

    private Transform cam;
    private Vector2 rel_cam_angle;
    private Vector3 target_point;

    public float currTurnRate;

    private SteamVR_Behaviour_Pose pose = null;

    private Rigidbody rb;
    private Vector3 gravityDirection;

    void Awake()
    {
        cam = GameObject.FindGameObjectWithTag("MainCamera").transform;
        rb = GetComponent<Rigidbody>();
        left_tread_rb = left_tread.GetComponent<Rigidbody>();
        right_tread_rb = right_tread.GetComponent<Rigidbody>();
        pose = GetComponentInParent<SteamVR_Behaviour_Pose>();
    }

    private void Update()
    {
        GetInput();
    }

    private void GetInput()
    {
        //strafeLeftRight = TouchPadPosition.GetAxis(SteamVR_Input_Sources.Any).x;
        //forwardReverse = TouchPadPosition.GetAxis(SteamVR_Input_Sources.Any).y;
    }

    void FixedUpdate ()
    {
        MoveTank();
    }

    private void SetTurningPoint()
    {
        TurningPoint.localPosition = new Vector3(TouchPadPosition.GetAxis(SteamVR_Input_Sources.Any).x, 0, TouchPadPosition.GetAxis(SteamVR_Input_Sources.Any).y);
    }

    private void RotateTracks()
    {
        CameraTrackingTracks.rotation = Quaternion.Euler(new Vector3(transform.localRotation.eulerAngles.x, turret.rotation.eulerAngles.y, transform.localRotation.eulerAngles.z));

        var direction = (TurningPoint.position - transform.position).normalized;
        direction.y = 0;

        //create the rotation we need to be in to look at the target
        var lookRotation = Quaternion.LookRotation(direction);

        //rotate us over time according to speed until we are in the required rotation
        tracks.localRotation = Quaternion.Slerp(tracks.localRotation, lookRotation, Time.deltaTime * 2);
    }

    private void MoveTank()
    {
        //assuming x and y go from -1 to 1
        //Left tread accel = (y*speed) + (0.5 * x_axis*speed)
        //Right tread accel = (y*speed) - (0.5 * x_axis*speed)
        //this angle is the angle between the tank position and the turret position
        //when let go of, the pad recalibrates, but the coordinate system is held while the pad is held regardless of head position

        pad_position = TouchPadPosition.GetAxis(SteamVR_Input_Sources.Any).normalized;

        float angle = Vector2.SignedAngle(Vector2.up, pad_position);

        target_point = Quaternion.AngleAxis(angle, Vector3.up) * rel_cam_angle;

        float accel_x = transform.position.x - target_point.x;
        float accel_y = transform.position.y - target_point.y;

        Vector2.SignedAngle(pad_position.normalized, rel_cam_angle);
        //Desired speed of left and right tank treads
        left_tread_speed = (max_velocity - ((accel_y * accel_scale) + (0.5f * accel_x * accel_scale))) * left_tread.forward;
        right_tread_speed = (max_velocity - ((accel_y * accel_scale) - (0.5f * accel_x * accel_scale))) * right_tread.forward;

        left_tread_rb.AddForce(left_tread_speed, ForceMode.Acceleration);
        right_tread_rb.AddForce(right_tread_speed, ForceMode.Acceleration);

        //desiredVelocity = (TouchPadPosition.GetAxis(SteamVR_Input_Sources.Any).magnitude * -tracks.forward) * 25;  //new Vector3(moveHorizontal, 0, moveVertical) * 5;
        //forceDirection = (desiredVelocity - rb.velocity);
        //rb.AddForce(forceDirection, ForceMode.Acceleration);
    }

    private void CalibratePad()
    {
        rel_cam_angle = new Vector2(cam.forward.x, cam.forward.z);
        rel_cam_angle = rel_cam_angle.normalized;
    }

    private void GravityToSurface()
    {
        rb.AddForce(gravityDirection * Physics.gravity.y, ForceMode.Acceleration);
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
