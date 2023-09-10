using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnBattle 
{
    //プレイヤーデータ
    public CharcterData PlayerData;
    //エネミーデータ
    public CharcterData EnemyData;


    public TurnBattle(CharcterData PlayerData, CharcterData EnemyData){
        this.PlayerData = PlayerData;
        this.EnemyData = EnemyData;
    }

    public TurnData TurnAction(){
        //返答する行動結果
        TurnData returnData = new TurnData(0,0,false,false);

        int playerDmg=0;
        int enemyDmg=0;
        //先行、後攻の判定の実施 プレイヤーが先行の場合
        if(PlayerData.IsFirstAttack(EnemyData.Speed)){
            returnData.IsPlayerFirstAttack = true;
            //プレイヤーの攻撃
            enemyDmg = EnemyData.AddDamage(PlayerData.Strength);
            //エネミーがまだ生きていれば、反撃
            if(!EnemyData.IsDead()){
                playerDmg = PlayerData.AddDamage(EnemyData.Strength);
            }
        }
        else{
            //エネミーの攻撃
            playerDmg = PlayerData.AddDamage(EnemyData.Strength);
            //プレイヤーがまだ生きていれば、反撃
            if(!PlayerData.IsDead()){
                enemyDmg = EnemyData.AddDamage(PlayerData.Strength);
            }
        }
        //結果を返すためにデータを埋め込む
        returnData.SetData(playerDmg,enemyDmg,PlayerData.IsDead(),EnemyData.IsDead());
        return returnData;

    }
}


//１ターンごとのデータを返す仕組み
public class TurnData{


    public bool IsPlayerFirstAttack=false;

    public int PlayerDmg;
    public int EnemyDmg;

    public bool PlayerIsDead;
    public bool EnemyIsDead;

    public TurnData(int PlayerDmg,int EnemyDmg,bool PlayerIsDead,bool EnemyIsDead)
    {
        this.PlayerDmg = PlayerDmg;
        this.EnemyDmg = EnemyDmg;
        this.PlayerIsDead  = PlayerIsDead;
        this.EnemyIsDead = EnemyIsDead;
    }

    public void SetData(int PlayerDmg,int EnemyDmg,bool PlayerIsDead,bool EnemyIsDead){
        this.PlayerDmg = PlayerDmg;
        this.EnemyDmg = EnemyDmg;
        this.PlayerIsDead  = PlayerIsDead;
        this.EnemyIsDead = EnemyIsDead;
    }
}
