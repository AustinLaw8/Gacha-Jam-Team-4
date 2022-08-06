using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    // [SerializeField] private Player player; //when player class is created
    private static int NUM_PARTS_IN_SECTION = 3;
    private static Vector3 SECTION_START_POS = new Vector3(36.5f,0,0);
    private static Vector3 PART_START_POS = new Vector3(-16.5f,0,0);
    private static Vector3 GAP = new Vector3(5,0,0);
    private static float START_SPEED = 5f;

    [SerializeField] private GameObject SECTION_BLANK;
    [SerializeField] private List<GameObject> LEVEL_PARTS;
    [SerializeField] private TMP_Text scoreText;

    private float currentSpeed;
    private GameObject currentSection;
    private GameObject nextSection;
    private float score;
    private int boosts;

    public void DestroyLastSection(){
        Destroy(currentSection);
        currentSection = nextSection;
        nextSection = null;
    }

    public void GenerateNextSection(){
        nextSection = generateSection();
    }

    void Start()
    {
        // if (player == null) { Debug.Error("Player not set"); Destroy(this) }
        score = 0;
        boosts = 0;
        if (scoreText == null) {
            scoreText = GameObject.Find("Score Text").GetComponent<TMP_Text>();
        }
        currentSection = generateSection();
        currentSpeed = START_SPEED;
    }

    void Update()
    {
        score += Time.deltaTime + (boosts * 500); //500 points for each boost they get
        scoreText.text = "Score: " + (int)score;
        currentSection.transform.position = new Vector3(currentSection.transform.position.x - currentSpeed * Time.deltaTime, 0f, 0f);
        if(nextSection != null) nextSection.transform.position = new Vector3(nextSection.transform.position.x - currentSpeed * Time.deltaTime, 0f, 0f);
    }

    GameObject generateSection()
    {
        GameObject section = Instantiate(SECTION_BLANK, SECTION_START_POS, Quaternion.identity);
        for (int i = 0; i < NUM_PARTS_IN_SECTION; i++)
        {
            Instantiate(LEVEL_PARTS[Random.Range(0, LEVEL_PARTS.Count)],
                    SECTION_START_POS + PART_START_POS + GAP * i,
                    Quaternion.identity,
                    section.transform);
        }
        return section;
    }
}