using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine
{
    public IState currentState { get; private set; }
    public GameObject Owner;

    public IState searching;
    public IState lockingOn;
    public IState attack;

    public StateMachine(GameObject o)
    {
        Owner = o;

        searching = new TSearch(this);
        lockingOn = new TLockOn(this);
        attack = new TAttack(this);

        currentState = searching;
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

