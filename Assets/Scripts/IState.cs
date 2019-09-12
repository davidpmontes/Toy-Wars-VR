using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IState
{
    void EnterState(GameObject Owner);
    void ExitState(GameObject Owner);
    void UpdateState(GameObject Owner);
}
