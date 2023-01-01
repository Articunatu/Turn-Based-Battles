using System.Collections;
using UnityEngine;

public class DamageCalculator : BattleBase
{
    Hud hud;

    public float[] CalculateDamage(Attack _attack, Character attacker, Player target, float _damage = 0)
    {
        Character defender = target._character;
        AttackBase attack = _attack.Base;
        //Debug.Log(attack.Damage);

        float elementMatchup = ElementalMatchups.GetElementalMatchup(attack.Element, defender.Base.Element1) *
        ElementalMatchups.GetElementalMatchup(attack.Element, defender.Base.Element2);

        float elementBoost = (attacker.Base.Element1 == attack.Element || attacker.Base.Element2 == attack.Element) ? 1.5f : 1;
        //float healthVariable = 1, area = 1, screen = 1, thirdHealth = 1;

        //AdditionalCalcs(_attack, attacker, target, attack);
        float attackValue = (attack.IsMovement) ? attacker.Strength : attacker.Magic;
        //Debug.Log(attackValue);

        float defenseValue = (attack.IsForceDamage) ? defender.Toughness : defender.Composition;
        //Debug.Log(defenseValue);

        float damage = attack.Damage * ((float)attackValue / defenseValue) * elementMatchup * elementBoost;
        Debug.Log(damage);


        int roundedDamage = Mathf.FloorToInt(damage);
        //Debug.Log(roundedDamage);

        defender.UpdateHealth(roundedDamage);

        //if (attack.Effects.Recoil)
        //{
        //    attacker.UpdateRecoil(roundedDamage * (int)attack.Effects.numberEffect);
        //}

        float[] damageAndMatchup = new float[2] { roundedDamage, elementMatchup };

        return damageAndMatchup;
    }

    //private void AdditionalCalcs(Attack _attack, Character attacker, Player target, AttackBase attack, out float healthVariable, out float area, out float screen, out float thirdHealth)
    //{
    //    healthVariable = 1;
    //    area = 1;
    //    screen = 1;
    //    thirdHealth = 1;
    //    if (target.armor && attack.IsForceDamage)
    //    {
    //        screen = 1 / 1.5f;
    //    }

    //    //if (attacker.Base.Speciality.Effects.ThirdHealth != Element.None)
    //    //{
    //    //    thirdHealth = ThirdHealth(attack, attacker);
    //    //}

    //    if ((target.barrier && attack.IsForceDamage) || (target.armor && !attack.IsForceDamage))
    //    {
    //        screen = 1 / 1.5f;
    //    }

    //    if (UserField != AreaID.None || OpponentField != AreaID.None)
    //    {
    //        area = Check(_attack);
    //    }

    //    if (1 == 1)
    //    {
    //        healthVariable = attacker.Health / attacker.MaxHealth;
    //    }
    //}

    public int EntryHazard(Character eneterer)
    {
        float type = ElementalMatchups.GetElementalMatchup(Element.Earth, eneterer.Base.Element1) * ElementalMatchups.GetElementalMatchup(Element.Earth, eneterer.Base.Element2);

        float d = eneterer.MaxHealth / 8 * type;
        int damage = Mathf.FloorToInt(d);
        eneterer.UpdateHealth(damage);

        return damage;
    }

    public float Check(Attack m)
    {
        float userArea = 1, opponentArea = 1, totalArea;
        switch (UserField)
        {
            case AreaID.Forest:
                if (m.Base.Element.Equals(Element.Plant))
                {
                    userArea = 1.5f;
                }
                break;
            case AreaID.Volcano:
                if (m.Base.Element.Equals(Element.Fire))
                {
                    userArea = 1.5f;
                }
                if (m.Base.Element.Equals(Element.Ice))
                {
                    userArea = 0.75f;
                }
                break;
            case AreaID.Ocean:
                if (m.Base.Element.Equals(Element.Lightning))
                {
                    userArea = 1.5f;
                }
                break;
            case AreaID.Polar:
                if (m.Base.Element.Equals(Element.Ice))
                {
                    userArea = 1.5f;
                }
                if (m.Base.Element.Equals(Element.Fire))
                {
                    userArea = 0.75f;
                }
                break;
            case AreaID.Storm:
                if (m.Base.Element.Equals(Element.Air))
                {
                    userArea = 1.5f;
                }
                break;
            case AreaID.Mountain:
                if (m.Base.Element.Equals(Element.Earth))
                {
                    userArea = 1.5f;
                }
                break;
            case AreaID.City:
                if (m.Base.Element.Equals(Element.Lightning) || m.Base.Element.Equals(Element.Metal))
                {
                    userArea = 1.5f;
                }
                break;
            case AreaID.Ooze:

                break;
            case AreaID.Luminous:
                if (m.Base.Element.Equals(Element.Ray))
                {
                    userArea = 1.5f;
                }
                if (m.Base.Element.Equals(Element.Space))
                {
                    userArea = 0.75f;
                }
                break;
            case AreaID.Rift:
                if (m.Base.Element.Equals(Element.Space))
                {
                    userArea = 1.5f;
                }
                if (m.Base.Element.Equals(Element.Ray))
                {
                    userArea = 0.75f;
                }
                break;
        }
        switch (OpponentField)
        {
            case AreaID.Forest:
                if (m.Base.Element.Equals(Element.Plant))
                {
                    opponentArea = 1.5f;
                }
                break;
            case AreaID.Volcano:
                if (m.Base.Element.Equals(Element.Fire))
                {
                    opponentArea = 1.5f;
                }
                if (m.Base.Element.Equals(Element.Ice))
                {
                    opponentArea = 0.75f;
                }
                break;
            case AreaID.Ocean:
                if (m.Base.Element.Equals(Element.Lightning))
                {
                    opponentArea = 1.5f;
                }
                break;
            case AreaID.Polar:
                if (m.Base.Element.Equals(Element.Ice))
                {
                    opponentArea = 1.5f;
                }
                if (m.Base.Element.Equals(Element.Fire))
                {
                    opponentArea = 0.75f;
                }
                break;
            case AreaID.Storm:
                if (m.Base.Element.Equals(Element.Air))
                {
                    userArea = 1.5f;
                }
                break;
            case AreaID.Mountain:
                if (m.Base.Element.Equals(Element.Earth))
                {
                    opponentArea = 1.5f;
                }
                break;
            case AreaID.City:
                if (m.Base.Element.Equals(Element.Lightning) || m.Base.Element.Equals(Element.Metal))
                {
                    opponentArea = 1.5f;
                }
                break;
            case AreaID.Ooze:

                break;
            case AreaID.Luminous:
                if (m.Base.Element.Equals(Element.Ray))
                {
                    opponentArea = 1.5f;
                }
                if (m.Base.Element.Equals(Element.Space))
                {
                    opponentArea = 0.75f;
                }
                break;
            case AreaID.Rift:
                if (m.Base.Element.Equals(Element.Space))
                {
                    opponentArea = 1.5f;
                }
                if (m.Base.Element.Equals(Element.Ray))
                {
                    opponentArea = 0.75f;
                }
                break;

        }
        totalArea = userArea * opponentArea;
        return totalArea;
    }

    public float ThirdHealth(Attack attack, Character character)
    {
        float damage = 1;
        //if (character.Base.Speciality.Effects.Blaze == true && attack.Base.Element ==
        //    character.Base.Speciality.Effects.ThirdHealth)
        //{
        //    damage = 1.3f;
        //}
        return damage;
    }
}