using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class CharacterTeam : MonoBehaviour
{
    [SerializeField] List<Character> characters;
    [SerializeField] string teamName;
    [SerializeField] Text partyName;
    [SerializeField] bool isCurrent;
     static public bool lobby = true;

    public List<Character> Characters
    {
        get
        {
            return characters;
        }
    }

    public void Start()
    {
        TeamInitialization();
    }

    public void TeamInitialization()
    {
        foreach (var character in characters)
        {
            character.SetAttacks();
        }
    }

    public Character GetHealthyCharacter()
    {
        return characters.FirstOrDefault(character => character.Health > 0);
    }
}
