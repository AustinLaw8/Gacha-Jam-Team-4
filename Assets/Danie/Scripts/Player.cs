using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{

    public Rigidbody2D rb;
    public float jumpAmount = 10;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void OnFly(InputAction action)
    {
        rb.AddForce(Vector2.up * jumpAmount, ForceMode2D.Impulse);
        Debug.Log("Action");

    }
    // Update is called once per frame
    void Update()
    {
        
    }


}
