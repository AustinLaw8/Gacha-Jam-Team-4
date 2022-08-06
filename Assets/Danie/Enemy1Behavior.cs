using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy1Behavior : MonoBehaviour
{

    private float moveSpeed;
    private float dirX;
    private Rigidbody2D rb;
    private Vector3 localScale;
    

    // Start is called before the first frame update
    void Start()
    {
        localScale = transform.localScale; //moves enemy
        rb = GetComponent<Rigidbody2D>();
        dirX = -1f;
        moveSpeed = 2f;

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        rb.velocity = new Vector2(dirX * moveSpeed, rb.velocity.y);
    }

    private void OnTriggerEnter2D(Collider2D data)
    {
        
        if (data.CompareTag("Player"))
        {
            Destroy(data.gameObject);
            GameLogic.inprogress = false;
            Debug.Log("u suck");
        }


    }
}
