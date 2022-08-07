using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [SerializeField] private float jumpAmount = 100f;
    [SerializeField] private float fuel = 100f;
    [SerializeField] private bool boosted = false;
    [SerializeField] private bool flying = false;

    private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        if (flying)
        {
            rb.velocity = new Vector3(0, jumpAmount, 0);
            fuel -= 10 * Time.deltaTime;
        }
    }

    public void OnFly(InputValue value)
    {
        flying = value.Get<float>() > 0;
    }

    public float getFuel()
    {
        return fuel;
    }

    public void incFuel()
    {
        fuel = fuel + 20;
    }

    public void die()
    {
        if(this.transform.position.y < -8.2) //when player falls underneath floor
        {
            Time.timeScale = 0;
            
        }
        
    }
}
