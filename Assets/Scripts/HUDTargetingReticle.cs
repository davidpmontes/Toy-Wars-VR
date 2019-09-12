using UnityEngine;

public class HUDTargetingReticle : MonoBehaviour
{
    public RectTransform rtReticle;
    public Transform TrueAimingLocation;
    public Camera cam;

    readonly float SCREEN_WIDTH_HALF = Screen.width / 2;
    readonly float SCREEN_HEIGHT_HALF = Screen.height / 2;

    void Update()
    {
        MoveReticle();
    }

    void MoveReticle()
    {
        Vector3 viewPos = cam.WorldToViewportPoint(TrueAimingLocation.transform.position);

        rtReticle.localPosition = new Vector2(-SCREEN_WIDTH_HALF + (Screen.width * viewPos.x),
                                              -SCREEN_HEIGHT_HALF + (Screen.height * viewPos.y));
    }
}
