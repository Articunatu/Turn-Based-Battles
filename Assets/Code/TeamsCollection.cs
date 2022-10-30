using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeamsCollection : MonoBehaviour
{
    [SerializeField] public List<CharacterTeam> CharacterTeams;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void InitializeTeams()
    {
        foreach (var item in CharacterTeams)
        {
            item.TeamInitialization();
        }
    }

    public void InitializeMembers()
    {

    }
}
