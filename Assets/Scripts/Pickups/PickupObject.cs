using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PickupObject : MonoBehaviour
{
    public abstract void pickupAction(Player player);

    void OnTriggerEnter2D(Collider2D data)
    {
        if (data.gameObject.tag == "Player")
        {
            data.GetComponent<Player>().PlayPickupSound();
            pickupAction(data.GetComponent<Player>());
            Destroy(this.gameObject);
        }

    }
}
