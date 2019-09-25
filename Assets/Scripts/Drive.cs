using UnityEngine;
using Valve.VR;

public class Drive : MonoBehaviour, ICameraRelocate
{
    public SteamVR_Action_Vector2 touchPosition;

    private float moveHorizontal;
    private float moveVertical;

    [SerializeField] private float MAX_MOVE_RATE;
    [SerializeField] private float MAX_TURN_RATE;
    [SerializeField] private float MOVE_ACCELERATION;
    [SerializeField] private float TURN_ACCELERATION;

    [SerializeField] private Transform cameraPosition;
    private float currMoveRate;
    private float currTurnRate;

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
        moveHorizontal = touchPosition.GetAxis(SteamVR_Input_Sources.Any).x;
        moveVertical = touchPosition.GetAxis(SteamVR_Input_Sources.Any).y;
    }

    void FixedUpdate ()
    {
        if (Mathf.Abs(moveVertical) > 0.1f)
        {
            currMoveRate = Mathf.MoveTowards(currMoveRate, MAX_MOVE_RATE, MOVE_ACCELERATION);
        }
        else
        {
            currMoveRate = Mathf.MoveTowards(currMoveRate, 0, MOVE_ACCELERATION);
        }

        if (Mathf.Abs(moveHorizontal) > 0.1f)
        {
            currTurnRate = Mathf.MoveTowards(currTurnRate, MAX_TURN_RATE, TURN_ACCELERATION);
        }
        else
        {
            currTurnRate = Mathf.MoveTowards(currTurnRate, 0, TURN_ACCELERATION);
        }

        Vector3 wantedPosition = transform.position + transform.forward * moveVertical * currMoveRate * Time.deltaTime;
        Quaternion wantedRotation = transform.rotation * Quaternion.Euler(Vector3.up * moveHorizontal * currTurnRate * Time.deltaTime);

        rb.MovePosition(wantedPosition);
        rb.MoveRotation(wantedRotation);
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
