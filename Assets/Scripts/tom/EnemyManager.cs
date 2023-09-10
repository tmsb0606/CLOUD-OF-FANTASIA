using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour, IDamageable, IAliveable
{
    public GameObject Enemy;
    public Status status;
    public int hp;
    public int maxHP;
    // Start is called before the first frame update
    void Start()
    {

        hp = status.cardData.hp;
        maxHP = hp;
    }
    public int NormalAttack()
    {
        return status.cardData.atk;
    }

    public void Damage(int value)
    {
        this.hp -= value;
    }
    public bool isAlive()
    {
        if (hp <= 0)
        {
            return false;
        }
        return true;
    }

}
