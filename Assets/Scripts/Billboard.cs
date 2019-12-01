
using UnityEngine;

public class Billboard : MonoBehaviour
{
    private void Update()
    {
        LookAtCamera();
    }

    private void LookAtCamera()
    {
        Vector3 direction = transform.position - Camera.main.transform.position;
        transform.LookAt(Camera.main.transform.position);
    }
}
