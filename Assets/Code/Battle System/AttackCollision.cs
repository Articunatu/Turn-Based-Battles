using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackCollision : TurnManager
{
    DamageCalculator dc;


    Player userPlayer, opponentPlayer;
    Dialog dialog;
    public Character randomMember, selectedMember;

    private void Start()
    {
        opponentPlayer = GetComponent<Entrance>().opponentPlayer;
        userPlayer = GetComponent<Entrance>().userPlayer;
        randomMember = GetComponent<Entrance>().randomMember;
        selectedMember = GetComponent<Entrance>().selectedMember;


        dialog = GetComponent<Dialog>();
        dc = GetComponent<DamageCalculator>();
    }

    public IEnumerator SpeedTie()
    {
        yield return dialog.WriteDialog($"Both characters are equally fast!");
        yield return new WaitForSeconds(0.5f);
        yield return dialog.WriteDialog($"{userPlayer._character.Base.Name} used {userPlayer._character.CurrentAttack.Base.Name} ");
        yield return new WaitForSeconds(0.5f);
        yield return dialog.WriteDialog($"& the opposing {opponentPlayer._character.Base.Name}\n used {opponentPlayer._character.CurrentAttack.Base.Name}!");
        userPlayer.PlayAttackStationaryAnimation(); opponentPlayer.PlayAttackStationaryAnimation();
        yield return new WaitForSeconds(0.75f);
        yield return dialog.WriteDialog($"Their attacks will result in a collision!");
        StartCoroutine(AttacksCollide(userPlayer._character, opponentPlayer._character, userPlayer._character.CurrentAttack, opponentPlayer._character.CurrentAttack));
    }

    IEnumerator AttacksCollide(Character player, Character opponent, Attack userAttack, Attack opponentAttack)
    {
        float playerType = ElementalMatchups.GetElementalMatchup(userAttack.Base.Element, opponentAttack.Base.Element);
        float enemyType = ElementalMatchups.GetElementalMatchup(opponentAttack.Base.Element, userAttack.Base.Element);

        float playerAttack = (userAttack.Base.IsMovement == true) ? player.Strength : player.Magic; ;
        float enemyAttack = (opponentAttack.Base.IsForceDamage == true) ? opponent.Toughness : opponent.Composition;

        bool isPlayerStronger;

        player.Energy -= userAttack.Cost;
        opponent.Energy -= opponentAttack.Cost;

        if (userAttack.Base.Damage * playerType >= opponentAttack.Base.Damage * enemyType * enemyAttack / playerAttack)
        {
            isPlayerStronger = true;
        }
        else
        {
            isPlayerStronger = false;
        }

        var strongerAttack = (isPlayerStronger) ? userAttack : opponentAttack;
        var weakerAttack = (!isPlayerStronger) ? userAttack : opponentAttack;
        var strongerCharacter = (isPlayerStronger) ? player : opponent;
        var weakerCharacter = (!isPlayerStronger) ? player : opponent;
        var strongElement = (isPlayerStronger) ? playerType : enemyType;
        var weakElement = (!isPlayerStronger) ? playerType : enemyType;
        var strongAttack = (isPlayerStronger) ? playerAttack : enemyAttack;
        var weakAttack = (!isPlayerStronger) ? playerAttack : enemyAttack;
        var strongPlayer = (isPlayerStronger) ? userPlayer : opponentPlayer;
        var weakPlayer = (!isPlayerStronger) ? userPlayer : opponentPlayer;

        float coefficient = (weakerAttack.Base.Damage * weakElement * (weakAttack / strongAttack)) / (strongerAttack.Base.Damage * strongElement);
        float moveDamage = strongerAttack.Base.Damage - strongerAttack.Base.Damage * coefficient;
        //int roundedMoveDamage = (int)moveDamage;  Might be useful?

        yield return dialog.WriteDialog($"{strongerCharacter.Base.Name}'s attack reigns supreme!");
        yield return new WaitForSeconds(1f);

        float restDamage = moveDamage / strongerAttack.Base.Damage;
        if (restDamage > 0.67)
        {
            yield return dialog.WriteDialog($"{weakerCharacter.Base.Name} gets hit by a huge chunk of {strongerAttack.Base.Name}!");
        }
        else if (restDamage > 0.33)
        {
            yield return dialog.WriteDialog($"{weakerCharacter.Base.Name} gets hit by the majority of {strongerAttack.Base.Name}!");
        }
        else
        {
            yield return dialog.WriteDialog($"{weakerCharacter.Base.Name} gets hit by a small part of {strongerAttack.Base.Name}!");
        }

        dc.CalculateDamage(strongerAttack, strongerCharacter, weakPlayer, restDamage);

        //float type = ElementalMatchups.GetElementalMatchup(strongerAttack.Base.Element, weakerCharacter.Base.Element1) * 
        //     ElementalMatchups.GetElementalMatchup(strongerAttack.Base.Element, weakerCharacter.Base.Element2);

        //float defense = (strongerAttack.Base.IsForceDamage) ? weakerCharacter.Toughness : weakerCharacter.Composition;

        //float damage = damageMove * ((float)strongAttack / defense) * type + 2;
        //int roundedDamage = Mathf.FloorToInt(d);
        ////Debug.Log(koeff);
        ////Debug.Log(moveDamage);
        ////Debug.Log(damageMove);
        ////Debug.Log(damage);

        //yield return new WaitForSeconds(0.5f);
        //weakPlayer.PlayHitAnimation();
        //yield return new WaitForSeconds(0.5f);

        //weakerCharacter.UpdateHP(roundedDamage);
        //yield return weakPlayer.Hud.UpdateHP();

        //if (weakPlayer.Character_.Health <= 0)
        //{
        //    if (weakPlayer == opponentPlayer)
        //    {
        //        yield return dialog.WriteDialog($"Opponent's {weakPlayer.Character_.Base.Name} is defeated");
        //    }
        //    else
        //    {
        //        yield return dialog.WriteDialog($"{weakPlayer.Character_.Base.Name} is defeated");
        //    }
        //    weakPlayer.PlayFaintAnimation();

        //    yield return new WaitForSeconds(2f);

        //    yield return RunAfterTurn(strongPlayer);
        //    StartCoroutine(CheckForBattleOver(weakPlayer));
        //}
        //else
        //{
        //    yield return RunAfterTurn(strongPlayer);
        //    yield return RunAfterTurn(weakPlayer);
        //    ActionSelection();
        //}
    }
}
