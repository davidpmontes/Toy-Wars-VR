using UnityEngine;

public class MainMenuTargetMove : MonoBehaviour
{
    private float h;
    private float v;

    private float rate = 0.2f;

    void Start()
    {
        transform.position = new Vector3(0, 0, -1);
    }

    void Update()
    {
        GetInput();
        MoveTarget();
    }

    void GetInput()
    {
        h = InputController.Horizontal();
        v = InputController.Vertical();
    }

    void MoveTarget()
    {
        transform.Translate(new Vector3(h * rate, v * rate, 0));
        Vector3 currentPosition = transform.position;
        currentPosition.x = Mathf.Clamp(currentPosition.x, -10.5f, 10.5f);
        currentPosition.y = Mathf.Clamp(currentPosition.y, -6.5f, 6.5f);
        transform.position = currentPosition;
    }
}
