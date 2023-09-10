using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Status : MonoBehaviour, IDamageable
{
    [SerializeField] private int _cardID = 0;
    public CardData cardData { private set; get; }
    private Image _charImage;

    private SkilBase _skil;

    //テスト用
    public Status target;
    // Start is called before the first frame update
    void Start()
    {
        this._skil = this.GetComponent<SkilBase>();
        cardData = new CardData();
        cardData = CardDataList.cardDatas[_cardID];
        print(cardData.name + cardData.hp + cardData.atk + cardData.def + cardData.attribute + cardData.type + cardData.rare);
        _charImage = this.GetComponent<Image>();
        _charImage.sprite = Resources.Load<Sprite>("Images/" + cardData.name);
        //Skil();
        //NormalAttack(target);
    }

    public void Skil(Status target)
    {
        _skil.Action(target,cardData.atk);
    }

    public int NormalAttack(GameObject target)
    {
        Status status = target.GetComponent<Status>();
        status.Damage(cardData.atk - status.cardData.def);
        return cardData.atk - status.cardData.def;
    }

    public void Damage(int value)
    {
        this.cardData.hp -= value;
    }


}
