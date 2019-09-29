using UnityEngine;
using Valve.VR;

public class Drive : MonoBehaviour, ICameraRelocate
{
    public SteamVR_Action_Boolean TankGasPedal;
    public SteamVR_Action_Vector2 TouchPadPosition;

    private int tankGasPedal;
    private int forwardReverse;

    //private float moveHorizontal;
    //private float moveVertical;
    public Vector3 desiredVelocity;
    public Vector3 forceDirection;

    [SerializeField] private float MAX_MOVE_RATE;
    [SerializeField] private float MAX_TURN_RATE;
    [SerializeField] private float MOVE_ACCELERATION;
    [SerializeField] private float TURN_ACCELERATION;

    [SerializeField] private Transform cameraPosition;
    public float currTurnRate;

    private SteamVR_Behaviour_Pose pose = null;

    private Rigidbody rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        pose = GetComponentInParent<SteamVR_Behaviour_Pose>();
    }

    private void Update()
    {
        GetInput();
    }

    private void GetInput()
    {
        tankGasPedal = TankGasPedal.state ? 1 : 0;
        forwardReverse = TouchPadPosition.GetAxis(SteamVR_Input_Sources.Any).y >= 0 ? 1 : -1;
        //moveHorizontal = touchPosition.GetAxis(SteamVR_Input_Sources.Any).x;
        //moveVertical = touchPosition.GetAxis(SteamVR_Input_Sources.Any).y;
    }

    void FixedUpdate ()
    {

        //currMoveRate = Mathf.MoveTowards(currMoveRate, MAX_MOVE_RATE * moveVertical, MOVE_ACCELERATION);

        //if (Mathf.Abs(moveVertical) > 0.1f)
        //{
        //    currMoveRate = Mathf.MoveTowards(currMoveRate, MAX_MOVE_RATE, MOVE_ACCELERATION);
        //}
        //else
        //{
        //    currMoveRate = Mathf.MoveTowards(currMoveRate, 0, MOVE_ACCELERATION);
        //}

        //if (Mathf.Abs(moveHorizontal) > 0.1f)
        //{
        //    currTurnRate = Mathf.MoveTowards(currTurnRate, MAX_TURN_RATE, TURN_ACCELERATION);
        //}
        //else
        //{
        //    currTurnRate = Mathf.MoveTowards(currTurnRate, 0, TURN_ACCELERATION);
        //}

        //Vector3 wantedPosition = transform.position + ((transform.forward) + (transform.right)) * currMoveRate * Time.deltaTime;
        //Quaternion wantedRotation = transform.rotation * Quaternion.Euler(Vector3.up * moveHorizontal * currTurnRate * Time.deltaTime);

        desiredVelocity = forwardReverse * Camera.main.transform.forward * tankGasPedal * 25;  //new Vector3(moveHorizontal, 0, moveVertical) * 5;
        forceDirection =  (desiredVelocity - rb.velocity);
        rb.AddForce(forceDirection, ForceMode.Acceleration);

        //rb.MovePosition(wantedPosition);
        //rb.MoveRotation(wantedRotation);
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
