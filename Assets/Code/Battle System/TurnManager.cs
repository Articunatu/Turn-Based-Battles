using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnManager : MonoBehaviour
{
    [SerializeField] Entrance entrance;
    [SerializeField] ActionSelect actionSelect;
    StateMachine _machine;

    public TurnManager(StateMachine machine)
    {
        _machine = machine;
    }

    void Start()
    {
        BattleController();
    }

    void BattleController()
    {
       _machine.SetState(new Entrance());
    }
}
