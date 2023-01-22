using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TeamMemeberUI : MonoBehaviour
{
    [SerializeField] Text nameText;
    [SerializeField] HealthBar healthBar;
    [SerializeField] Image image;

    Character _character;

    public void DisplayMember(Character character)
    {
        if (!CharacterTeam.lobby)
        {
            _character = character;
            nameText.text = character.Base.Name;
            healthBar.SetHealth((float)_character.Health / _character.MaxHealth);
        }
        
        image.sprite = character.Base.FrontSprite;
    }
}
