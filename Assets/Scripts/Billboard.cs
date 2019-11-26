
using UnityEngine;

public class Billboard : MonoBehaviour
{
    public float repeatRate;

    private void Start()
    {
        InvokeRepeating("LookAtCamera", 0, repeatRate);
    }

    private void LookAtCamera()
    {
        Vector3 direction = transform.position - Camera.main.transform.position;
        transform.LookAt(Camera.main.transform.position);
    }
}
