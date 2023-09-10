using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealSkil : SkilBase
{
    public override void Action(Status target,int value)
    {
        print("heal");
        target.cardData.hp += value;
    }
}