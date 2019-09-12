using UnityEngine;

public class VRAimer : MonoBehaviour
{
    public static VRAimer Instance { get; private set; }
    [SerializeField] private GameObject targetPoint;

    void Update()
    {
        Instance = this;
        CastRay();
    }

    private void CastRay()
    {
        if (Physics.Raycast(transform.position, transform.forward, out RaycastHit hitInfo, 1000, LayerMask.GetMask("Statics")))
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
