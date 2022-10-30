using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Speciality", menuName = "Create new speciality")]
public class Speciality : ScriptableObject
{
    [SerializeField] SpecialityEffect effects;
    [SerializeField] Element element;
    [SerializeField] float numberValue;

    public SpecialityEffect Effects
    {
        get { return effects; }
    }

    public Element Element 
    { 
        get { return element; } 
    }

    public float NumberValue
    {
        get { return numberValue; }
    }
}

public enum SpecialityEffect
{
    none, absorbAttack, areaBoost, antiSpeciality, decreaseImmunity,
    reverseBoosts, noRecoil, antiAilment, smallHealthBoost
}

public enum AreaID
{
    None, Forest, Volcano, Ocean, Polar, Storm, Mountain, City, Ooze, Luminous, Rift
}
