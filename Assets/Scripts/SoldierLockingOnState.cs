using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoldierLockingOnState : IState
{
    private GameObject NPC;
    private readonly SoldierStateMachine stateMachine;
    private readonly SoldierAI ai;
    float startTime;

    public SoldierLockingOnState(SoldierStateMachine sm)
    {
        stateMachine = sm;
        NPC = sm.Owner;
        ai = sm.Owner.GetComponent<SoldierAI>();
    }

    public void EnterState(GameObject owner)
    {
        startTime = Time.time;
    }

    public void ExitState(GameObject owner)
    {
    }

    public void UpdateState(GameObject owner)
    {
        ai.lookAt();
        var distance = Vector3.Distance(owner.transform.position, ai.Player.transform.position);
        if (Time.time - startTime > 2.0f)
        {
            stateMachine.ChangeState(stateMachine.attacking);
        }
        else if (distance > ai.Range)
        {
            stateMachine.ChangeState(stateMachine.patrolling);
        }
    }
}
