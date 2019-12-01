using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

public enum SOLDIER_TYPE
{
    BASE_PERIMETER,
    TOWER_GUARD
}

public class Soldier : MonoBehaviour
{
    private string[] phrases = { "SoldierOw", "SoldierStopThat", "SoldierWatchYourFire"};

    private NavMeshAgent navMeshAgent;
    private UnityAction action;
    private int currIndex;
    private List<Vector3> path = new List<Vector3>();
    public SOLDIER_TYPE type;
    private int state = 0;
    private Animator animator;
    private float fratricideTimer;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    void Start()
    {
        if (type == SOLDIER_TYPE.BASE_PERIMETER)
        {
            navMeshAgent = GetComponent<NavMeshAgent>();
            currIndex = 0;
            path = PathManager.Instance.GetPath(PATHS.PATROL1);
            navMeshAgent.SetDestination(path[currIndex]);
            action = BasePatrolAction;
        }
        else if (type == SOLDIER_TYPE.TOWER_GUARD)
        {
            action = TowerGuardAction;
        }
    }

    void Update()
    {
        action();
    }

    public void Fratricide()
    {
        // AudioManager.Instance.PlayNarration(phrases[Random.Range(0, 3)]);
        if (fratricideTimer < Time.time)
            fratricideTimer = Time.time + 1;
        else
            return;

        AudioManager.Instance.PlayOneshot(phrases[Random.Range(0, phrases.Length)], transform.position);
    }

    private void TowerGuardAction()
    {
        if (state == 0) //initiate watch
        {
            Invoke("Watch", Random.Range(8, 15));
            state = 1;
        }
    }

    private void BasePatrolAction()
    {
        if (state == 0)
        {
            Invoke("Walk", Random.Range(0, 2));
            state = 1;
        }
        else if (state == 2)
        {
            var distance = Vector3.Distance(transform.position, navMeshAgent.destination);

            if (distance < 1)
            {
                currIndex = (currIndex + 1) % path.Count;
                navMeshAgent.SetDestination(path[currIndex] + new Vector3(Random.Range(-5, 5), 0, Random.Range(-5, 5)));
            }
        }
    }

    private void Attack()
    {
        
    }

    private void Watch()
    {
        animator.SetTrigger("watch");
        state = 0;
    }

    private void Walk()
    {
        animator.SetTrigger("walk");
        state = 2;
    }
}
