using UnityEngine;

public class HelicopterFly : MonoBehaviour, IRotateable
{
    public static HelicopterFly Instance;

    private float horizontal;
    private float vertical;
    public float altitude;

    public Vector3 idealVelocity;

    private readonly float ROTATION_SPEED = 75f;
    private readonly float DESIRED_SPEED_ACCELERATION = 2f;
    private readonly float DESIRED_SIDEWAYS_TILT_ACCELERATION = 7f;
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
        UpdateIdealVelocity();
        Rotation();
        Tilting();
    }

    void FixedUpdate()
    {
        MatchIdealVelocity();
    }

    void GetInput()
    {
        horizontal = InputController.Horizontal();

        desiredSidewaysTilt += horizontal * Time.deltaTime * DESIRED_SIDEWAYS_TILT_ACCELERATION;
        if (Mathf.Abs(horizontal) < 0.8f)
            desiredSidewaysTilt = Mathf.MoveTowards(desiredSidewaysTilt, 0, Time.deltaTime * DESIRED_SIDEWAYS_TILT_ACCELERATION);

        if (Mathf.Abs(desiredSidewaysTilt) > 1)
            desiredSidewaysTilt = Mathf.Sign(desiredSidewaysTilt);

        vertical = InputController.Vertical();

        desiredSpeed += vertical * Time.deltaTime * DESIRED_SPEED_ACCELERATION;
        if (Mathf.Abs(desiredSpeed) > 1)
            desiredSpeed = Mathf.Sign(desiredSpeed);

        altitude = 0;
        if (InputController.Button3())
            altitude++;

        if (InputController.Button2())
            altitude--;
    }

    void UpdateIdealVelocity()
    {
        var rotateableHeading = helicopterRotateable.transform.forward;
        rotateableHeading.y = 0;
        idealVelocity = rotateableHeading * desiredSpeed;
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