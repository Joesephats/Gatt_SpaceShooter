using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ship
{
    int hp;
    int speed;

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

    public Ship(int setHp, int setSpeed)
    {
        hp = setHp;
        speed = setSpeed;
    }
}
