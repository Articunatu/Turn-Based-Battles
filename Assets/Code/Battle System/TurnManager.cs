using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnManager : MonoBehaviour
{
    [SerializeField] Entrance entrance;
    [SerializeField] ActionSelect actionSelect;

    void Start()
    {
        entrance.Start();
    }
}
