using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoldierPatrolState : IState {

    private GameObject NPC;
    private readonly SoldierStateMachine stateMachine;
    private readonly SoldierAI ai;
    int currentWP;

    public SoldierPatrolState(SoldierStateMachine sm)
    {
        stateMachine = sm;
        NPC = sm.Owner;
        ai = sm.Owner.GetComponent<SoldierAI>();
    }

    public void EnterState(GameObject owner)
    {
        currentWP = 0;
    }

    public void ExitState(GameObject owner)
    {
    }

    public void UpdateState(GameObject owner)
    {
        if (ai.waypoints.Length == 0) Debug.Break();
        if (Vector3.Distance(ai.waypoints[currentWP].transform.position,
                             NPC.transform.position) < 0.01f)
        {
            currentWP++;
            currentWP = currentWP % ai.waypoints.Length;
        }

        var direction = ai.waypoints[currentWP].transform.position - NPC.transform.position;
        NPC.transform.rotation = Quaternion.Slerp(NPC.transform.rotation,
                                                  Quaternion.LookRotation(direction),
                                                  ai.TurnRate * Time.deltaTime);
        NPC.transform.Translate(0, 0, Time.deltaTime * ai.Speed);

        var distance = Vector3.Distance(owner.transform.position, ai.Player.transform.position);
        if (distance < ai.Range)
        {
            stateMachine.ChangeState(stateMachine.lockingOn);
        }
    }
}
