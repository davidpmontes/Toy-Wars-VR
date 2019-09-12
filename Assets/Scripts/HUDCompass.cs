using UnityEngine;

public class HUDCompass : MonoBehaviour
{
    public GameObject compass;

    private RectTransform rt;

    private readonly float MULTIPLIER = 1.175f;
    private readonly float OFFSET = 200;

    void Awake()
    {
        rt = compass.GetComponent<RectTransform>();
    }

    void Update()
    {
        rt.anchoredPosition = new Vector2(-PlayerManager.Instance.CurrentRotateable().transform.rotation.eulerAngles.y * MULTIPLIER + OFFSET, 0);
    }
}
