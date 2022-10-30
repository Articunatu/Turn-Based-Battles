using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Equipment", menuName = "Create new equipment")]
public class Equipment : ScriptableObject
{
    [SerializeField] EquipmentEffect[] effects;

    public EquipmentEffect[] Effects
    {
        get { return effects; }
    }
}

public enum EquipmentEffect
{
    none, attackLock, healPerTurns, attributeBoost, helmet, vest, orb,
    instantCharge, resetAttributes, equipmentResist
}