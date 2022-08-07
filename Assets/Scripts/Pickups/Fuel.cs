using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fuel : PickupObject
{
    public override void pickupAction(Player player) {
        player.incFuel();
    }
}
