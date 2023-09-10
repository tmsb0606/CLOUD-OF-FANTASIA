using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class GameController : MonoBehaviour
{
    public CharcterDataScriptableObject playerDataScriptableObject;
    public List<CharcterDataScriptableObject> enemyDataScriptableObjectList = new List<CharcterDataScriptableObject>();

    public Text CurrentStatus;

   

    void Start()
    {
        InitData();
    }

    void Update()
    {
    }

    private void InitData()
    {
        //敵のデータを複数用意しておきました
        //現在はランダムに選ぶようになっていますがデッキ編成機能を追加したり、
        //味方、敵複数のキャラクターで戦闘する要素をいれてみるとよりゲームらしくなるかと思います
        TurnLogic.Instance.SetPlayerData(playerDataScriptableObject,GetRandom(enemyDataScriptableObjectList));
        CurrentStatus.text = string.Join("\n", TurnLogic.Instance.TurnStatusTextList.Select(x => x.ToString())); ;
    }

    public void OnClickReset()
    {
        InitData();
    }

    public void OnclickNext()
    {
        TurnLogic.Instance.GoNextTurn();
        CurrentStatus.text = string.Join("\n", TurnLogic.Instance.TurnStatusTextList.Select(x => x.ToString())); ;
    }

    //配列やリストを渡すとランダムに一つの要素を返す
    static T GetRandom<T>(IList<T> Params)
    {
        return Params[Random.Range(0, Params.Count)];
    }

}
