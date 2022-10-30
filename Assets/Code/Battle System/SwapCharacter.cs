using System.Collections;
using UnityEngine;

public class SwapCharacter : TurnManager
{
    private ActionSelect actionSelect;
    private Dialog dialog;

    Player userPlayer, opponentPlayer;
    public Character randomMember, selectedMember;


    private void Start()
    {
        opponentPlayer = GetComponent<Entrance>().opponentPlayer;
        userPlayer = GetComponent<Entrance>().userPlayer;
        randomMember = GetComponent<Entrance>().randomMember;
        selectedMember = GetComponent<Entrance>().selectedMember;

        dialog = GetComponent<Dialog>();
    }

    public IEnumerator SwitchPlayer()
    {
        if (turn == States.Battle)
        {
            yield return dialog.WriteDialog($"{userPlayer._character.Base.Name} swaps out!");
            userPlayer.PlaySwapAnimation();
            yield return new WaitForSeconds(2f);
            userPlayer.Setup(selectedMember);

            dialog.SetAttackNames(selectedMember.Attacks);

            yield return dialog.WriteDialog($"{selectedMember.Base.Name} enters the battle!");
            yield return new WaitForSeconds(0.6f);
            
            switching = true;

            if (turn == States.BattleEnd)
            {
                actionSelect.ActionSelection();
            }
        }
        else
        {
            userPlayer.Setup(selectedMember);

            dialog.SetAttackNames(selectedMember.Attacks);

            yield return dialog.WriteDialog($"{selectedMember.Base.Name} enters the battle!");
            yield return new WaitForSeconds(0.5f);
            
            yield return new WaitForSeconds(0.7f);
            defeatedUserCharacter = false;
            actionSelect.ActionSelection();
            
        }
    }

    public IEnumerator SwitchOpponent()
    {
        if (turn == States.Battle)
        {
            yield return dialog.WriteDialog($"Opponent's {opponentPlayer._character.Base.Name} switches out!");
            opponentPlayer.PlaySwapAnimation();
            yield return new WaitForSeconds(2.2f);
        }

        opponentPlayer.Setup(randomMember);

        yield return dialog.WriteDialog($"Opponent's {opponentPlayer._character.Base.Name} enters the battle!");
        //yield return new WaitForSeconds(0.65f);
        //EffectHandler(EffectState.EnterE);
        
        yield return new WaitForSeconds(3f);
        switching = true;
        if (turn == States.BattleEnd)
        {
            actionSelect.ActionSelection();
        }
        
    }

}