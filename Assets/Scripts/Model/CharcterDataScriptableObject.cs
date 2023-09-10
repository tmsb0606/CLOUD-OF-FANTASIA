using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Create CharcterData")]
public class CharcterDataScriptableObject : ScriptableObject
{
    public int Hp;
    public int Strength;
    public int Speed;
}