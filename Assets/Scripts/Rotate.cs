using UnityEngine;

public class Rotate : MonoBehaviour
{
    [SerializeField]
    private float rate;
	
	void Update ()
    {
        float forwardBack = Input.GetAxis("Vertical");
        float horizontal = Input.GetAxis("Horizontal");

        if (transform.localPosition.x < 0) //left track
        {
            transform.Rotate(new Vector3(-horizontal * rate * Time.deltaTime, 0, 0), Space.Self);
            transform.Rotate(new Vector3(-forwardBack * rate * Time.deltaTime, 0, 0), Space.Self);
        }
        else //right track
        {
            transform.Rotate(new Vector3(-horizontal * rate * Time.deltaTime, 0, 0), Space.Self);
            transform.Rotate(new Vector3(forwardBack * rate * Time.deltaTime, 0, 0), Space.Self);
        }
    }
}
