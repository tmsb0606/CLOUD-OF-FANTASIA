using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class CharcterData
{
    public int Hp;
    public int Strength;
    public int Speed;

    public CharcterData(CharcterDataScriptableObject charData)
    {
        Hp = charData.Hp;
        Strength = charData.Strength;
        Speed = charData.Speed;
    }


   public CharcterData(int _Hp,int _Str,int _Speed){
        UnityEngine.Random.InitState(DateTime.Now.Millisecond);
        Strength = _Str;
        Speed = _Speed;
    }

    public int AddDamage(int dmgBase){
        int Dmg;
        Dmg = dmgBase;;

        //ダメージに幅を持たせるならこのような感じだけど、これだと説得力がない
        //カードやスキルによって、安定しているけど爆発力にかける、ダメージはでかいけど低いときもあるというものを入れるとゲームらしくなる
        //int damageRange = UnityEngine.Random.Range(-2, 2);
        //Dmg += damageRange;

        Hp -= Dmg;
        return Dmg;
    }

    public bool IsDead(){
        if(Hp <= 0){
            return true;
        }
        else{
            return false;
        }
    }

    /// <summary>
    /// プレイヤーが先行のときtrueを返す
    /// </summary>
    /// <param name="Enemy_Speed"></param>
    /// <returns></returns>
    public bool IsFirstAttack(int Enemy_Speed){

        //プレイヤーの速度+エネミーの速度を足して、ランダムな値を作成
        //ランダムな値がプレイヤーの値以下ならプレイヤーが先行
        //つまりプレイヤーの速度が高いほど比率で先行確率が高まる
        int AllSpeed = Speed + Enemy_Speed;
        int RandomValue = UnityEngine.Random.Range (0, AllSpeed);
        if(RandomValue <= Speed){
            return true;
        }
        else{
            return false;
        }
    }
}
