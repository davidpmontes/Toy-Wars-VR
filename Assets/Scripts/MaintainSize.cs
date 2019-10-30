using UnityEngine;

public class MaintainSize : MonoBehaviour
{
    public float adjustment = 100;
    void Update()
    {
        var distance = Vector3.Distance(transform.position, Camera.main.transform.position);
        transform.localScale = Vector3.one * distance / adjustment;
    }
}
