using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Entrance : TurnManager
{
    private ActionSelect actionSelect;

    [SerializeField] public Player userPlayer, opponentPlayer;
    [SerializeField] public Dialog dialog;
    [SerializeField] public CharacterTeam userTeam, opponentTeam;
    [SerializeField] public TeamUI teamUI;
    public Character selectedMember, randomMember;


    public void Start()
    {
        actionSelect = GetComponent<ActionSelect>();
        //userPlayer = GetComponents<Player>().FirstOrDefault(p => p.IsUser.Equals(true));
        //opponentPlayer = GetComponents<Player>().FirstOrDefault(p => p.IsUser.Equals(false));


        //userPlayer.gameObject.SetActive(false);
        //partyScreen.gameObject.SetActive(false);
        StartCoroutine(BeginBattle());
    }

    public IEnumerator BeginBattle()
    {
        opponentPlayer.Setup(opponentTeam.GetHealthyCharacter());
        yield return dialog.WriteDialog($"Opponent starts with {opponentPlayer._character.Base.Name}!");
        yield return new WaitForSeconds(0.5f);
        userPlayer.gameObject.SetActive(true);
        teamUI.gameObject.SetActive(transform);
        userPlayer.Setup(userTeam.GetHealthyCharacter());
        yield return dialog.WriteDialog($"{userPlayer._character.Base.Name}, let's go!");
        yield return new WaitForSeconds(0.75f);
        dialog.SetEnergy(userPlayer._character.Energy.ToString());

        teamUI.InitializeTeam();
        dialog.SetAttackNames(userPlayer._character.Attacks);
        turn = States.Lead;
        Entry();
    }

    void Entry()
    {
        if (turn == States.Lead)
        {
            actionSelect.ActionSelection();
        }
        else
        {
            EntryEffects(userPlayer, opponentPlayer);
            EntryEffects(opponentPlayer, userPlayer);
        }
    }

    public void EntryEffects(Player user, Player opponent)
    {
        if (user.entryDamage)
        {

        }

        if (user._character.Base.Speciality.Effects == SpecialityEffect.antiAilment)
        {
            // ?
        }
    }
}