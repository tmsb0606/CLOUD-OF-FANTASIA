using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour, IDamageable
{
    public List<Status> deck = new List<Status>();
    public int maxHP;
    public int hp;
    public GameObject player;
    private void Start()
    {
        foreach (Status status in deck)
        {
            maxHP += status.cardData.hp;
        }
        hp = maxHP;
        print(hp);
        
    }
    public int NormalAttack(int num)
    {
        int damage = deck[num].cardData.atk;
        //クリティカル
        if (Random.RandomRange(0, 100) < 20)
        {
            damage *= 2; 
        }
        Debug.Log("ダメージ" + damage);
        return damage;
    }

    public int NormalAttack(int num,EnemyManager enemy)
    {
        Status target = enemy.status;
        int damage = (int)(deck[num].cardData.atk * Advantage(deck[num].cardData.attribute,target.cardData.attribute));

        //クリティカル
        if (Random.RandomRange(0, 100) < 20)
        {
            damage *= 2;
        }
        Debug.Log("ダメージ" + damage);
        return damage;
    }


    public int ComboSkil(int[] nums)
    {
        int damage = 0;
        CardData status1 = deck[nums[0]].cardData;
        CardData status2 = deck[nums[1]].cardData;

        if (status1.attribute == CardData.Attribute.Thunder)
        {
            if(status2.attribute == CardData.Attribute.Thunder)
            {
                damage = ExtraLargeAttack(status1.atk,status2.atk);
            }else if(status2.attribute == CardData.Attribute.Wind)
            {
                damage = LargeAttack(status1.atk, status2.atk);
            }else if(status2.attribute == CardData.Attribute.Rain)
            {
                damage = LargeAttack(status1.atk, status2.atk);
            }
        }else if(status1.attribute == CardData.Attribute.Rain)
        {
            if (status2.attribute == CardData.Attribute.Thunder)
            {
                damage = LargeAttack(status1.atk, status2.atk);
            }
            else if (status2.attribute == CardData.Attribute.Wind)
            {
                damage = ExtraLargeAttack(status1.atk, status2.atk);
            }
            else if (status2.attribute == CardData.Attribute.Rain)
            {
                damage = LargeAttack(status1.atk, status2.atk); ;
            }
        }else if(status1.attribute == CardData.Attribute.Wind)
        {
            if (status2.attribute == CardData.Attribute.Thunder)
            {
                Heal(3000);
                damage =int.MaxValue;

            }
            else if (status2.attribute == CardData.Attribute.Wind)
            {
                Heal(3000);
                damage = int.MaxValue;
            }
            else if (status2.attribute == CardData.Attribute.Rain)
            {
                Heal(5000);
                damage = int.MaxValue;
            }
        }
        return damage;
    }


    public int ComboSkil(int[] nums, EnemyManager enemy)
    {
        int damage = 0;
        CardData status1 = deck[nums[0]].cardData;
        CardData status2 = deck[nums[1]].cardData;

        if (status1.attribute == CardData.Attribute.Thunder)
        {
            if (status2.attribute == CardData.Attribute.Thunder)
            {
                damage = ExtraLargeAttack(status1.atk, status2.atk);
            }
            else if (status2.attribute == CardData.Attribute.Wind)
            {
                damage = LargeAttack(status1.atk, status2.atk);
            }
            else if (status2.attribute == CardData.Attribute.Rain)
            {
                damage = LargeAttack(status1.atk, status2.atk);
            }
        }
        else if (status1.attribute == CardData.Attribute.Rain)
        {
            if (status2.attribute == CardData.Attribute.Thunder)
            {
                damage = LargeAttack(status1.atk, status2.atk);
            }
            else if (status2.attribute == CardData.Attribute.Wind)
            {
                damage = ExtraLargeAttack(status1.atk, status2.atk);
            }
            else if (status2.attribute == CardData.Attribute.Rain)
            {
                damage = LargeAttack(status1.atk, status2.atk); ;
            }
        }
        else if (status1.attribute == CardData.Attribute.Wind)
        {
            if (status2.attribute == CardData.Attribute.Thunder)
            {
               
                damage = Heal(3000) * -1;

            }
            else if (status2.attribute == CardData.Attribute.Wind)
            {
               
                damage = Heal(3000) * -1;
            }
            else if (status2.attribute == CardData.Attribute.Rain)
            {
                //Heal(5000);
                damage = Heal(5000)*-1;
            }
        }
        Status target = enemy.status;
        damage *= (int)Advantage(status1.attribute, target.cardData.attribute);
        return damage;
    }

    private int ExtraLargeAttack(int a,int b)
    {
        return (a + b) * 5;
    }
    private int LargeAttack(int a,int b)
    {
        return (a + b) * 3;
    }
    private int Heal(int value)
    {
        print("ヒール");
        if (maxHP < hp + value)
        {
            value -= (hp + value) - maxHP;
        }
        hp += value;
        return value;
    }
    public void Damage(int value)
    {
        this.hp -= value;
    }

    public bool isAlive()
    {
        if (this.hp <= 0)
        {
            return false;
        }
        return true;
    }
    public float Advantage(CardData.Attribute player, CardData.Attribute enemy)
    {
        if(player == CardData.Attribute.Thunder)
        {
            if(enemy == CardData.Attribute.Rain)
            {
                return 1.2f;
            }else if(enemy == CardData.Attribute.Wind)
            {
                return 0.8f;
            }
        }
        else if (player == CardData.Attribute.Rain)
        {
            if (enemy == CardData.Attribute.Wind)
            {
                return 1.2f;
            }
            else if (enemy == CardData.Attribute.Thunder)
            {
                return 0.8f;
            }
        }
        else if (player == CardData.Attribute.Wind)
        {
            if (enemy == CardData.Attribute.Thunder)
            {
                return 1.2f;
            }
            else if (enemy == CardData.Attribute.Rain)
            {
                return 0.8f;
            }
        }
        return 1;
    }

}
