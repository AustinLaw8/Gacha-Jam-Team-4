using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bird : Enemy
{
    [SerializeField] private int scoreBonus = 500;

    protected override void OnStart()
    {
        SCORE_BONUS = scoreBonus;
    }
    protected override void act(){}
}
