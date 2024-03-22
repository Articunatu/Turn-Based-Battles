using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;

[CreateAssetMenu(fileName = "Character", menuName = "Create new character")]
public class CharacterBase : ScriptableObject
{
    [TextArea]
    [SerializeField] string description;
    [SerializeField] Sprite frontSprite, backSprite;

    [SerializeField] Element affinity1, affinity2;

    [SerializeField] int maxHealth, strength, toughness, magic, composition, agility;
    [SerializeField] List<LearnableAttack> learnableAttacks;

    [SerializeField] Speciality speciality;

    public Speciality Speciality
    {
        get { return speciality; }
    }

    public string Name
    {
        get { return name; }
    }

    public string Description
    {
        get { return description; }
    }
    public Sprite FrontSprite
    {
        get { return frontSprite; }
    }
    public Sprite BackSprite
    {
        get { return backSprite; }
    }

    public Element Element1
    {
        get { return affinity1; }
    }
    //public Element Element2
    //{
    //    get { return affinity2; }
    //}

    public Element Element2 { get; set; }

    public int MaxHealth
    {
        get { return maxHealth; }
    }

    public int Strength
    {
        get { return strength; }
    }
    public int Toughness
    {
        get { return toughness; }
    }
    public int Magic
    {
        get { return magic; }
    }

    public int Composition
    {
        get { return composition; }
    }

    public int Agility
    {
        get { return agility; }
    }
    public List<LearnableAttack> LearnableAttacks
    {
        get { return learnableAttacks; }
    }
}

[System.Serializable]
public class LearnableAttack
{
    [SerializeField] AttackBase attackBase;

    public AttackBase Base
    {
        get { return attackBase; }
    }
}

public enum Element
{
    Plant,
    Fire,
    Water,
    Ice,
    Lightning,
    Air,
    Earth,
    Metal,
    Chemical,
    Space,
    Ray,
    None
}

public enum Attribute
{
    Strength,
    Toughness,
    Magic,
    Composition,
    Agility,
    Energy
}

public class ElementalMatchups
{
    static float[,] matchupChart = new float[11, 11]
    {   
    // rows    = attacker
    // columns = defender
    { 0.5F,  0.5F,     2,    1,    1, 0.5F,    2, 0.5F, 0.5F,    1,    1},  //Plant     0
    {    2,  0.5F,  0.5F,    2,    1,    1, 0.5F,    2,    1,    1,    1},  //Fire      1
    { 0.5F,     2,  0.5F, 0.5F,    1,    1,    2,    1,    1,    1,    1},  //Water     2
    {    2,  0.5F,     1, 0.5F,    1,    2,    2, 0.5F,    1,    1,    1},  //Ice       3
    { 0.5F,     1,     2,    1, 0.5F,    2, 0.5F,    1,    1,    1,    1},  //Lightning 4
    {    2,     1,     1, 0.5F, 0.5F,    1,    1, 0.5F,    1,    1,    1},  //Air       5
    { 0.5F,     2,     1,    1,    2,    1,    1, 0.5F,    1,    1,    1},  //Earth     6
    {    1,  0.5F,  0.5F,    2, 0.5F,    1,    1, 0.5F,    2,    1,    1},  //Metal     7
    {    2,     1,     1,    1,    1,    1, 0.5F, 0.5F, 0.5F,    1,    1},  //Chemical  8
    {    1,     1,     1,    1,    1,    1,    1,    1,    1, 0.5F,    2},  //Space     9
    {    1,     1,     1,    1,    1,    1,    1,    1,    1,    2, 0.5F}   //Ray      10
   //    Pl     Fi     Wa    Ic    Li    Ai    Ea    Me    Ch    Sp    Ra   
    };

    public static float GetElementalMatchup(Element attackType, Element defenseType)
    {
        if (defenseType == Element.None)
        {
            return 1;
        }
        else
        {
            int attacker = (int)attackType; //row
            int defender = (int)defenseType; //column
            return matchupChart[attacker, defender];
        }
    }
}