using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemInterface : MonoBehaviour
{

    interface ItemPickUp<T>
    {
        void doThing();

    }
    

    // Start is called before the first frame update
    void Start()
    {
    

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
      
    }

    private void OnTriggerEnter2D(Collider2D hit)
    {
        
        if (hit.CompareTag("Player")) //when player picks up an item
        {
            Destroy(this.gameObject); //figure out how to make the item itself disappear
            //super boost shit
            Debug.Log("item obtained");
        }


    }
}
