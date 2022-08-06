using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FuelBehavior : MonoBehaviour, ItemPickUp
{

  
    public void PickUpAction()
    {
        
            Destroy(this.gameObject); //destroy the item
            //super boost shit
            Debug.Log("item obtained");
        
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

}
