using System.Collections;
using UnityEngine;

public class PostAttackHit : TurnManager
{
    public void AfterMove(Attack attack, Character character)
    {
        //if (character.Equipment.Effects.BerryStats && character.MaxHealth <= character.MaxHealth / 3)
        //{
        //    character.ApplyChanges(character.Equipment.Effects.Boosts);
        //    character.Equipment = null;
        //}
    }
}