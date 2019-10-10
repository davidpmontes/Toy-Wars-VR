using UnityEngine;
using Valve.VR;

public class TankDrive : MonoBehaviour, ICameraRelocate
{
    public SteamVR_Action_Vector2 TouchPadPosition;

    private float strafeLeftRight;
    private float forwardReverse;

    public Vector3 desiredVelocity;
    public Vector3 forceDirection;

    [SerializeField] private float MAX_MOVE_RATE;
    [SerializeField] private float MAX_TURN_RATE;
    [SerializeField] private float MOVE_ACCELERATION;
    [SerializeField] private float TURN_ACCELERATION;
    [SerializeField] private Transform GroundDetectorOrigin;
    [SerializeField] private Transform TurningPoint;
    [SerializeField] private Transform CameraTrackingTracks;

    [SerializeField] private Transform turret;
    [SerializeField] private Transform tracks;
    [SerializeField] private Transform cameraPosition;
    public float currTurnRate;

    private SteamVR_Behaviour_Pose pose = null;

    private Rigidbody rb;
    private Vector3 gravityDirection;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        pose = GetComponentInParent<SteamVR_Behaviour_Pose>();
    }

    private void Update()
    {
        DetectGround();
        GetInput();
        SetTurningPoint();
        RotateTracks();
    }

    private void DetectGround()
    {
        if (Physics.Raycast(GroundDetectorOrigin.position, -transform.up, out RaycastHit hit, 10f, LayerMask.GetMask("Statics")))
        {
            gravityDirection = hit.normal;
        }
    }

    private void GetInput()
    {
        strafeLeftRight = TouchPadPosition.GetAxis(SteamVR_Input_Sources.Any).x;
        forwardReverse = TouchPadPosition.GetAxis(SteamVR_Input_Sources.Any).y;
    }

    void FixedUpdate ()
    {
        MoveTank();
        GravityToSurface();
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
        desiredVelocity = (TouchPadPosition.GetAxis(SteamVR_Input_Sources.Any).magnitude * -tracks.forward) * 25;  //new Vector3(moveHorizontal, 0, moveVertical) * 5;
        forceDirection = (desiredVelocity - rb.velocity);
        rb.AddForce(forceDirection, ForceMode.Acceleration);
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
