using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ant : Enemy
{
    [SerializeField] private int scoreBonus = 500;

    protected override void OnStart()
    {
        SCORE_BONUS = scoreBonus;
    }
    protected override void act(){}
}
