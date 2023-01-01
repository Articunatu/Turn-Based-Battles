using System.Collections;
using UnityEngine;

public class PreAttackUse : BattleBase
{

    public void PreMove(Attack attack, Player user, Player opponent)
    {

        //if (attack.Base.Effects.TwoTurnCharge)
        //{
        //    if (user.Character_.Equipment.Effects.PowerHerb)
        //    {

        //    }
        //    //break; ?
        //}

    }

    public float ThirdHealth(Attack attack, Character character)
    {
        float damage = 1;
        if (character.Base.Speciality.Effects == SpecialityEffect.smallHealthBoost
            && (attack.Base.Element == character.Base.Element1))
        {
            damage = 1.3f;
        }
        return damage;
    }
}
