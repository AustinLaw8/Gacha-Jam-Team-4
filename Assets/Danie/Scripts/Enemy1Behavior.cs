using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy1Behavior : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private float dirX;
    private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        rb.velocity = new Vector2(dirX * moveSpeed, rb.velocity.y);
    }

    void OnTriggerEnter2D(Collider2D data)
    {
        if (data.CompareTag("Player"))
        {
            Destroy(data.gameObject);
            Debug.Log("u suck");
        }
    }
}
