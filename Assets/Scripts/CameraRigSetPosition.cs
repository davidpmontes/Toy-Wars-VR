using UnityEngine;

public class CameraRigSetPosition : MonoBehaviour
{
    public static CameraRigSetPosition Instance { get; private set; }
    [SerializeField] private Transform HMD = default;
    [SerializeField] private GameObject pivot = default;

    void Awake()
    {
        Instance = this;
    }

    public void Relocate(Vector3 globalPosition, float yRotation)
    {
        pivot.transform.parent = null;
        pivot.transform.position = HMD.transform.position;
        transform.parent = pivot.transform;
        pivot.transform.position = globalPosition;
        pivot.transform.Rotate(Vector3.up, yRotation - HMD.transform.rotation.eulerAngles.y);
        pivot.transform.parent = transform;
    }

    public void AttachToGameobject(Transform parent)
    {
        transform.parent = parent;
    }
}
