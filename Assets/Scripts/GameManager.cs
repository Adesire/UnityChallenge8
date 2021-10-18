using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
    public static bool gameOver;
    public static bool levelCompleted;
    public static bool isGameStared;
    public static bool mute = false;

    public GameObject gameOverPanel;
    public GameObject levelCompletedPanel;
    public GameObject gamePlayPanel;
    public GameObject startMenuPanel;
    public GameObject postProcessing;

    public static int currentLevelIndex;
    public Slider gameProgressSlider;
    public TextMeshProUGUI currentLevelText;
    public TextMeshProUGUI nextLevelText;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI highScoreText;

    public static int numberOfPassedRings;
    public static int score = 0;

    private void Awake()
    {
        currentLevelIndex = PlayerPrefs.GetInt("CurrentLevelIndex", 1);
    }

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1;
        gameOver = levelCompleted = false;
        numberOfPassedRings = 0;
        highScoreText.text = "Best Score:\n" + PlayerPrefs.GetInt("HighScore", 0);
        isGameStared = false;
        AdManager.instance.RequestInterstitial();
        if (postProcessing.GetComponent<Volume>().profile.TryGet<ColorAdjustments>(out var adj))
        {
            adj.hueShift.value = currentLevelIndex + 10;
        }
    }

    // Update is called once per frame
    void Update()
    {
        //Update our UI
        currentLevelText.text = currentLevelIndex.ToString();
        nextLevelText.text = (currentLevelIndex + 1).ToString();
        int progress = numberOfPassedRings * 100 / FindObjectOfType<HelixManager>().numberOfRings;
        gameProgressSlider.value = progress;

        scoreText.text = score.ToString();

        //For PC
        /*if (Input.GetMouseButtonDown(0) && !isGameStared)
        {
            if (EventSystem.current.IsPointerOverGameObject())
                return;

            isGameStared = true;
            gamePlayPanel.SetActive(true);
            startMenuPanel.SetActive(false);
        }*/

        //For Mobile
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began && !isGameStared)
        {
            if (EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId))
                return;

            isGameStared = true;
            gamePlayPanel.SetActive(true);
            startMenuPanel.SetActive(false);
        }

        if (gameOver)
        {
            Time.timeScale = 0;
            gameOverPanel.SetActive(true);

            if (Input.GetButtonDown("Fire1"))
            {
                if (score > PlayerPrefs.GetInt("HighScore"))
                {
                    PlayerPrefs.SetInt("HighScore", score);
                }

                score = 0;
                SceneManager.LoadScene("Level");
            }

            if (Random.Range(0, 3) == 0)
            {
                AdManager.instance.ShowInterstitial();
            }
        }


        if (levelCompleted)
        {
            levelCompletedPanel.SetActive(true);

            if (Input.GetButtonDown("Fire1"))
            {
                PlayerPrefs.SetInt("CurrentLevelIndex", currentLevelIndex + 1);
                SceneManager.LoadScene("Level");
            }
        }
    }
}