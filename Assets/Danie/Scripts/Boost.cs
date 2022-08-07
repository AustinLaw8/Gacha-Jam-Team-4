using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boost : PickupObject
{

    public override void pickupAction(Player player) {

        float timer = 10;

        while (timer > 0)
        {
            player.boostPlayer();
            timer = timer - Time.deltaTime;
        }

    }
}
