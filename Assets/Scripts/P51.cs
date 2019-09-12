using UnityEngine;

public class P51 : MonoBehaviour, IRotateable
{
    public static P51 Instance;
    private Rigidbody rb;

    private readonly float ROTATION_SPEED = 75f;
    private readonly float DESIRED_SPEED_ACCELERATION = 2f;
    private readonly float MIN_SPEED = 0.2f;
    private readonly float MAX_SPEED = 3f;

    private readonly float ROLL_SPEED = 100;

    private float horizontal;
    private float vertical;

    private float desiredSpeed = 0.25f;


    private Vector3 idealVelocity;
    private float idealRoll;
    private float idealPitch;

    [SerializeField] private GameObject yawable;
    [SerializeField] private GameObject pitchable;
    [SerializeField] private GameObject rollable;

    [SerializeField] private GameObject leftRoll;
    [SerializeField] private GameObject rightRoll;
    [SerializeField] private GameObject zeroRoll;

    void Start()
    {
        Instance = this;
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        GetInput();
        UpdateIdealVelocity();
        Yawing();
        Pitching();
        Rolling();
    }

    void FixedUpdate()
    {
        MatchIdealVelocity();
    }

    void GetInput()
    {
        horizontal = InputController.Horizontal();
        vertical = InputController.Vertical();

        if (InputController.Button3())
            desiredSpeed += Time.deltaTime * DESIRED_SPEED_ACCELERATION;

        if (InputController.Button2())
            desiredSpeed -= Time.deltaTime * DESIRED_SPEED_ACCELERATION;

        desiredSpeed = Mathf.Clamp(desiredSpeed, MIN_SPEED, MAX_SPEED);
    }

    void UpdateIdealVelocity()
    {
        var heading = yawable.transform.forward + pitchable.transform.forward;
        idealVelocity = heading * desiredSpeed;
    }

    void MatchIdealVelocity()
    {
        var dif = idealVelocity - rb.velocity;
        rb.AddRelativeForce(dif, ForceMode.Acceleration);
    }

    void Yawing()
    {
        yawable.transform.Rotate(Vector3.up, horizontal * Time.deltaTime * ROTATION_SPEED);
    }

    void Pitching()
    {
        pitchable.transform.Rotate(Vector3.right, vertical * Time.deltaTime * ROTATION_SPEED);
    }

    void Rolling()
    {
        //Vector3 rotation = rollable.transform.localEulerAngles;

        //if (horizontal < 0)
        //{
        //    rotation.z = Mathf.MoveTowards(rotation.z, 20, 10);
        //}
        //if (horizontal > 0)
        //{
        //    rotation.z = Mathf.MoveTowards(rotation.z, 20, 10);
        //}
        //rotation.z = -horizontal * MAX_WING_ROLL_DEGREES;
        //rollable.transform.localEulerAngles = rotation;

        if (horizontal < -0.1f)
        {
            rollable.transform.rotation = Quaternion.RotateTowards(rollable.transform.rotation, leftRoll.transform.rotation, Time.deltaTime * ROLL_SPEED);
        }
        else if (horizontal > 0.1f)
        {
            rollable.transform.rotation = Quaternion.RotateTowards(rollable.transform.rotation, rightRoll.transform.rotation, Time.deltaTime * ROLL_SPEED);
        }
        else
        {
            rollable.transform.rotation = Quaternion.RotateTowards(rollable.transform.rotation, zeroRoll.transform.rotation, Time.deltaTime * ROLL_SPEED);
        }
    }

    public GameObject GetRotateable()
    {
        return yawable;
    }
}
