using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class CardDataReader : MonoBehaviour
{

    TextAsset csvFile; // CSVファイル
    List<string[]> csvDatas = new List<string[]>(); // CSVの中身を入れるリスト;
    public Vector3 position;
    public Quaternion rotation;

    private void Start()
    {
        CardDataList.cardDatas = new List<CardData>();
        csvFile = Resources.Load("CardData") as TextAsset; // Resouces下のCSV読み込み
        StringReader reader = new StringReader(csvFile.text);

        // , で分割しつつ一行ずつ読み込み
        // リストに追加していく

        //csvの一行目をスルー、後で変える。
        reader.ReadLine();
        while (reader.Peek() != -1) // reader.Peaekが-1になるまで
        {
            
            string line = reader.ReadLine(); // 一行ずつ読み込み
            csvDatas.Add(line.Split(',')); // , 区切りでリストに追加
        }
      
        foreach(string[] a in csvDatas)
        {
            CardData.Attribute attribute;
            CardData.Type type;
            EnumCommon.TryParse(a[6], out attribute);
            EnumCommon.TryParse(a[7], out type);

            CardDataList.cardDatas.Add(new CardData(a[1],int.Parse(a[2]), int.Parse(a[3]), int.Parse(a[4]), attribute,type, int.Parse(a[8])));
        }
        //CardDataList.cardDatas.Add(new CardData(csvDatas[1][1], attribute, int.Parse(csvDatas[1][3]), int.Parse(csvDatas[1][4])));
        foreach(CardData a in CardDataList.cardDatas)
        {
           // print(a.name+a.hp+a.attack+a.attribute);
        }
    }
}
