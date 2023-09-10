using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class CardDataReader : MonoBehaviour
{

    TextAsset csvFile; // CSV�t�@�C��
    List<string[]> csvDatas = new List<string[]>(); // CSV�̒��g�����郊�X�g;
    public Vector3 position;
    public Quaternion rotation;

    private void Start()
    {
        CardDataList.cardDatas = new List<CardData>();
        csvFile = Resources.Load("CardData") as TextAsset; // Resouces����CSV�ǂݍ���
        StringReader reader = new StringReader(csvFile.text);

        // , �ŕ�������s���ǂݍ���
        // ���X�g�ɒǉ����Ă���

        //csv�̈�s�ڂ��X���[�A��ŕς���B
        reader.ReadLine();
        while (reader.Peek() != -1) // reader.Peaek��-1�ɂȂ�܂�
        {
            
            string line = reader.ReadLine(); // ��s���ǂݍ���
            csvDatas.Add(line.Split(',')); // , ��؂�Ń��X�g�ɒǉ�
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
