using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameLogic : MonoBehaviour
{



    public static bool inprogress;

    public GameObject Enemy1;
    private float score;
    public Text scoreText;
    private int boosts;



    void Start()
    {

        inprogress = true;
        score = 0;
        this.enabled = true;
        boosts = 0;
    }

    void Update()
    {

        score = score + Time.deltaTime + (boosts*500); //500 points for each boost they get
        scoreText.text = "Score: " + (int)score;

        if (!inprogress)
        {
          
            this.enabled = false;

        }


    }
}
