using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boost : PickupObject
{

    public override void pickupAction(Player player) {
        player.boostPlayer();
    }
}
