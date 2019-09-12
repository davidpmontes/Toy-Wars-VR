using TMPro;
using UnityEngine;

public class Points : MonoBehaviour
{
    public Camera cam;
    private Vector3 origin;
    private float lifespan = 1.5f;

    readonly float ScreenWidthHalf = Screen.width / 2;
    readonly float ScreenHeightHalf = Screen.height / 2;

    private void Awake()
    {
        cam = FindObjectOfType<Camera>();
    }

    private void Update()
    {
        Vector3 viewPos = cam.WorldToViewportPoint(origin);

        var x = -ScreenWidthHalf + (Screen.width * viewPos.x);
        var y = -ScreenHeightHalf + (Screen.height * viewPos.y);

        GetComponent<RectTransform>().anchoredPosition = new Vector2(x, y);
    }

    public void Init(int numPoints, Vector3 origin)
    {
        this.origin = origin;

        GetComponentInChildren<TextMeshProUGUI>().text = "+" + numPoints.ToString();
    }

    private void OnEnable()
    {
        Invoke("Deactivate", lifespan);
    }

    void Deactivate()
    {
        PointsContainer.Instance.AddToPool(gameObject);
    }
}
