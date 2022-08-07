using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{

    public Rigidbody2D rb;
    public float jumpAmount = 100;
    public int fuel;
    

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        fuel = 100;
    }

    public void OnFly()
    {
        if (fuel >= 2)
        {
            rb.velocity = new Vector3(0, jumpAmount, 0);
            fuel = fuel - 2; //for some reason each time you fly, it activates twice, so double the value
        }
        else
        {
            //what happens when you try to fly with no fuel
        }
    }

    public int getFuel()
    {
        return fuel;
    }

    public void incFuel()
    {
        fuel = fuel + 20;
    }
    // Update is called once per frame
    void Update()
    {
        if(this.transform.position.y < -8.2) //when player falls underneath floor
        {
            Time.timeScale = 0;
            
        }
        
    }


    void OnTriggerEnter2D(Collider2D data)
    {
        if (data.CompareTag("Fuel"))
        {
            incFuel();
            Destroy(data.gameObject);
            
        }
        if (data.CompareTag("Enemy"))
        {
            //change scene to GAME OVER screen 
            Time.timeScale = 0;
            Destroy(this.gameObject); //remove if not needed

        }
    }


}
