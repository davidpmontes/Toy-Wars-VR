using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoldierStateMachine {

    public IState currentState { get; private set; }
    public GameObject Owner;

    public IState patrolling;
    public IState lockingOn;
    public IState attacking;

    public SoldierStateMachine(GameObject o)
    {
        Owner = o;

        patrolling = new SoldierPatrolState(this);
        lockingOn = new SoldierLockingOnState(this);
        attacking = new SoldierAttackingState(this);

        currentState = patrolling;
    }

    public void ChangeState(IState newState)
    {
        if (currentState != null)
            currentState.ExitState(Owner);

        currentState = newState;
        currentState.EnterState(Owner);
    }

    public void Update()
    {
        if (currentState != null)
            currentState.UpdateState(Owner);
    }
}
