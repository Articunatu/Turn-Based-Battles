using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class CharacterTeam : MonoBehaviour
{
    [SerializeField] List<Character> characters;
    [SerializeField] TeamUI teamFigures;
    [SerializeField] string teamName;
    [SerializeField] Text partyName;
    [SerializeField] bool isCurrent;
     static public bool lobby = false;

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
        foreach (var figure in characters)
        {
            figure.SetAttacks();
        }

        if (lobby)
        {
            partyName.text = teamName;
            teamFigures.InitializeTeam();
            foreach (var figure in characters)
            {
                teamFigures.SetTeam(characters);
            }
        }
    }

    public Character GetHealthyCharacter()
    {
        return characters.Where(x => x.Health > 0).FirstOrDefault();
    }
}
