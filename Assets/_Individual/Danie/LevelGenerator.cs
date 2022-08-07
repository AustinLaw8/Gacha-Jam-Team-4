using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    [SerializeField] private Transform levelpartstart;
    [SerializeField] private List<Transform> levelpartlist; //can add multiple level parts here
   // [SerializeField] private Player player; //when player class is created

    private const float PLAYER_DISTANCE = 10f;

    private Vector3 lastEndPosition;

    private void Awake()
    {
       
        lastEndPosition = levelpartstart.Find("EndPosition").position; //set initial endposition of starting level
        int startingLevelParts = 3; //how many parts to spawn initially
        
        for (int x = 0; x < startingLevelParts; x++)
        {
            SpawnLevelPart();
        }
       
    }

    private void Update()
    {
        //if(Vector3.Distance(player.GetPosition(), lastEndPosition) < PLAYER_DISTANCE) for when player is introduced
       // {
            SpawnLevelPart();
      //  }
    }

    private void SpawnLevelPart()
    {
        Transform randomLevelPart = levelpartlist[Random.Range(0, levelpartlist.Count)]; //randomly choose level part
        Transform lastLevelPartTransform = SpawnLevelPart(randomLevelPart, lastEndPosition); //spawn level part at last end position
        lastEndPosition = lastLevelPartTransform.Find("EndPosition").position; //find new end position
    }
    private Transform SpawnLevelPart(Transform levelPart, Vector3 spawnPosition)
    {
        Transform levelpartTransform = Instantiate(levelPart, spawnPosition, Quaternion.identity);  //create levelpart1
        return levelpartTransform; //spawn levelpart1 

    }
}
