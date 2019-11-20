using UnityEngine;
using Valve.VR;

public class TankDrive : MonoBehaviour, ICameraRelocate
{
    public SteamVR_Action_Vector2 TouchPadPosition;
    public SteamVR_Action_Boolean click;

    private float strafeLeftRight;
    private float forwardReverse;

    public Vector3 desiredVelocity;
    public Vector3 forceDirection;

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
    private Vector2 pad_position;
    private bool moving = false;

    private Transform cam;
    private Vector3 rel_cam_angle;
    private Vector3 target_point;

    public float currTurnRate;

    private SteamVR_Behaviour_Pose pose = null;

    private Rigidbody rb;
    private Vector3 gravityDirection;

    void Awake()
    {
        cam = GameObject.FindGameObjectWithTag("MainCamera").transform;
        rb = GetComponent<Rigidbody>();
        pose = GetComponentInParent<SteamVR_Behaviour_Pose>();
        click.AddOnStateDownListener(ClickDown, SteamVR_Input_Sources.Any);
        click.AddOnStateUpListener(ClickUp, SteamVR_Input_Sources.Any);
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

            float angle = Vector2.SignedAngle(pad_position, Vector2.up);
            target_point = Quaternion.AngleAxis(angle, Vector3.up) * rel_cam_angle;

            target_point = target_point.normalized;

            float accel_x = target_point.x - transform.forward.x;
            float accel_y = target_point.z - transform.forward.z + 1;

            //Vector2.SignedAngle(pad_position.normalized, new Vector2(rel_cam_angle.x, rel_cam_angle.z));
            //Desired speed of left and right tank treads
            left_tread_speed = (((2 * pad_position.y * accel_scale) + (8* accel_x * accel_scale))) * left_tread.forward;
            right_tread_speed = (((2 * pad_position.y * accel_scale) - (8 * accel_x * accel_scale))) * right_tread.forward;
            print("left: " + left_tread.position);
            print("right: " + right_tread.position);
            
            rb.AddForceAtPosition(left_tread_speed, left_tread.position, ForceMode.Acceleration);
            rb.AddForceAtPosition(right_tread_speed, right_tread.position, ForceMode.Acceleration);
        }   
        //desiredVelocity = (TouchPadPosition.GetAxis(SteamVR_Input_Sources.Any).magnitude * -tracks.forward) * 25;  //new Vector3(moveHorizontal, 0, moveVertical) * 5;
        //forceDirection = (desiredVelocity - rb.velocity);
        //rb.AddForce(forceDirection, ForceMode.Acceleration);
    }

    private void ClickDown(SteamVR_Action_Boolean action_In, SteamVR_Input_Sources source)
    {
        print("clickdown");
        rel_cam_angle = cam.forward;
        rel_cam_angle.y = 0;
        rel_cam_angle = rel_cam_angle.normalized;
        moving = true;
    }

    private void ClickUp(SteamVR_Action_Boolean action_In, SteamVR_Input_Sources source)
    {
        print("clickup");
        moving = false;
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
