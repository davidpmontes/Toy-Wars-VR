using System.Collections.Generic;
using UnityEngine;

public class RedHelicopterAI : MonoBehaviour, IDamageable
{
    public List<Transform> Waypoints { get; set; }
    private Rigidbody rb;
    public int CurrentWP { get; set; }

    private readonly float ROTATION_SPEED = 1f;
    private readonly float MOVEMENT_SPEED = 0.03f;
    private readonly float ELEVATION_SPEED = 0.01f;
    private readonly float MIN_WP_DISTANCE = 1f;

    private int healthPoints = 2;

    void Awake()
    {
        Waypoints = new List<Transform>();
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        Patrol();
    }

    public void Patrol()
    {
        if (Vector3.Distance(Waypoints[CurrentWP].transform.position, transform.position) < MIN_WP_DISTANCE)
        {
            CurrentWP++;
            CurrentWP = CurrentWP % Waypoints.Count;
        }

        var direction = Waypoints[CurrentWP].transform.position - transform.position;
        var directionHeading = new Vector3(direction.x, 0, direction.z);
        var directionElevation = direction.y;

        Vector3 movement = transform.forward * MOVEMENT_SPEED + transform.up * directionElevation * ELEVATION_SPEED;

        rb.MovePosition(rb.position + movement);

        var heading = -Vector3.Cross(directionHeading, transform.forward).y;

        Quaternion turnRotation = Quaternion.Euler(0, heading * ROTATION_SPEED, 0);
        rb.MoveRotation(rb.rotation * turnRotation);
    }

    public void Aim()
    {

    }

    public void TakeDamage(int value)
    {
        healthPoints -= value ;
        if (healthPoints <= 0)
        {
            DestroyHelicopter();
        }
    }

    public void DestroyHelicopter()
    {
        ObjectPool.Instance.GetFromPoolActiveSetTransform(Pools.smallExplosion, transform);
        EnemyManager.Instance.DestroyEnemy(gameObject);
    }
}
