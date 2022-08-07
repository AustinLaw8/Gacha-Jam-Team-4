using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    private static float X_POSITION = -6.5f;
    private static float RESTORATION_TIME = .33f;
    private static float MAX_TIME_OFF_SCREEN = 2f;
    [SerializeField] private GameManager gameManager;
    [SerializeField] private float jumpAmount = 100f;
    [SerializeField] private float fuel = 100f;
    [SerializeField] private bool boosted { get; set; }
    [SerializeField] private bool flying { get; set; }

    private float timeOffScreen;
    private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        boosted = false;
        flying = false;
        rb = GetComponent<Rigidbody2D>();
        if (gameManager == null) gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
    }

    void FixedUpdate()
    {
        if (flying && fuel > 0)
        {
            rb.velocity = new Vector3(rb.velocity.x, jumpAmount, 0);
            fuel -= 10 * Time.deltaTime;
        }
        if (!Mathf.Approximately(transform.position.x, X_POSITION))
        {
            attemptToCatchUp();
        }
        else 
        {
            timeOffScreen = 0f;
            rb.velocity = new Vector3(0,rb.velocity.y,0);
        }
    }

    void OnTriggerStay2D(Collider2D hit)
    {
        if(hit.gameObject.tag == "Ground")
        {
            Debug.Log("bump");
            this.transform.position -= new Vector3(0,.1f,0);
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
        fuel += 20;
    }

    public void incScore(int amount)
    {
        gameManager.score += amount;
    }

    public void die()
    {
        gameManager.OnPlayerDie();
    }

    public bool isBoosted()
    {
        return boosted;
    }

    public void boostPlayer()
    {
        gameManager.boostEffect();
    }

    private void attemptToCatchUp()
    {
        timeOffScreen += Time.deltaTime;
        rb.velocity = new Vector3((X_POSITION - transform.position.x)/RESTORATION_TIME,rb.velocity.y,0);
        if (timeOffScreen > MAX_TIME_OFF_SCREEN)
        {
            die();
        }
    }
}
