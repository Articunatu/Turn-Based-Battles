using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Character
{
    [SerializeField] CharacterBase characterBase;

    public CharacterBase Base
    {
        get
        {
            return characterBase;
        }
    }

    int recoil;

    public int Health { get; set; }
    public int Energy { get; set; }
    public Equipment Equipment;

    public List<Attack> Attacks { get; set; }

    public Attack CurrentAttack { get; set; }
    public Attack LockedAttack { get; set; }
    public bool IsLocked { get; set; }
    public bool IsRecharging { get; set; }
    public Dictionary<Attribute, int> Attributes { get; private set; }

    public Dictionary<Attribute, int> AttributeBoosts { get; private set; }
    public List<AttributeChange> AttributeChanges = null;


    public bool HealthChanged, IsItemActive;
    public void SetAttacks()
    {

        Attacks = new List<Attack>();

        foreach (var attack in Base.LearnableAttacks)
        {
            Attacks.Add(new Attack(attack.Base));
            //if (Attacks.Count >= 4)
            //    break;
        }

        CalculateAttributes();

        Health = MaxHealth;
        Energy = MaxEnergy;

        ResetStatBoosts();
    }

    void CalculateAttributes()
    {
        Attributes = new Dictionary<Attribute, int>();
        Attributes.Add(Attribute.Strength, Mathf.FloorToInt(Base.Toughness));
        Attributes.Add(Attribute.Toughness, Mathf.FloorToInt(Base.Toughness));
        Attributes.Add(Attribute.Magic, Mathf.FloorToInt(Base.Magic));
        Attributes.Add(Attribute.Composition, Mathf.FloorToInt(Base.Composition));
        Attributes.Add(Attribute.Agility, Mathf.FloorToInt(Base.Agility));

        MaxEnergy = 3;
        MaxHealth = Mathf.FloorToInt((Base.MaxHealth));
    }

    void ResetStatBoosts()
    {
        AttributeBoosts = new Dictionary<Attribute, int>()
        {
            {Attribute.Strength, 0},
            {Attribute.Toughness, 0},
            {Attribute.Magic, 0},
            {Attribute.Composition, 0},
            {Attribute.Agility, 0},
        };
    }

    public int GetAttribute(Attribute attribute)
    {
        int attributeValue = Attributes[attribute];

        int change = AttributeBoosts[attribute];
        var changeValues = new float[] { 1f, 1.5f, 2f, 2.5f, 3f};

        if (change >= 0)
        {
            attributeValue = Mathf.FloorToInt(attributeValue * changeValues[change]);
        }
        else
        {
            attributeValue = Mathf.FloorToInt(attributeValue / changeValues[-change]);
        }

        return attributeValue;
    }

    public void ApplyChanges(List<AttributeChange> _attributeChanges)
    {
        foreach (var attributeChange in _attributeChanges)
        {
            Attribute stat = attributeChange.AttributeBoost.stat;
            int boost = attributeChange.AttributeBoost.boost;

            AttributeBoosts[stat] = Mathf.Clamp(AttributeBoosts[stat] + boost, -4, 4);

            ///Add dialog in StateMachine instead
            //if (boost > 1)
            //{
            //    ConditionChange.Enqueue($"{Base.Name}'s {stat} sharply rose!");
            //}
            //else if (boost > 0)
            //{
            //    ConditionChange.Enqueue($"{Base.Name}'s {stat} rose!");
            //}
            //else if (boost > -2)
            //{
            //    ConditionChange.Enqueue($"{Base.Name}'s {stat} fell!");
            //}
            //else
            //{
            //    ConditionChange.Enqueue($"{Base.Name}'s {stat} harshly fell!");
            //}
            //Debug.Log($"{stat} has been boosted to {StatBoosts[stat]}");
            AttributeBoost newBoost = new AttributeBoost() { boost = boost, stat = stat };
            AttributeChange newChange = new AttributeChange() { AttributeBoost = newBoost, Timer = attributeChange.Timer };
            AttributeChanges.Add(newChange);
        }
    }

    public int Strength
    {
        get { return GetAttribute(Attribute.Strength); }
    }
    public int Toughness
    {
        get { return GetAttribute(Attribute.Toughness); }
    }

    public int Magic
    {
        get { return GetAttribute(Attribute.Magic); }
    }

    public int Composition
    {
        get { return GetAttribute(Attribute.Composition); ; }
    }

    public int Agility
    {
        get { return GetAttribute(Attribute.Agility); }
    }
    public int MaxHealth
    {
        get; private set;
    }
    public int MaxEnergy
    {
        get; private set;
    }


    public void UpdateHealth(int damage)
    {
        Health = Mathf.Clamp(Health - damage, 0, MaxHealth);
        HealthChanged = true;
    }
    public void UpdateRecoil(int recoil_)
    {
        Health = Mathf.Clamp(Health - recoil_, 0, MaxHealth);
        HealthChanged = true;
    }

    public void UpdateHeal(int heal)
    {
        Health = Mathf.Clamp(Health + heal, 0, MaxHealth);
        HealthChanged = true;
    }

    public Attack GetRandomAttack()
    {
        int r = Random.Range(0, Attacks.Count);
        return Attacks[r];
    }
}