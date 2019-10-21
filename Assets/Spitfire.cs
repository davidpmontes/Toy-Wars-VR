using UnityEngine;

public class Spitfire : MonoBehaviour
{
    [SerializeField] private GameObject[] waypoints;
    private int wpIndex = 0;
    public float moveSpeed;
    public float rotateSpeed;
    [SerializeField] private GameObject tiltable;
    private Vector3 waypointRandom;

    void Update()
    {
        FlyToWaypoint();
    }

    private void FlyToWaypoint()
    {
        var distance = Vector3.Distance(transform.position, waypoints[wpIndex].transform.position + waypointRandom);

        if (distance > 50)
        {
            Vector3 targetDir = waypoints[wpIndex].transform.position + waypointRandom - transform.position;
            Vector3 newDir = Vector3.RotateTowards(transform.forward, targetDir, Time.deltaTime * rotateSpeed, 0.0f);
            transform.localRotation = Quaternion.LookRotation(newDir);

            transform.Translate(0, 0, Time.deltaTime * moveSpeed);
        }
        else
        {
            NextWayPoint();
}
    }

    private void NextWayPoint()
    {
        wpIndex = (++wpIndex) % waypoints.Length;
        waypointRandom = Random.insideUnitSphere * 30;
    }
}
