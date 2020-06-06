using UnityEngine;

public class VRAimer : MonoBehaviour
{
    public static VRAimer Instance { get; private set; }

    [SerializeField] private GameObject targetPoint = default;
    [SerializeField] private LayerMask layerMask;

    private void Awake()
    {
        Instance = this;
    }

    void Update()
    {
        CastRay();
    }

    private void CastRay()
    {
        if (Physics.Raycast(transform.position, transform.forward, out RaycastHit hitInfo, 1000, layerMask))
        {
            targetPoint.gameObject.SetActive(true);
            targetPoint.transform.position = hitInfo.point;
            targetPoint.transform.LookAt(Camera.main.transform);
        }
        else
        {
            targetPoint.gameObject.SetActive(false);
        }
    }

    public GameObject GetTargetPoint()
    {
        return targetPoint;
    }
}
