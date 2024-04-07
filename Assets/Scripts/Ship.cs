using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ship
{
    protected int hp;
    protected int speed;
    protected bool isAlive;

    public int HP
    {
        get { return hp; }
        set { hp = value; }
    }
    public int Speed
    {
        get { return speed; }
        set { speed = value; }
    }
    public bool Alive
    {
        get { return isAlive; }
    }

    public Ship(int setHp, int setSpeed)
    {
        hp = setHp;
        speed = setSpeed;
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
