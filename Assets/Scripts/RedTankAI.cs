using System.Collections.Generic;
using UnityEngine;

public class RedTankAI : MonoBehaviour, IDamageable
{
    [SerializeField] private List<Transform> Waypoints;
    private Rigidbody rb;
    public int CurrentWP { get; set; }

    private readonly float ROTATION_SPEED = 3f;
    private readonly float MOVEMENT_SPEED = 0.01f;
    private readonly float MIN_WP_DISTANCE = 50f;

    private int healthPoints = 5;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        Patrol();
    }

    public void AddToWaypoints(Transform wp)
    {
        Waypoints.Add(wp);
    }

    public void Patrol()
    {
        if (Waypoints.Count == 0)
            return;

        float distance = Vector3.Distance(Waypoints[CurrentWP].transform.position, transform.position);

        if (distance < MIN_WP_DISTANCE)
        {
            CurrentWP++;
            CurrentWP %= Waypoints.Count;
        }

        var direction = Waypoints[CurrentWP].transform.position - transform.position;
        Vector3 movement = transform.forward * MOVEMENT_SPEED;

        rb.MovePosition(rb.position + movement);

        var heading = -Vector3.Cross(direction, transform.forward).y;

        Quaternion turnRotation = Quaternion.Euler(0, heading * ROTATION_SPEED, 0);
        rb.MoveRotation(rb.rotation * turnRotation);
    }

    public void Aim()
    {

    }

    public void TakeDamage(int value)
    {
        healthPoints -= value;
        if (healthPoints <= 0)
        {
            DestroyTank();
        }
    }

    public void DestroyTank()
    {
        Waypoints.Clear();
        EnemyManager.Instance.DestroyEnemy(gameObject);
        ObjectPool.Instance.GetFromPoolActiveSetTransform(Pools.smallExplosion, transform);
    }
}
