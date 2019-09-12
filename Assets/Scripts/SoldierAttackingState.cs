using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoldierAttackingState : IState {

    private GameObject NPC;
    private readonly SoldierStateMachine stateMachine;
    private readonly SoldierAI ai;

    public SoldierAttackingState(SoldierStateMachine sm)
    {
        stateMachine = sm;
        NPC = sm.Owner;
        ai = sm.Owner.GetComponent<SoldierAI>();
    }

    public void EnterState(GameObject owner)
    {
        ai.attack();
    }

    public void ExitState(GameObject owner)
    {
        ai.haltAttack();
    }

    public void UpdateState(GameObject owner)
    {
        ai.lookAt();
        var distance = Vector3.Distance(owner.transform.position, ai.Player.transform.position);
        if (distance > ai.Range)
        {
            stateMachine.ChangeState(stateMachine.patrolling);
        }
    }
}
