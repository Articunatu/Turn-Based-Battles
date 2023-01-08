using System.Collections;
using System.Linq;
using UnityEngine;

public class AttackUsed : BattleBase
{
    DamageCalculator dc;
    EndTurn end;

    Dialog dialog;
    public Character randomMember, selectedMember;


    private void Start()
    {
        randomMember = GetComponent<Entrance>().randomMember;
        selectedMember = GetComponent<Entrance>().selectedMember;


        dialog = GetComponent<Dialog>();
        dc = GetComponent<DamageCalculator>();
        end = GetComponent<EndTurn>();
    }

    public IEnumerator UseAttack(Player sourcePlayer, Player targetPlayer, Attack attack)
    {
        if (!sourcePlayer._character.IsRecharging)
        {
            sourcePlayer._character.Energy -= attack.Cost;
            if (sourcePlayer.IsUser)
            {
                yield return dialog.WriteDialog($"{sourcePlayer._character.Base.Name} attacks with {attack.Base.Name}");
            }
            else if (!sourcePlayer.IsUser)
            {
                yield return dialog.WriteDialog($"Opponent {sourcePlayer._character.Base.Name} attacks with {attack.Base.Name}");
            }
            //effectsDB.PreMove(attack, sourcePlayer, targetPlayer);
            if (attack.Base.IsContact)
            {
                sourcePlayer.PlayAttackMovementAnimation();
            }
            else
            {
                sourcePlayer.PlayAttackStationaryAnimation();
            }

            yield return new WaitForSeconds(1f);
            //effectsDB.PreHit(attack, targetPlayer);
            targetPlayer.PlayHitAnimation();

            if (attack.Base.Damage == 0)
            {
                yield return UseAttackEffects(attack.Base.Effects, sourcePlayer, targetPlayer, attack);
            }
            else
            {
                float matchup = dc.CalculateDamage(attack, sourcePlayer._character, targetPlayer)[1];
                yield return targetPlayer.Hud.UpdateHP();
                if (matchup > 1)
                {
                    yield return dialog.WriteDialog($"Good matchup!");
                }
                if (matchup < 1)
                {
                    yield return dialog.WriteDialog($"Bad matchup...");
                }
                //damageCurrent = damageDetails.Damage;
            }

            if (attack.Base.Effects != null)
            {
                yield return UseAttackEffects(attack.Base.Effects, sourcePlayer, targetPlayer, attack);
            }

            foreach (AttackEffect effect in attack.Base.Effects.AttackEffect)
            {
                if (effect == AttackEffect.recoil)
                {
                    yield return UseAttackEffects(attack.Base.Effects, sourcePlayer, targetPlayer, attack);
                }
            }
            //EffectHandler(EffectState.AfterHit);

            if (targetPlayer._character.Health <= 0)
            {
                if (targetPlayer.IsUser)
                {
                    yield return dialog.WriteDialog($"{targetPlayer._character.Base.Name} is defeated");
                    defeatedUserCharacter = true;
                }
                else
                {
                    yield return dialog.WriteDialog($"Opponent {targetPlayer._character.Base.Name} is defeated");
                    defeatedTargetCharacter = true;
                }
                targetPlayer.PlayDefeatAnimation();

                yield return new WaitForSeconds(1.3f);
                //state = BattleState.BattleOver;

                StartCoroutine(end.CheckForBattleOver(targetPlayer));
            }
        }
        else
        {
            if (sourcePlayer.IsUser)
            {
                yield return dialog.WriteDialog($"{sourcePlayer._character.Base.Name} saves its energy this turn.");
            }
            else
            {
                yield return dialog.WriteDialog($"Opponent's {sourcePlayer._character.Base.Name} saves its energy this turn.");
            }
            sourcePlayer._character.IsRecharging = false;
            yield return new WaitForSeconds(1.3f);
        }
    }

    IEnumerator UseAttackEffects(AttackEffects effects, Player source, Player target, Attack attack)
    {
        /// Stat Boosting

        if (effects.Boosts != null)
        {
            if (!attack.Base.HasTarget)
            {
                source._character.ApplyChanges(effects.Boosts.ToList());
            }
            else
            {
                if (target._character.Health > 0)
                {
                    target._character.ApplyChanges(effects.Boosts.ToList());
                }
            }
        }

        ///All attack effects
        foreach (var item in effects.AttackEffect)
        {

            /// Heal
            if (item == AttackEffect.heal && source.IsUser)
            {
                source._character.UpdateHeal((int)(source._character.MaxHealth * attack.Base.Effects.numberEffect));
                yield return source.Hud.UpdateDrainHP();

                yield return new WaitForSeconds(1);
                yield return dialog.WriteDialog($"{source._character.Base.Name} restored some of its health.");
            }
            else if (item == AttackEffect.heal && !source.IsUser)
            {
                source._character.UpdateHealth((int)(damageCurrent * attack.Base.Effects.numberEffect));
                yield return dialog.WriteDialog($"Opponent's {source._character.Base.Name} restored some of its health.");
                yield return source.Hud.UpdateHP();
                yield return new WaitForSeconds(0.5f);
            }

            /// Recoil
            if (item == AttackEffect.recoil && source.IsUser)
            {
                source._character.UpdateHealth((int)(damageCurrent * attack.Base.Effects.numberEffect));
                yield return dialog.WriteDialog($"{source._character.Base.Name} is hurt by the recoil.");
                yield return source.Hud.UpdateHP();
                yield return new WaitForSeconds(0.5f);
            }
            else if (item == AttackEffect.recoil && !source.IsUser)
            {
                source._character.UpdateHealth((int)(damageCurrent * attack.Base.Effects.numberEffect));
                yield return dialog.WriteDialog($"Opponent's {source._character.Base.Name} is hurt by the recoil.");
                yield return source.Hud.UpdateHP();
                yield return new WaitForSeconds(0.5f);
            }

            ///Drain
            if (item == AttackEffect.drain && source.IsUser && attack.Base.Damage > 0)
            {
                source._character.UpdateHeal((int)(damageCurrent * attack.Base.Effects.numberEffect));
                yield return source.Hud.UpdateDrainHP();

                yield return new WaitForSeconds(1);
                yield return dialog.WriteDialog($"{source._character.Base.Name} restored some of its health.");
            }
            else if (item == AttackEffect.drain && !source.IsUser && attack.Base.Damage > 0)
            {
                source._character.UpdateHeal(damageCurrent * (int)attack.Base.Effects.numberEffect);
                yield return source.Hud.UpdateDrainHP();

                yield return new WaitForSeconds(1);
                yield return dialog.WriteDialog($"Opponent's {source._character.Base.Name} restored some of its health.");
            }
            else if (item == AttackEffect.drain && source.IsUser)
            {
                source._character.UpdateHeal(damageCurrent * (int)attack.Base.Effects.numberEffect - 10);
                yield return source.Hud.UpdateDrainHP();

                yield return new WaitForSeconds(1);
                yield return dialog.WriteDialog($"{source._character.Base.Name} restored some of its health.");
            }
            else if (item == AttackEffect.drain && !source.IsUser)
            {
                source._character.UpdateHeal(damageCurrent * (int)attack.Base.Effects.numberEffect - 10);
                yield return source.Hud.UpdateDrainHP();
                yield return new WaitForSeconds(1);
                yield return dialog.WriteDialog($"Opponent's {source._character.Base.Name} restored some of its health.");
            }

            if (item == AttackEffect.area)
            {
                yield return dialog.WriteDialog($"The field has turned to {attack.Base.Element}!");
            }
        }
    }

    public void MoveUsed(Attack attack, Player user, Player opponent)
    {
        foreach (var item in attack.Base.Effects.AttackEffect)
        {
            if (item == AttackEffect.entryDamage)
            {
                opponent.entryDamage = true;
            }
            if (item == AttackEffect.area)
            {
                //switch (item)
                //{
                //    case Element.Plant:
                //        if (user.IsUser)
                //        {
                //            UserField = AreaID.Forest;
                //        }
                //        else
                //        {
                //            OpponentField = AreaID.Forest;
                //        }
                //        break;
                //    case Element.Fire:
                //        if (user.IsUser)
                //        {
                //            UserField = AreaID.Volcano;
                //        }
                //        else
                //        {
                //            OpponentField = AreaID.Volcano;
                //        }
                //        break;
                //    case Element.Water:
                //        if (user.IsUser)
                //        {
                //            UserField = AreaID.Ocean;
                //        }
                //        else
                //        {
                //            OpponentField = AreaID.Ocean;
                //        }
                //        break;
                //    case Element.Ice:
                //        if (user.IsUser)
                //        {
                //            UserField = AreaID.Polar;
                //        }
                //        else
                //        {
                //            OpponentField = AreaID.Polar;
                //        }
                //        break;
                //    case Element.Lightning:
                //        if (user.IsUser)
                //        {
                //            UserField = AreaID.City;
                //        }
                //        else
                //        {
                //            OpponentField = AreaID.City;
                //        }
                //        break;
                //    case Element.Air:
                //        if (user.IsUser)
                //        {
                //            UserField = AreaID.Storm;
                //        }
                //        else
                //        {
                //            OpponentField = AreaID.Storm;
                //        }
                //        break;
                //    case Element.Earth:
                //        if (user.IsUser)
                //        {
                //            UserField = AreaID.Mountain;
                //        }
                //        else
                //        {
                //            OpponentField = AreaID.Mountain;
                //        }
                //        break;
                //    case Element.Metal:
                //        if (user.IsUser)
                //        {
                //            UserField = AreaID.City;
                //        }
                //        else
                //        {
                //            OpponentField = AreaID.City;
                //        }
                //        break;
                //    case Element.Chemical:
                //        if (user.IsUser)
                //        {
                //            UserField = AreaID.Ooze;
                //        }
                //        else
                //        {
                //            OpponentField = AreaID.Ooze;
                //        }
                //        break;
                //    case Element.Space:
                //        if (user.IsUser)
                //        {
                //            UserField = AreaID.Rift;
                //        }
                //        else
                //        {
                //            OpponentField = AreaID.Rift;
                //        }
                //        break;
                //    case Element.Ray:
                //        if (user.IsUser)
                //        {
                //            UserField = AreaID.Luminous;
                //        }
                //        else
                //        {
                //            OpponentField = AreaID.Luminous;
                //        }
                //        break;
                //    default:
                //        break;
                //}
            }

            if (item == AttackEffect.armor)
            {
                user.armor = true;
            }

            if (item == AttackEffect.barrier)
            {
                user.barrier = true;
            }

            if (item == AttackEffect.area)
            {
                Color color = new Color();
                switch (item)
                {
                //    case Element.Plant:
                //        if (user.IsUser)
                //        {
                //            TurnManager.UserField = AreaID.Forest;
                //        }
                //        else
                //        {
                //            TurnManager.OpponentField = AreaID.Forest;
                //        }
                //        color = Color.green;
                //        break;
                //    case Element.Fire:
                //        if (user.IsUser)
                //        {
                //            TurnManager.UserField = AreaID.Volcano;
                //        }
                //        else
                //        {
                //            TurnManager.OpponentField = AreaID.Volcano;
                //        }
                //        color = Color.red;
                //        break;
                //    case Element.Water:
                //        if (user.IsUser)
                //        {
                //            TurnManager.UserField = AreaID.Ocean;
                //        }
                //        else
                //        {
                //            TurnManager.OpponentField = AreaID.Ocean;
                //        }
                //        color = Color.blue;
                //        break;
                //    case Element.Ice:
                //        if (user.IsUser)
                //        {
                //            TurnManager.UserField = AreaID.Polar;
                //        }
                //        else
                //        {
                //            TurnManager.OpponentField = AreaID.Polar;
                //        }
                //        color = Color.cyan;
                //        break;
                //    case Element.Lightning:
                //        if (user.IsUser)
                //        {
                //            TurnManager.UserField = AreaID.City;
                //        }
                //        else
                //        {
                //            TurnManager.OpponentField = AreaID.City;
                //        }
                //        color = Color.yellow;
                //        break;
                //    case Element.Air:
                //        if (user.IsUser)
                //        {
                //            TurnManager.UserField = AreaID.Storm;
                //        }
                //        else
                //        {
                //            TurnManager.OpponentField = AreaID.Storm;
                //        }
                //        color = Color.white;
                //        break;
                //    case Element.Earth:
                //        if (user.IsUser)
                //        {
                //            TurnManager.UserField = AreaID.Mountain;
                //        }
                //        else
                //        {
                //            TurnManager.OpponentField = AreaID.Mountain;
                //        }
                //        color = Color.HSVToRGB(23, 72, 51);
                //        break;
                //    case Element.Metal:
                //        if (user.IsUser)
                //        {
                //            TurnManager.UserField = AreaID.City;
                //        }
                //        else
                //        {
                //            TurnManager.OpponentField = AreaID.City;
                //        }
                //        color = Color.grey;
                //        break;
                //    case Element.Chemical:
                //        if (user.IsUser)
                //        {
                //            TurnManager.UserField = AreaID.Ooze;
                //        }
                //        else
                //        {
                //            TurnManager.OpponentField = AreaID.Ooze;
                //        }
                //        color = Color.HSVToRGB(25, 90, 90);
                //        break;
                //    case Element.Space:
                //        if (user.IsUser)
                //        {
                //            TurnManager.UserField = AreaID.Rift;
                //        }
                //        else
                //        {
                //            TurnManager.OpponentField = AreaID.Rift;
                //        }
                //        color = Color.HSVToRGB(350, 50, 30);
                //        break;
                //    case Element.Ray:
                //        if (user.IsUser)
                //        {
                //            TurnManager.UserField = AreaID.Luminous;
                //        }
                //        else
                //        {
                //            TurnManager.OpponentField = AreaID.Luminous;
                //        }
                //        color = Color.HSVToRGB(300, 85, 85);
                //        break;
                }
                //if (user.IsUser)
                //{
                //    fieldTimerPlayer = 5;
                //    hud.Area.color = color;
                //    playerHud.Area.gameObject.SetActive(true);
                //}
                //else
                //{
                //    fieldTimerEnemy = 5;
                //    enemyHud.Area.color = color;
                //    enemyHud.Area.gameObject.SetActive(true);
                //}
            }

            if (item == AttackEffect.recharge)
            {
                if (opponent._character.MaxHealth > 0)
                {
                    user._character.IsRecharging = true;
                }
            }
        }
    }
}