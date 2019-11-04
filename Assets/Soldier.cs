using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

public class Soldier : MonoBehaviour
{
    private NavMeshAgent navMeshAgent;
    private UnityAction action;
    private int currIndex;
    private List<Vector3> path = new List<Vector3>();

    private void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    void Start()
    {
        currIndex = 0;
        path = PathManager.Instance.GetPath(PATHS.PATROL1);
        navMeshAgent.SetDestination(path[currIndex]);
        action = PatrolAction;
    }

    void Update()
    {
        action();
    }

    private void PatrolAction()
    {
        var distance = Vector3.Distance(transform.position, navMeshAgent.destination);

        if (distance < 1)
        {
            currIndex = (currIndex + 1) % path.Count;
            navMeshAgent.SetDestination(path[currIndex] + new Vector3(Random.Range(-5, 5), 0, Random.Range(-5, 5)));
        }
    }
}
