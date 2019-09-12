using UnityEngine;

public class FollowPosition : MonoBehaviour
{
    void Update()
    {
        transform.position = Camera.main.transform.position;
    }
}
