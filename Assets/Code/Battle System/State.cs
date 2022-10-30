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

    public virtual IEnumerator Lead()
    {
        yield break;
    }

    public virtual IEnumerator Entrance()
    {
        yield break;
    }

    public virtual IEnumerator ChooseAction()
    {
        yield break;
    }

    public virtual IEnumerator SpeedComparison()
    {
        yield break;
    }

    public virtual IEnumerator PreUseAttack()
    {
        yield break;
    }

    public virtual IEnumerator PreHitAttack()
    {
        yield break;
    }

    public virtual IEnumerator AttackHit()
    {

        yield break;
    }

    public virtual IEnumerator AfterHitAttack()
    {
        yield break;
    }

    public virtual IEnumerator CheckDefeat()
    {
        yield break;
    }

    public virtual IEnumerator TurnEnd()
    {
        yield break;
    }

    public virtual IEnumerator GameOver()
    {
        yield break;
    }
}