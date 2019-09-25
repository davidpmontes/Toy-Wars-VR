using UnityEngine;

public class TabletController : MonoBehaviour
{
    public static TabletController Instance { get; private set; }
    [SerializeField] private Camera tabletCam;

    private void Awake()
    {
        Instance = this;
    }

    public void SetCameraToFollowObject(Transform t)
    {
        tabletCam.GetComponent<FollowTarget>().SetCameraTargets(t);
    }
}
