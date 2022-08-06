using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy1Spawner : MonoBehaviour
{

    [SerializeField] private GameObject Enemy1;

    private float time = 0;
    [SerializeField] private float spawnTime = 1f;

    [SerializeField] private float maxLeft = -3f;
    [SerializeField] private float maxRight = 3f;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (time > spawnTime) 
        {
            GameObject enemy = Instantiate(Enemy1);
            enemy.transform.position = new Vector2(Random.Range(maxLeft, maxRight), 0); //spawn in the right place
            Destroy(enemy, 10); //destroy barrier after 10 seconds to prevent memory loss
            time = 0;
        }

        time = time + Time.deltaTime;
    }
}
