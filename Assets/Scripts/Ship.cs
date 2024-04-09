using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ship
{
    protected int hp;
    protected bool isAlive;

    public int HP
    {
        get { return hp; }
        set { hp = value; }
    }

    public bool Alive
    {
        get { return isAlive; }
    }

    public Ship(int setHp)
    {
        hp = setHp;

        isAlive = true;
    }

    public void TakeDamage(int damage)
    {
        hp -= damage;
        if (hp < 1)
        {
            isAlive = false;
        }
    }
}
