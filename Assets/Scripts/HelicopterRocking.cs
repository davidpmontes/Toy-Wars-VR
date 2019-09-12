using UnityEngine;

public class HelicopterRocking : MonoBehaviour
{
    private float h;
    private float f;
    private bool flightToggle;

    //private float v;

    private float forwardTilt;
    private float horizontalTilt;

    public float maxTilt;
    public float returnFromTiltSpeed;
    public float tiltSpeed;
    
    Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void FixedUpdate()
    {
        GetInput();
        Rock();
    }

    void GetInput()
    {
        flightToggle = InputController.Button1();
        h = InputController.Horizontal();
        f = InputController.Vertical();
    }

    void Rock()
    {
        if (Mathf.Abs(f) > 0 && !flightToggle)
        {
            forwardTilt = Mathf.MoveTowards(forwardTilt, Mathf.Sign(f) * maxTilt, Time.deltaTime * tiltSpeed);
        }

        if (Mathf.Abs(h) > 0 && !flightToggle)
        {
            horizontalTilt = Mathf.MoveTowards(horizontalTilt, Mathf.Sign(h) * maxTilt, Time.deltaTime * tiltSpeed);
        }

        forwardTilt = Mathf.MoveTowards(forwardTilt, 0, Time.deltaTime * returnFromTiltSpeed);
        horizontalTilt = Mathf.MoveTowards(horizontalTilt, 0, Time.deltaTime * returnFromTiltSpeed);

        forwardTilt = Mathf.Clamp(forwardTilt, -maxTilt, maxTilt);
        horizontalTilt = Mathf.Clamp(horizontalTilt, -maxTilt, maxTilt);

        animator.SetFloat("forward", forwardTilt);
        animator.SetFloat("horizontal", horizontalTilt);
    }
}
