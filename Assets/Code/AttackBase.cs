using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Attack", menuName = "Create new attack")]
public class AttackBase : ScriptableObject
{
    //[TextArea]
    [SerializeField] int damage, speed, cost;
    [SerializeField] string description;
    [SerializeField] bool isForceDamage, isMovement, isContact, hasTarget;
    
    [SerializeField] Sprite ui;
    [SerializeField] AttackEffects effects;
    //[SerializeField] HealthCategory healthCategory;

    [SerializeField] Element element;
    
    //[SerializeField] List<EffectState> states;

    public string Name
    {
        get { return name; }
    }
    public bool IsMovement
    {
        get { return isMovement; }
    }

    public bool IsForceDamage
    {
        get { return isForceDamage; }
    }

    public bool IsContact
    {
        get { return isContact; }
    }

    public bool HasTarget
    {
        get { return hasTarget; }
    }

    public string Description
    {
        get { return description; }
    }
    public int Damage
    {
        get { return damage; }
    }

    public int Speed
    {
        get { return speed; }
    }

    public int Cost
    {
        get { return cost; }
    }

    public Element Element
    {
        get { return element; }
    }

    public Sprite UI
    {
        get { return ui; }
    }

    public AttackEffects Effects
    {
        get { return effects; }
    }
}

[System.Serializable]
public class AttackEffects
{
    [SerializeField] AttributeChange[] boosts;
    [SerializeField] AttackEffect[] attackEffect;

    [SerializeField] public float numberEffect;
    [SerializeField] bool targetSelf;


    public AttributeChange[] Boosts
    {
        get { return boosts; }
    }

    public AttackEffect[] AttackEffect
    {
        get { return attackEffect; }
    }
}


[System.Serializable]
public class AttributeBoost
{
    public Attribute stat;
    public int boost;
}


[System.Serializable]
public class AttributeChange
{
    public AttributeBoost AttributeBoost;
    public int Timer = 3;
}


public enum AttackEffect
{
    none, recoil, heal, drain, recharge, armor, barrier, provoke, swap,
    healthBased, twoTurnCharge, entryDamage, safeEntry, trapper, area, attackLock
}