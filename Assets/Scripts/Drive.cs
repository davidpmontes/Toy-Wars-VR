using UnityEngine;

public class Drive : MonoBehaviour
{
    [SerializeField]
    private float moveRate;

    [SerializeField]
    private float turnRate;

    private Rigidbody rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate ()
    {
        Vector3 wantedPosition = transform.position + transform.forward * InputController.Vertical() * moveRate * Time.deltaTime;
        Quaternion wantedRotation = transform.rotation * Quaternion.Euler(Vector3.up * InputController.Horizontal() * turnRate * Time.deltaTime); //  * horizontal * turnRate * Time.deltaTime

        rb.MovePosition(wantedPosition);
        rb.MoveRotation(wantedRotation);
    }
}
