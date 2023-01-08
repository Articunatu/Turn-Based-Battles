using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class State
{
    protected TurnManager TurnManager;

    public State(TurnManager turnManager)
    {
        TurnManager = turnManager;
    }

    public virtual IEnumerator Invoke()
    {
        yield break;
    }
}