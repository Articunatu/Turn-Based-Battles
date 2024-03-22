using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ScriptableObjectGenerator : EditorWindow
{
    [MenuItem("MyMenu/Generate Scriptable Objects")]
    public static void GenerateScriptableObjects()
    {
        // Create a new instance of your Scriptable Object
        AttackBase scriptableObject = ScriptableObject.CreateInstance<AttackBase>();

        var elements = Enum.GetValues(typeof(Element));
        var weapons = new List<string>();
        var meleeAttacks = new List<string>();
        // Set any properties or data on the Scriptable Object as needed
        foreach (var element in elements)
        {
            foreach (var weapon in weapons)
            {
                AssetDatabase.CreateAsset(scriptableObject, $"Assets/{element}{weapon}.asset");    
            }
            foreach (var meleeAttack in meleeAttacks)
            {
                AssetDatabase.CreateAsset(scriptableObject, $"Assets/{element}{meleeAttack}.asset");
            }
        }
        // Create an asset file for the Scriptable Object
        AssetDatabase.SaveAssets();

        // Optional: Refresh the Unity Editor to show the newly created asset
        AssetDatabase.Refresh();

        Debug.Log("Scriptable Objects generated successfully!");
    }
}
