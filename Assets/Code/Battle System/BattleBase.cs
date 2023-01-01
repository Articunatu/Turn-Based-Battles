using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum BattleState { Start, ActionSelect, PlayerAttack, RunningTurn, Busy, PartyScreen, Switching, BattleOver }
public enum States { Lead, SendOut, Entrance, Choose, Party, Switch, Battle, BeforeSpeed, SpeedCompare, PreMove, Damage, AfterMove, BattleEnd, GameOver }
public enum BattleAction { Attack, Switch, Forfeit }
public enum EffectState { EnterP, EnterE, Speed, Use, PreHit, AfterHit, TurnEnd }

public class BattleBase : MonoBehaviour
{
    [HideInInspector] public States turn;
    [HideInInspector] public AreaID UserField, OpponentField;
    [HideInInspector] public int fieldTimerPlayer = 5, fieldTimerEnemy = 5, damageCurrent;



    [HideInInspector] public BattleAction player, enemy;

    [HideInInspector] public int currentAttack, currentMember, currentEnemy = 0, opponentAttackSpeed, userAttackSpeed;

    [HideInInspector] public bool defeatedUserCharacter, defeatedTargetCharacter, switching, entryDamage;
    [HideInInspector] public bool multiplayer, playerAction, enemyAction;
}
    //[SerializeField] Entrance entrance;[SerializeField] ActionSelect actionSelect;[SerializeField] SpeedComparison speedComparison;
    //[SerializeField] PreAttackUse preAttackUse;[SerializeField] DamageCalculator damageCalculator;[SerializeField] PreAttackHit preAttackHit;
    //[SerializeField] PostAttackHit postAttackHit;[SerializeField] AttackUsed attackUsed;[SerializeField] SwapCharacter swapCharacter;
    //[SerializeField] AttackCollision attackCollision;[SerializeField] EndTurn endTurn;

    //void Start()
    //{
    //    userPlayer.gameObject.SetActive(false);
    //    partyScreen.gameObject.SetActive(false);
    //    StartCoroutine(entrance.BeginBattle());
    //}


    //IEnumerator ShowDamageDetails(DamageDetails damageDetails)
    //{
    //    if (damageDetails.ElementMatchup > 1)
    //    {
    //        yield return dialog.TypeDialog("The attack is favourly matched!");
    //    }
    //    else if (damageDetails.ElementMatchup < 1)
    //    {
    //        yield return dialog.TypeDialog("The attack is resisted!");
    //    }
    //}