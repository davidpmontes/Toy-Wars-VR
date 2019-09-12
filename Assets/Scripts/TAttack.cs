using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TAttack : IState {

    private readonly StateMachine stateMachine;
    private readonly Transform turret;
    private readonly AI ai;

    public TAttack(StateMachine sm)
    {
        stateMachine = sm;
        turret = sm.Owner.transform.Find("turretHead");
        ai = sm.Owner.GetComponent<AI>();
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
            stateMachine.ChangeState(stateMachine.searching);
        }
    }
}
