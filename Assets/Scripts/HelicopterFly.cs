using UnityEngine;
using Valve.VR;

public class HelicopterFly : MonoBehaviour, IRotateable
{
    public SteamVR_Action_Vector2 touchPosition;

    public static HelicopterFly Instance;

    private float horizontal;
    private float vertical;
    public float altitude;

    public Vector3 idealVelocity;

    private readonly float ROTATION_SPEED = 75f;
    private readonly float DESIRED_SPEED_ACCELERATION = 2f;
    private readonly float ALTITUDE_ACCELERATION = 20f;
    private readonly float DESIRED_SIDEWAYS_TILT_ACCELERATION = 7f;
    private readonly float MAX_SPEED = 50f;
    private readonly float MAX_FORWARD_TILT_ANGLE = 15;
    private readonly float MAX_SIDEWAYS_TILT_ANGLE = 25;

    private Rigidbody rb;

    public float desiredSpeed = 0;
    public float desiredSidewaysTilt = 0;

    [SerializeField] private GameObject helicopterRotateable;
    [SerializeField] private GameObject helicopterTiltable;

    void Awake()
    {
        Instance = this;
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        GetInput();
        GetInputVR();
        ProcessInput();
        UpdateIdealVelocity();
        Rotation();
        Tilting();
    }

    void FixedUpdate()
    {
        MatchIdealVelocity();
    }

    void GetInputVR()
    {
        horizontal = touchPosition.GetAxis(SteamVR_Input_Sources.Any).x;
        vertical = touchPosition.GetAxis(SteamVR_Input_Sources.Any).y;
    }

    void GetInput()
    {
        horizontal = InputController.Horizontal();
        vertical = InputController.Vertical();
    }

    void ProcessInput()
    {
        desiredSidewaysTilt += horizontal * Time.deltaTime * DESIRED_SIDEWAYS_TILT_ACCELERATION;
        if (Mathf.Abs(horizontal) < 0.8f)
            desiredSidewaysTilt = Mathf.MoveTowards(desiredSidewaysTilt, 0, Time.deltaTime * DESIRED_SIDEWAYS_TILT_ACCELERATION);

        if (Mathf.Abs(desiredSidewaysTilt) > 1)
            desiredSidewaysTilt = Mathf.Sign(desiredSidewaysTilt);

        desiredSpeed += vertical * Time.deltaTime * DESIRED_SPEED_ACCELERATION;

        if (Mathf.Abs(vertical) < 0.8f)
            desiredSpeed = Mathf.MoveTowards(desiredSpeed, 0, Time.deltaTime * DESIRED_SPEED_ACCELERATION);

        if (Mathf.Abs(desiredSpeed) > 1)
            desiredSpeed = Mathf.Sign(desiredSpeed);

        altitude = 0;
        if (InputController.Button3())
            altitude += ALTITUDE_ACCELERATION;

        if (InputController.Button2())
            altitude -= ALTITUDE_ACCELERATION;
    }

    void UpdateIdealVelocity()
    {
        var rotateableHeading = helicopterRotateable.transform.forward;
        rotateableHeading.y = 0;
        idealVelocity = rotateableHeading * desiredSpeed * MAX_SPEED;
        idealVelocity.y = altitude;
    }

    void MatchIdealVelocity()
    {
        var dif = idealVelocity - rb.velocity;
        rb.AddRelativeForce(dif, ForceMode.Acceleration);
    }

    void Rotation()
    {
        helicopterRotateable.transform.Rotate(Vector3.up, horizontal * Time.deltaTime * ROTATION_SPEED);
    }

    void Tilting()
    {
        helicopterTiltable.transform.localEulerAngles = new Vector3(desiredSpeed * MAX_FORWARD_TILT_ANGLE, 0, -desiredSidewaysTilt * MAX_SIDEWAYS_TILT_ANGLE);
    }

    public GameObject GetRotateable()
    {
        return helicopterRotateable;
    }
}