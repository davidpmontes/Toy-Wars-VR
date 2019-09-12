using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TSearch : IState
{
    private readonly StateMachine stateMachine;
    private readonly Transform turret;
    private readonly AI ai;

    public TSearch(StateMachine sm)
    {
        stateMachine = sm;
        turret = sm.Owner.transform.Find("turretHead");
        ai = sm.Owner.GetComponent<AI>();
    }

    public void EnterState(GameObject owner)
    {
    }

    public void ExitState(GameObject owner)
    {
    }

    public void UpdateState(GameObject owner)
    {
        turret.Rotate(Vector3.up * Time.deltaTime * ai.TurnRate);
        var distance = Vector3.Distance(owner.transform.position, ai.Player.transform.position);

        if (distance < ai.Range)
        {
            stateMachine.ChangeState(stateMachine.lockingOn);
        }
    }
}
