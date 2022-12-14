using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    private static Color FUEL_FULL = new Color(252f/255f, 77f/255f, 64f/255f);
    private static Color FUEL_LOW = new Color(239f/255f, 167f/255f, 54f/255f);
    private static float BOOSTED_MULTIPLIER = 2f;
    private static float SPEED_INCREASE_RATE = .2f;
    private static float CEILING_HEIGHT = 9f;
    private static float OFFSCREEN_LIM = 9.5f;
    private static Vector3 SECTION_LOAD_POS = new Vector3(36.5f,0f,0f);
    private static float START_SPEED = 5f;

    [SerializeField] private Player player;
    [SerializeField] private Camera mainCamera;
    [SerializeField] private GameObject SECTION_BLANK;
    [SerializeField] private List<GameObject> LEVEL_PARTS;
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject deathScreen;
    [SerializeField] private GameObject playCanvas;
    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private GameObject fuelSlider;
    [SerializeField] private AudioSource music;

    public float currentSpeed;
    private GameObject currentSection;
    private GameObject nextSection;
    private Queue<GameObject> levelParts = new Queue<GameObject>();
    public float score;
  
    public void DestroyLastSection()
    {
        Destroy(currentSection);
        currentSection = nextSection;
        nextSection = null;
    }

    public void GenerateNextSection()
    {
        nextSection = Instantiate(SECTION_BLANK, SECTION_LOAD_POS, Quaternion.identity);
    }

    public void GenerateNextPart()
    {
        GameObject nextPart = LEVEL_PARTS[Random.Range(0, LEVEL_PARTS.Count)];
        Vector3 spawnLoc = new Vector3(
                OFFSCREEN_LIM - nextPart.transform.Find("StartPosition").transform.position.x,
                0f,
                0f);
        levelParts.Enqueue(Instantiate(
                nextPart,
                spawnLoc,
                Quaternion.identity));
    }

    public void ClearParts()
    {
        levelParts.Dequeue();
    }

    void Start()
    {
        Time.timeScale = 1f;
        OFFSCREEN_LIM = GameObject.Find("ScreenEndTrigger").transform.position.x;
        if (mainCamera == null) mainCamera = GameObject.Find("Main Camera").GetComponent<Camera>();
        if (player == null) player = GameObject.Find("Player").GetComponent<Player>();
        if (scoreText == null) scoreText = GameObject.Find("Score Text").GetComponent<TMP_Text>();
        if (fuelSlider == null) fuelSlider = GameObject.Find("Fuel Slider");
        if (music == null) music = GameObject.Find("Music").GetComponent<AudioSource>();
        // music.gameObject.GetComponent<MusicController>().SetVolume();
        score = 0;
        currentSection = Instantiate(SECTION_BLANK, Vector3.zero, Quaternion.identity);
        currentSpeed = START_SPEED;
        GenerateNextPart();
    }

    void Update()
    {
        score += currentSpeed * Time.timeScale;
        if (player.transform.position.y > 0 && player.transform.position.y < CEILING_HEIGHT)
        {
            mainCamera.transform.position = new Vector3(
                    mainCamera.transform.position.x,
                    player.transform.position.y,
                    mainCamera.transform.position.z);
        }
        scoreText.text = "Score: " + (int)score;
        updateFuelSlider();
    }

    void FixedUpdate()
    {
        Vector3 speedVec = new Vector3(currentSpeed,0f,0f) * Time.deltaTime;
        speedVec = player.isBoosted() ? speedVec * BOOSTED_MULTIPLIER : speedVec;
        currentSection.transform.position -= speedVec;
        if(nextSection != null) nextSection.transform.position -= speedVec;
        foreach (GameObject obj in levelParts)
            obj.transform.position -= speedVec;
        currentSpeed += Time.deltaTime * SPEED_INCREASE_RATE;
    }

   
    void updateFuelSlider()
    {
        Color color;
        if (player.getFuel() > 50) {
            color = FUEL_FULL;
        } else {
            color = FUEL_LOW;
        }
        if (player.getFuel() > 0) {
            Image img = fuelSlider.transform.Find("Fill Area").Find("Fill").GetComponent<Image>();
            img.enabled = true;
            img.color = color;
            fuelSlider.GetComponent<Slider>().value = player.getFuel() / 100;
        } else {
            fuelSlider.transform.Find("Fill Area").Find("Fill").GetComponent<Image>().enabled = false;
        }

    }

    public void OnPause()
    {
        Time.timeScale = 0;
        if (!pauseMenu.activeSelf) {
            pauseMenu.SetActive(true);
            GameObject audio = GameObject.Find("Audio Settings Menu");
            GameObject inst = GameObject.Find("Instructions Canvas");
            if (audio) audio.SetActive(false);
            if (inst) inst.SetActive(false);
        }
    }

    public void OnPlay()
    {
        Time.timeScale = 1f;
        if (pauseMenu.activeSelf) {
            pauseMenu.SetActive(false);
        }
    }

    public void OnPlayerDie()
    {
        Time.timeScale = 0f;
        playCanvas.SetActive(false);
        deathScreen.SetActive(true);
        deathScreen.transform.Find("Score Text").gameObject.GetComponent<TMP_Text>().text = ((int)score).ToString();
    }

    public void ResetGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1f;
    }

    public void OnVolumeChange()
    {
        float val = GameObject.Find("Volume Slider").GetComponent<Slider>().value;
        music.volume = val;
        foreach (var source in player.GetComponents<AudioSource>())
            source.volume = val;
    }
}