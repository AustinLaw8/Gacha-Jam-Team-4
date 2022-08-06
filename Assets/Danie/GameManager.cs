using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Text scoreText;
    private float score;
    private int boosts;

    void Start()
    {
        score = 0;
        boosts = 0;
        if (scoreText == null) {
            scoreText = GameObject.Find("Score").GetComponent<Text>();
        }
    }

    void Update()
    {
        score += Time.deltaTime + (boosts * 500); //500 points for each boost they get
        scoreText.text = "Score: " + (int)score;
    }
}