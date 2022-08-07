using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Vector3 speed;

    void FixedUpdate()
    {
        this.transform.position += speed * Time.deltaTime;
    }

    void OnTriggerEnter2D(Collider2D data)
    {
        if (data.gameObject.tag == "Player")
        {
            Player player = data.gameObject.GetComponent<Player>();
            if (player.isBoosted()) {
                Destroy(this.gameObject);
            } else {
                player.die();
            }
        }
    }
}
