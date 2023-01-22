using System.Collections;
using UnityEngine;

public class ActionSelect : BattleBase
{
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
    }
    public void ActionSelection()
    {
        if (!userPlayer._character.IsRecharging)
        {
            userPlayer._character.Energy += 1; opponentPlayer._character.Energy += 1;
            if (userPlayer._character.Energy > 3)
            {
                userPlayer._character.Energy = 3;
            }
            if (opponentPlayer._character.Energy > 3)
            {
                opponentPlayer._character.Energy = 3;
            }
            dialog.UpdateEnergy(userPlayer._character.Energy.ToString());
            turn = States.Choose;
            if (!multiplayer)
            {
                CPUMove();
            }
            //Debug.Log(userPlayer._character.Agility);
            StartCoroutine(dialog.WriteDialog("Choose an action"));
            dialog.ActivateAttackSelectionUI(true);
        }
        else
        {
            Recharge();
        }
    }

    public void CPUMove()
    {
        if (opponentPlayer._character.IsRecharging)
        {
            Recharge2();
            return;
        }

        int r = Random.Range(0, 7);
        int hasNoEnergy = 0;

        for (int i = 0; i < opponentPlayer._character.Attacks.Count; i++)
        {
            if (opponentPlayer._character.Attacks[i].Cost > opponentPlayer._character.Energy)
            {
                hasNoEnergy++;
            }
        }

        if (r > 0 & hasNoEnergy <= opponentPlayer._character.Attacks.Count)
        {
            do
            {
                opponentPlayer._character.CurrentAttack = opponentPlayer._character.GetRandomAttack();
            }
            while (opponentPlayer._character.CurrentAttack.Cost > opponentPlayer._character.Energy);
            enemy = BattleAction.Attack;
            enemyAction = true;
        }

        else if (hasNoEnergy >= 4 & opponentTeam.Characters.FindAll(e => e.MaxHealth > 0).Count == 1)
        {
            Recharge2();
        }

        else
        {
            if (opponentTeam.Characters.FindAll(e => e.MaxHealth > 0).Count > 1)
            {
                int r2 = Random.Range(0, opponentTeam.Characters.FindAll(e => e.MaxHealth > 0).Count);
                if (opponentPlayer._character != opponentTeam.Characters[r2] && opponentTeam.Characters[r2].Health > 0)
                {
                    randomMember = opponentTeam.Characters[r2];
                    currentEnemy = r2;
                    enemy = BattleAction.Switch;
                    Debug.Log(opponentTeam.Characters[r2].Base.Name);
                    enemyAction = true;
                }
                else
                {
                    CPUMove();
                }
            }

            else //if (enemyParty.Figures.Count == 1)
            {
                CPUMove();
            }
        }
    }

    public void Recharge()
    {
        userPlayer._character.IsRecharging = true;
        player = BattleAction.Attack;
        dialog.ActivateAttackSelectionUI(false);
        speed.BeforeSpeed();
    }

    public void Recharge2()
    {
        opponentPlayer._character.IsRecharging = true;
        enemy = BattleAction.Attack;
        enemyAction = true;
    }

    public void FirstAttack()
    {
        AttackControl(0);
    }
    public void SecondAttack()
    {
        AttackControl(1);
    }
    public void ThirdAttack()
    {
        AttackControl(2);
    }
    public void FourthAttack()
    {
        AttackControl(3);
    }


    public void AttackControl(int index)
    {
        //if (userPlayer.Character_.Equipment.Effects.ChoiceLock)
        //{
        //    if (userPlayer.Character_.IsLocked)
        //    {
        //        while (userPlayer.Character_.LockedAttack != userPlayer.Character_.Attacks[currentAttack])
        //        {
        //            dialog.SetDialog($"This character can only use {userPlayer.Character_.LockedAttack}!");
        //        }
        //    }
        //    else
        //    {
        //        userPlayer.Character_.LockedAttack = userPlayer.Character_.Attacks[currentAttack];
        //    }
        //}
        userPlayer._character.CurrentAttack = userPlayer._character.Attacks[index];

        if (userPlayer._character.Attacks[index].Cost > userPlayer._character.Energy)
        {
            dialog.UpdateDialog("This character has no energy to use that attack right now!");
            return;
        }
        else
        {
            dialog.ActivateAttackSelectionUI(false);
            player = BattleAction.Attack;
            if (multiplayer)
            {
                playerAction = true;
                //Message other player's script that enemyAction = true
                StartCoroutine(MultiCheck());
            }
            else
            {
                speed.BeforeSpeed();
            }
        }
    }
    IEnumerator MultiCheck()
    {
        playerAction = true;
        yield return new WaitUntil(() => enemyAction == true);
        speed.BeforeSpeed();
    }

    public void OpenTeam()
    {
        teamUI.DisplayTeam(userTeam.Characters);
        dialog.ActivateAttackSelectionUI(false);
        teamUI.gameObject.SetActive(true);
    }

    #region Switch
    public void Switch1()
    {
        currentMember = 0;
        SwitchControl();
    }
    public void Switch2()
    {
        currentMember = 1;
        SwitchControl();
    }
    public void Switch3()
    {
        currentMember = 2;
        SwitchControl();
    }
    public void Switch4()
    {
        currentMember = 3;
        SwitchControl();
    }
    public void Switch5()
    {
        currentMember = 4;
        SwitchControl();
    }
    public void Switch6()
    {
        currentMember = 5;
        SwitchControl();
    }
    #endregion

    public void CloseParty()
    {
        teamUI.gameObject.SetActive(false);
        dialog.ActivateAttackSelectionUI(true);
    }
    public void SwitchControl()
    {
        selectedMember = userTeam.Characters[currentMember];
        if (selectedMember.Health <= 0)
        {
            dialog.UpdateDialog("That character has no will left to fight");
            return;
        }
        else if (selectedMember == userPlayer._character)
        {
            dialog.UpdateDialog("That character is already sent out");
            return;
        }
        else
        {
            teamUI.gameObject.SetActive(false);

            if (turn == States.Choose)
            {
                player = BattleAction.Switch;
                if (multiplayer)
                {
                    StartCoroutine(MultiCheck());
                }
                else
                {
                    speed.BeforeSpeed();
                }
            }
            else //if (turn == StateMachine.BattleEnd)
            {
                StartCoroutine(swap.SwitchPlayer());
            }
        }
    }

    public void ChooseMove(Attack attack, Character character)
    {
        Attack lockedMove;
        //if (character.Equipment.Effects.ChoiceLock)
        //{
        //    lockedMove = attack;
        //}
        //while (move != lockedMove)
        //{

        //}
    }
}