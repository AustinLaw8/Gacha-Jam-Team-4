using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpItem : MonoBehaviour
{

    interface ItemPickUp<T>
    {
        void OnTriggerEnter2D(Collider2D hit);

        string item();


    }


}
