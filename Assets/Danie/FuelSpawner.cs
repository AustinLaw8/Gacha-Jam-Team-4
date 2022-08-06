using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FuelSpawner : MonoBehaviour
{

    [SerializeField] public GameObject Item;

    private float time = 0;

    [SerializeField] public float spawnTime = 1f;
    [SerializeField] public float maxLeft = -3f;
    [SerializeField] public float maxRight = 3f;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (time > spawnTime && GameLogic.inprogress) 
        {
            GameObject item1 = Instantiate(Item);
            item1.transform.position = new Vector2(Random.Range(maxLeft, maxRight), 0); //spawn in the right place
            Destroy(item1, 10); //destroy barrier after 10 seconds to prevent memory loss
            time = 0;
        }

        time = time + Time.deltaTime;
    }
}
