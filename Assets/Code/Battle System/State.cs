using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class State : BattleBase
{
    protected TurnManager TurnManager;

    public State(TurnManager turnManager)
    {
        TurnManager = turnManager;
    }

    public virtual IEnumerator Execute()
    {
        yield break;
    }
}
