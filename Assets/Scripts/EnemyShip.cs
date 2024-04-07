using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShip : Ship
{
    public EnemyShip(int hp, int speed) : base(hp, speed)
    {
        hp = 1;
        speed = 6;
    }
}
