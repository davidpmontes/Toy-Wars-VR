using UnityEngine;

public class Helicopter2Fly : MonoBehaviour
{
    CharacterController cc;
    GameObject HelicopterTilt;

    private int forwardInput;
    private int horizontalInput;
    private int verticalInput;

    private float noseElevation;
    private float forwardSpeed;
    private float turnSpeed;
    private float elevationSpeed;

    private readonly float FORWARD_MAX_SPEED = 2f;
    private readonly float VERTICAL_MAX_SPEED = 2;
    private readonly float TURN_MAX_SPEED = 1.2f;
    private readonly float NOSE_MAX_ELEVATION = 0.15f;

    private readonly float FORWARD_ACCELERATION = 0.5f;
    private readonly float VERTICAL_ACCELERATION = 2f;
    private readonly float TURN_ACCELERATION = 1f;
    private readonly float NOSE_ACCELERATION = 2f;

    void Start()
    {
        cc = GetComponent<CharacterController>();
        HelicopterTilt = transform.Find("HelicopterTilt").gameObject;
    }

    void Update()
    {
        GetInput();
        MoveHelicopter();
    }

    public float GetTurnSpeed()
    {
        return turnSpeed;
    }

    private void GetInput()
    {
        forwardInput = InputController.Vertical();
        horizontalInput = InputController.Horizontal();
        verticalInput = 0;
        if (InputController.Button2())
            verticalInput = 1;
        if (InputController.Button5())
            verticalInput = -1;
    }

    private void MoveHelicopter()
    {
        TiltNose();
        MoveForwardAndVertical();
        Turn();
    }

    private void TiltNose()
    {
        if (forwardInput != 0)
            noseElevation = Mathf.MoveTowards(noseElevation, forwardInput, Time.deltaTime * NOSE_ACCELERATION);

        HelicopterTilt.transform.localPosition = new Vector3(0, -noseElevation * NOSE_MAX_ELEVATION, 0);
    }

    private void MoveForwardAndVertical()
    {
        forwardSpeed = Mathf.MoveTowards(forwardSpeed, noseElevation, Time.deltaTime * FORWARD_ACCELERATION);

        Vector3 forwardMovement = transform.forward * forwardSpeed * FORWARD_MAX_SPEED * Time.deltaTime;

        elevationSpeed = Mathf.MoveTowards(elevationSpeed, verticalInput, Time.deltaTime * VERTICAL_ACCELERATION);

        Vector3 verticalMovement = new Vector3(0, elevationSpeed, 0) * VERTICAL_MAX_SPEED * Time.deltaTime;

        cc.Move(forwardMovement + verticalMovement);
    }

    private void Turn()
    {
        turnSpeed = Mathf.MoveTowards(turnSpeed, horizontalInput, Time.deltaTime * TURN_ACCELERATION);

        transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles + new Vector3(0, turnSpeed, 0) * TURN_MAX_SPEED);
    }
}
