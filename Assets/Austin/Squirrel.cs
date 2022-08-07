using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Squirrel : Enemy
{
    private static float TRAVEL_TIME = 3f;
    [SerializeField] private int scoreBonus = 750;
    [SerializeField] private GameObject BULLET;
    [SerializeField] private GameObject player;

    private static float SHOOT_COOLDOWN = 2f;
    private float timer = 0f;
    
    protected override void OnStart()
    {
        SCORE_BONUS = scoreBonus;
    }

    protected override void act()
    {
        timer += Time.deltaTime;
        if (timer > SHOOT_COOLDOWN)
        {
            shoot();
            timer = 0f;
        }
    }

    private void shoot()
    {
        GameObject bullet = Instantiate(BULLET, this.transform.position, Quaternion.identity); 
        bullet.GetComponent<Bullet>().speed = (player.transform.position - this.transform.position) / TRAVEL_TIME;
        Destroy(bullet, TRAVEL_TIME + 2f);
    }
}
