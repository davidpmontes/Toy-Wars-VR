using UnityEngine;

public class AlienAI : MonoBehaviour
{
    Animator animator;
    public GameObject[] waypoints;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        animator.SetFloat("distance", Vector3.Distance(transform.position, PlayerManager.Instance.CurrentVehicle().transform.position));
    }
}
