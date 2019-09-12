using UnityEngine;

public class AimingCircle : MonoBehaviour
{
    public static AimingCircle Instance;
    [SerializeField] private Transform trueAimingTransform;

    private RectTransform rt;

    void Awake()
    {
        Instance = this;
        rt = GetComponent<RectTransform>();
    }

    void Update()
    {
        var viewportPoint = Camera.main.WorldToViewportPoint(trueAimingTransform.position);
        viewportPoint.x = (viewportPoint.x * Screen.width) - Screen.width / 2;
        viewportPoint.y = (viewportPoint.y * Screen.height) - Screen.height / 2;
        rt.anchoredPosition = viewportPoint;
    }

    public void SetTrueAimingTransform(Transform t)
    {
        trueAimingTransform = t.FindDeepChild("TrueAimingPosition");
    }

    public Vector3 rtPosition()
    {
        return rt.anchoredPosition;
    }
}
