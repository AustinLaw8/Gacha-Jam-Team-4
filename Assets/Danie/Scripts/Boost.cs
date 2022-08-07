using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boost : PickupObject
{
    public float time = 10;

    public override void pickupAction(Player player) {
        
        if(time > 0)
        {

            time = time - Time.deltaTime;
        }

    }
}
