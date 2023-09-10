using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnLogic : SingletonMonoBehaviour<TurnLogic>
{

    CharcterData PlayerData;
    CharcterData EnemyData;
    TurnBattle turnBattleData;

    public List<string> TurnStatusTextList = new List<string>();

    public void SetPlayerData(CharcterDataScriptableObject player, CharcterDataScriptableObject enemy)
    {
        TurnStatusTextList.Clear();
        PlayerData = new CharcterData(player);
        EnemyData = new CharcterData(enemy) ;
        turnBattleData = new TurnBattle(PlayerData, EnemyData);
        
        TurnStatusTextList.Add($"PlayerData.Hp:{PlayerData.Hp} EnemyData.Hp:{EnemyData.Hp}");
    }


    public bool IsEncount(){
           if(EnemyData == null){
                return false;
           }
           return true;
    }

    public void GoNextTurn(){
        TurnData tempTurnData;
        TurnStatusTextList.Clear();

         if(EnemyData != null){
            tempTurnData = turnBattleData.TurnAction();
            if (tempTurnData.IsPlayerFirstAttack)
            {
                TurnStatusTextList.Add($"プレイヤーの先行 敵に{tempTurnData.EnemyDmg} のダメージ");
                TurnStatusTextList.Add($"敵の反撃　プレイヤーに:{tempTurnData.PlayerDmg}のダメージ");

                TurnStatusTextList.Add($"PlayerData.Hp:{PlayerData.Hp} EnemyData.Hp:{EnemyData.Hp}");
            }
            else
            {
                TurnStatusTextList.Add($"敵の先行　プレイヤーに:{tempTurnData.PlayerDmg}のダメージ");
                TurnStatusTextList.Add($"プレイヤーの反撃 敵に{tempTurnData.EnemyDmg} のダメージ");

                TurnStatusTextList.Add($"PlayerData.Hp:{PlayerData.Hp} EnemyData.Hp:{EnemyData.Hp}");

            }
          
            if(EnemyData.IsDead()){
                TurnStatusTextList.Add("敵をたおした！");
                EnemyData = null;
                turnBattleData = null;
            }else if(PlayerData.IsDead()){
                TurnStatusTextList.Add("プレイヤーが倒れた！");
                EnemyData = null;
                turnBattleData = null;
            }
         
         }
         else{
            TurnStatusTextList.Add("敵は倒しています。リセットしてください。");
        }
    }
}
