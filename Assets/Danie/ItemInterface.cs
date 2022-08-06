using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpObject : MonoBehaviour
{

    interface ItemPickUp<T>
    {
        void OnTriggerEnter2D(Collider2D hit); //collision
        

    }


}
