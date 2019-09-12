using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TLockOn : IState
{
    private readonly StateMachine stateMachine;
    private readonly Transform turret;
    private readonly AI ai;
    float startTime;

    public TLockOn(StateMachine sm)
    {
        stateMachine = sm;
        turret = sm.Owner.transform.Find("turretHead");
        ai = sm.Owner.GetComponent<AI>();
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
            stateMachine.ChangeState(stateMachine.attack);
        }
        else if (distance > ai.Range)
        {
            stateMachine.ChangeState(stateMachine.searching);
        }
    }
}
