﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeamUI : MonoBehaviour
{
    TeamMemeberUI[] characterSlots;

    public void InitializeTeam()
    {
        characterSlots = GetComponentsInChildren<TeamMemeberUI>();
    }

    public void DisplayTeam(List<Character> characters)
    {
        for (int i = 0; i < characterSlots.Length; i++)
        {
            if (i < characters.Count)
            {
                characterSlots[i].DisplayMember(characters[i]);
            }
            else
            {
                characterSlots[i].gameObject.SetActive(false);
            }
        }
    }
}
