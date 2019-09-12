using UnityEngine;

public class FollowTarget : MonoBehaviour
{
    public static FollowTarget Instance;

    private Transform IdealCameraPosition;
    private Transform IdealLookAtPosition;

    private readonly float SMOOTH = .1F;

    private void Awake()
    {
        Instance = this;
    }

    private void LateUpdate()
    {
        MoveCamera();
    }

    public void SetCameraTargets(Transform target)
    {
        IdealCameraPosition = target.FindDeepChild("IdealCameraPosition");
        IdealLookAtPosition = target.FindDeepChild("IdealLookAtPosition");
    }

    private void MoveCamera()
    {
        if (IdealCameraPosition == null)
            return;

        transform.LookAt(IdealLookAtPosition.position);
        transform.position = Vector3.Lerp(transform.position, IdealCameraPosition.position, SMOOTH);
    }
}
