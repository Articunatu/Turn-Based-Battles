using System.Collections;
using UnityEngine;


public class SpeedComparison : BattleBase
{
    private SwapCharacter swap;
    private EndTurn et;
    private AttackUsed au;
    AttackCollision ac;

    SpeedComparison speed;
    Player userPlayer, opponentPlayer;
    Dialog dialog;
    public Character randomMember, selectedMember;
    CharacterTeam userTeam, opponentTeam;
    TeamUI teamUI;

    private void Start()
    {
        opponentPlayer = GetComponent<Entrance>().opponentPlayer;
        opponentTeam = GetComponent<Entrance>().opponentTeam;
        userPlayer = GetComponent<Entrance>().userPlayer;
        userTeam = GetComponent<Entrance>().userTeam;
        randomMember = GetComponent<Entrance>().randomMember;
        selectedMember = GetComponent<Entrance>().selectedMember;
        teamUI = GetComponent<Entrance>().teamUI;

        speed = GetComponent<SpeedComparison>();
        swap = GetComponent<SwapCharacter>();
        dialog = GetComponent<Dialog>();
        et = GetComponent<EndTurn>();
        au = GetComponent<AttackUsed>();
        ac = GetComponent<AttackCollision>();
    }

    public void BeforeSpeed()
    {
        turn = States.Battle;
        //EffectHandler(EffectState.Speed);
        StartCoroutine(CompareSpeed());
    }

    IEnumerator CompareSpeed()
    {
        //Before speed: User/Opponent's ablity item ();
        if (player == BattleAction.Switch && enemy == BattleAction.Switch)
        {
            if (userPlayer._character.Agility >= opponentPlayer._character.Agility)
            {
                StartCoroutine(swap.SwitchPlayer());
                yield return new WaitUntil(() => switching == true);
                StartCoroutine(swap.SwitchOpponent());
                yield return new WaitForSeconds(2.7f);
                yield return et.AfterSpeed(userPlayer, opponentPlayer);
                switching = false;
            }
            else
            {
                StartCoroutine(swap.SwitchOpponent());
                yield return new WaitUntil(() => switching == true);
                StartCoroutine(swap.SwitchPlayer());
                yield return new WaitForSeconds(2.7f);
                yield return et.AfterSpeed(userPlayer, opponentPlayer);
            }
        }
        else if (player == BattleAction.Switch && enemy == BattleAction.Attack)
        {
            yield return swap.SwitchPlayer();
            yield return new WaitUntil(() => switching == true);
            Attack opponentAttack = opponentPlayer._character.CurrentAttack;
            yield return au.UseAttack(opponentPlayer, userPlayer, opponentAttack);
            yield return new WaitForSeconds(1f);
            yield return et.AfterSpeed(userPlayer, opponentPlayer);
            switching = false;

        }
        else if (player == BattleAction.Attack && enemy == BattleAction.Switch)
        {
            yield return swap.SwitchOpponent();
            yield return new WaitUntil(() => switching == true);
            Attack userAttack = userPlayer._character.CurrentAttack;
            yield return au.UseAttack(userPlayer, opponentPlayer, userAttack);
            yield return new WaitForSeconds(1f);
            yield return et.AfterSpeed(userPlayer, opponentPlayer);
            switching = false;
        }
        else
        {
            userPlayer._character.CurrentAttack = userPlayer._character.CurrentAttack;
            if (!opponentPlayer._character.IsRecharging)
            {
                opponentPlayer._character.CurrentAttack = opponentPlayer._character.GetRandomAttack();
                opponentAttackSpeed = opponentPlayer._character.CurrentAttack.Base.Speed;
            }

            userAttackSpeed = userPlayer._character.CurrentAttack.Base.Speed;


            bool userAttacksFirst = true;

            if (opponentAttackSpeed > userAttackSpeed)
            {
                userAttacksFirst = false;
            }
            else if (opponentAttackSpeed == userAttackSpeed)
            {
                if (userPlayer._character.Agility > opponentPlayer._character.Agility)
                {
                    userAttacksFirst = true;
                }
                else if (userPlayer._character.Agility < opponentPlayer._character.Agility)
                {
                    userAttacksFirst = false;
                }
                else
                {
                    if (userPlayer._character.IsRecharging)
                    {
                        userAttacksFirst = false;
                    }
                    else if (opponentPlayer._character.IsRecharging)
                    {
                        userAttacksFirst = true;
                    }
                    else
                    {
                        StartCoroutine(ac.SpeedTie());
                        yield break;
                    }
                }
            }

            var firstPlayer = (userAttacksFirst) ? userPlayer : opponentPlayer;
            var secondPlayer = (userAttacksFirst) ? opponentPlayer : userPlayer;

            var secondCharacter = secondPlayer._character;

            // First Turn
            yield return au.UseAttack(firstPlayer, secondPlayer, firstPlayer._character.CurrentAttack);
            //EffectHandler(EffectState.AfterHit);

            if (secondCharacter.Health > 0)
            {
                // Second Turn
                yield return au.UseAttack(secondPlayer, firstPlayer, secondPlayer._character.CurrentAttack);
                yield return new WaitForSeconds(1f);
                yield return et.AfterSpeed(userPlayer, opponentPlayer);
            }
        }
    }


    public void BeforeSpeed(Attack attack, Player player)
    {
        //if (player.Character_.Equipment.Effects.Boosts != null && !player.Character_.IsItemActive)
        //{
        //    player.Character_.ApplyChanges(player.Character_.Equipment.Effects.);
        //    player.Character_.IsItemActive = true;
        //}
        //if (attack.Base.Effects.Where() == AttackEffect.area && (UserField == AreaID.Forest
        //                             || OpponentField == AreaID.Forest))
        //{
        //    if (player.IsUser)
        //    {
        //        userAttackSpeed += 1;
        //    }
        //    else
        //    {
        //        opponentAttackSpeed += 1;
        //    }
        //}

        if ((player._character.Base.Speciality.name.Equals("Swift Swim") && (UserField == AreaID.Ocean
                                         ||OpponentField == AreaID.Ocean))
                                         ||
            (player._character.Base.Speciality.name.Equals("Chlorophyll") && (UserField == AreaID.Volcano
                                         || OpponentField == AreaID.Volcano))
                                         ||
            (player._character.Base.Speciality.name.Equals("Sand Rush") && (UserField == AreaID.Mountain
                                         || OpponentField == AreaID.Mountain))
                                         ||
            (player._character.Base.Speciality.name.Equals("Slush Rush") && (UserField == AreaID.Polar
                                         || OpponentField == AreaID.Polar)))
        {
            //player.Character_.ApplyChanges(player.Character_.Base.Speciality.Effects);
        }
    }
}
