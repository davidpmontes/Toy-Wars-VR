using UnityEngine;

public class CameraAutoFollowAimer : MonoBehaviour
{
    void Update()
    {
        GameObject targetPoint = VRAimer.Instance.GetTargetPoint();

        if (targetPoint.activeSelf)
        {
            var direction = (targetPoint.transform.position - transform.position).normalized;
            transform.rotation = Quaternion.LookRotation(direction);
        }
    }
}
