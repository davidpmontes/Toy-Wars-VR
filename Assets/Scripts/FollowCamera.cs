using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    void Update()
    {
        transform.position = Camera.main.transform.position;
        transform.rotation = Camera.main.transform.rotation;
    }
}
