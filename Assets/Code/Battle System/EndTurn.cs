using System.Collections;
using UnityEngine;

public class EndTurn : BattleBase
{
    private ActionSelect actionSelect;
    private SwapCharacter switchCharacter;

    SpeedComparison speed;
    SwapCharacter swap;
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
        actionSelect = GetComponent<ActionSelect>();
        switchCharacter = GetComponent<SwapCharacter>();
    }

    public IEnumerator AfterSpeed(Player user, Player target)
    {
        turn = States.BattleEnd;
        if (!defeatedUserCharacter && !defeatedTargetCharacter)
        {
            if (user._character.Agility >= target._character.Agility)
            {
                yield return RunAfterTurn(user);
                yield return RunAfterTurn(target);
                if (defeatedUserCharacter)
                {
                    StartCoroutine(CheckForBattleOver(user));
                }
                else
                {
                    actionSelect.ActionSelection();
                }
                if (defeatedTargetCharacter)
                {
                    StartCoroutine(CheckForBattleOver(target));
                }
            }
            else
            {
                yield return RunAfterTurn(target);
                yield return RunAfterTurn(user);

                if (defeatedUserCharacter)
                {
                    StartCoroutine(CheckForBattleOver(user));
                }
                else
                {
                    actionSelect.ActionSelection();
                }
                if (defeatedTargetCharacter)
                {
                    StartCoroutine(CheckForBattleOver(target));
                }
            }
        }
        else
        {
            if (!defeatedUserCharacter && defeatedTargetCharacter)
            {
                StartCoroutine(RunAfterTurn(user));
                StartCoroutine(CheckForBattleOver(target));
                yield return new WaitForSeconds(2f);
            }
            else if (defeatedUserCharacter && !defeatedTargetCharacter)
            {
                yield return RunAfterTurn(target);
                yield return new WaitForSeconds(0.5f);

                StartCoroutine(CheckForBattleOver(user));
            }
            else //(faintPlayer && faintEnemy)
            {
                StartCoroutine(CheckForBattleOver(user));

                StartCoroutine(CheckForBattleOver(target));
                
                yield return new WaitForSeconds(1f);
            }
        }
    }

    IEnumerator RunAfterTurn(Player user)
    {
        //if (state == BattleState.BattleOver) yield break;
        //yield return new WaitUntil(() => state == BattleState.RunningTurn);

        /// Damaging statuses will hurt the character after the turn
        yield return user.Hud.UpdateHP();
        if (user._character.Health <= 0)
        {
            if (user.IsUser)
            {
                yield return dialog.WriteDialog($"{user._character.Base.Name} is defeated");
                yield return new WaitForSeconds(1);
                defeatedUserCharacter = true;
            }
            else
            {
                yield return dialog.WriteDialog($"Opponent's {user._character.Base.Name} is defeated");
                defeatedTargetCharacter = true;
            }
            user.PlayDefeatAnimation();
            CheckForBattleOver(user);
            yield return new WaitForSeconds(1.3f);
        }

        if (UserField != AreaID.None)
        {
            fieldTimerPlayer -= 1;
            if (fieldTimerPlayer <= 0)
            {
                yield return dialog.WriteDialog($"The {UserField} disappeared from the field.");
                UserField = AreaID.None;
            }
        }

        if (OpponentField != AreaID.None)
        {
            fieldTimerEnemy -= 1;
            if (fieldTimerPlayer <= 0)
            {
                yield return dialog.WriteDialog($"The opponent's {OpponentField} disappaered from the field.");
                OpponentField = AreaID.None;
            }
        }

        if ((user.IsUser && !defeatedUserCharacter) || (!user.IsUser && !defeatedTargetCharacter))
        {
            if (user._character.AttributeChanges != null)
            {
                AttributeChange[] statChanges = user._character.AttributeChanges.ToArray();
                foreach (var statChange in statChanges)
                {
                    statChange.Timer -= 1;
                    if (statChange.Timer <= 0)
                    {

                        user._character.AttributeBoosts[statChange.AttributeBoost.stat] -= statChange.AttributeBoost.boost;

                        string statDialog;
                        if (user._character.AttributeBoosts[statChange.AttributeBoost.stat] != 0)
                        {
                            statDialog = user.IsUser ?
                            $"Some of {user._character.Base.Name}'s {statChange.AttributeBoost.stat} boosts disappeared." :
                            $"Some of the opponent's {user._character.Base.Name}'s {statChange.AttributeBoost.stat} boosts disappeared.";
                        }
                        else
                        {
                            statDialog = user.IsUser ?
                            $"{user._character.Base.Name}'s {statChange.AttributeBoost.stat} returned to normal." :
                            $"Opponent's {user._character.Base.Name}'s {statChange.AttributeBoost.stat} returned to normal.";
                        }

                        yield return dialog.WriteDialog(statDialog);
                        //yield return new WaitForSeconds(1.67f);
                        user._character.AttributeChanges.Remove(statChange);
                        statChange.Timer = 3;
                        //i -= 1;
                    }
                }
            }
        }
    }

    public IEnumerator CheckForBattleOver(Player defeatedCharacter)
    {
        turn = States.BattleEnd;

        if (defeatedCharacter.IsUser)
        {
            var nextCharacter = userTeam.GetHealthyCharacter();
            if (nextCharacter != null)
            {
                //faintPlayer = false;
                actionSelect.OpenTeam(); //Bug
            }
            else
            {
                defeatedCharacter.Hud.enabled = false;
                yield return dialog.WriteDialog($"Opponent has won the battle");
            }
        }
        else
        {
            var nextCharacter = opponentTeam.GetHealthyCharacter();
            yield return new WaitForSeconds(1f);
            if (nextCharacter != null)
            {
                defeatedTargetCharacter = false;
                yield return switchCharacter.SwitchOpponent();
            }
            else
            {
                yield return dialog.WriteDialog($"You have defeated the oppenent!");
                yield return new WaitForSeconds(1f);
                yield return dialog.WriteDialog($"Player recieved 10.000 euros!");
            }
        }
    }

    public void BattleEnd(Attack attack, Character character)
    {

    }
}