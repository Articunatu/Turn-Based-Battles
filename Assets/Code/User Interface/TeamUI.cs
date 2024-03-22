using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeamUI : MonoBehaviour
{
    TeamMemeberUI[] CharacterSlots;
    [SerializeField] public CharacterTeam Team;

    //private void Start()
    //{
    //    InitializeTeam();
    //    DisplayTeam(team.Characters);
    //}

    void AllTeamsRender()
    {
        InitializeTeam();
        DisplayTeam(Team.Characters);
    }

    public void InitializeTeam()
    {
        CharacterSlots = GetComponentsInChildren<TeamMemeberUI>();
    }

    public void DisplayTeam(List<Character> characters)
    {
        for (int i = 0; i < CharacterSlots.Length; i++)
        {
            if (i < characters.Count)
            {
                CharacterSlots[i].DisplayMember(characters[i]);
            }
            else
            {
                CharacterSlots[i].gameObject.SetActive(false);
            }
        }
    }

    public void ViewTeamMenu()
    {
        Team.TeamInitialization();
    }
}
