using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardData 
{
    public enum Attribute
    {
        Rain,
        Thunder,
        Wind,
    }
    public enum Type
    {
        Attack,
    }
    public enum State
    {
        Normal,
        Paralysis
    }

    public string name;
    public int hp;
    public Attribute attribute = Attribute.Rain;
    public Type type = Type.Attack;
    public int atk;
    public int def;
    public int rare;
    public bool isAttack;
    public State state = State.Normal;

    public CardData(string name, Attribute attribute, int hp, int attack)
    {
        this.name = name;
        this.attribute = attribute;
        this.hp = hp;
        this.atk = attack;
    }

    public CardData(string name,int hp,int atk,int def, Attribute attribute, Type type,int rare)
    {
        this.name = name;
        this.hp = hp;
        this.atk = atk;
        this.def = def;
        this.attribute = attribute;
        this.type = type;
        this.rare = rare;
    }
    public CardData(){}
    // private Attribute attribute = Attribute.a;
}
