using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;


public class GM : MonoBehaviour {

    public Text scoreText;
    public GameObject menu;
    public Text CurrentScore;
    public Text HighestScore;
    public GameObject StopButton;
    public GameObject StartButton;
    public Text fromStop;
    public AudioSource buttonAudio;
    public AudioSource scoreUpAudio;
    public AudioSource cardPicking;
    public Text chancesText;
    public GameObject[] Cards;  // in sequence
    public Text[] CardNum;  // in sequence

    public static bool isAlive = true;
    public static int score;
    public static bool scoreUp = false;
    public static int chance = 0;
    public static bool skillSetted = false;
    public static int[] skills = new int[5];

    bool unstopping = false;
    float timeCnt;
    bool menuShowed = false;
    bool prefsRefreshed = false;
    System.Random rand;

	// Use this for initialization
	void Start () {
        score = 0;
        isAlive = true;
        scoreUp = false;
        unstopping = false;
        chance = 0;
        rand = new System.Random();
        menuShowed = false;
        skillSetted = false;
        prefsRefreshed = false;
    }
	
	// Update is called once per frame
	void Update () {
        if (scoreUp)
        {
            scoreUp = false;
            scoreUpAudio.Play();
        }

        if (isAlive)
        {
            scoreText.text = "Score: " + score;
        }
        else if (!isAlive && !menuShowed)
        {
            menuShowed = true;
            if (PlayerPrefs.GetInt("HighestScore", 0) < score)
            {
                PlayerPrefs.SetInt("HighestScore", score);
            }
            HighestScore.text = "Highest Score: " + PlayerPrefs.GetInt("HighestScore");
            CurrentScore.text = "Your Score: " + score;
            chance = (int)(score / 10);
            chancesText.text = "You have " + chance.ToString() + " to pick up cards: "; 
            menu.SetActive(true);
        }
        
        if (menuShowed)
        {
            chancesText.text = "You have " + chance.ToString() + " to pick up cards: ";
        }

        if (unstopping)
        {
            if (Time.realtimeSinceStartup > timeCnt + 3f)
            {
                unstopping = false;
                fromStop.gameObject.SetActive(false);
                Time.timeScale = 1;
                StopButton.SetActive(true);
                StartButton.SetActive(false);
            }
            else
            {
                fromStop.text = ((int)(timeCnt + 3f - Time.realtimeSinceStartup) + 1).ToString();
            }
        }

        // 抽卡部分
        if (!skillSetted)
        {
            skillSetted = true;
            for (int i = 0; i < 5; i++)
            {
                skills[i] = PlayerPrefs.GetInt("Card" + i.ToString(), 0);
            }
        }
        if (skillSetted && !prefsRefreshed)
        {
            prefsRefreshed = true;
            for (int i = 0; i < 5; i++)
            {
                PlayerPrefs.SetInt("Card" + i.ToString(), 0);
            }
        }
	}

    public void Restart()
    {
        buttonAudio.Play();
        SceneManager.LoadScene(0);
    }

    public void QuitGame()
    {
        buttonAudio.Play();
        Application.Quit();
    }

    public void Stop()
    {
        buttonAudio.Play();
        if (Time.timeScale != 0)
        {
            Time.timeScale = 0;
            StartButton.SetActive(true);
            StopButton.SetActive(false);
        }
        else
        {
            unstopping = true;
            timeCnt = Time.realtimeSinceStartup;
            fromStop.gameObject.SetActive(true);
        }
    }

    // 抽卡部分
    public void pickCards()
    {
        if (chance > 0)
        {
            cardPicking.Play();
            chance -= 1;
            int tmp = rand.Next(0, 5);
            if (PlayerPrefs.GetInt("Card" + tmp.ToString(), 0) == 0)
            {
                PlayerPrefs.SetInt("Card" + tmp.ToString(), 1);
                Cards[tmp].SetActive(true);
            }
            else
            {
                int tmp2 = PlayerPrefs.GetInt("Card" + tmp.ToString());
                PlayerPrefs.SetInt("Card" + tmp.ToString(), tmp2 + 1);
            }
            CardNum[tmp].text = PlayerPrefs.GetInt("Card" + tmp.ToString()).ToString();
        }
    }
}
