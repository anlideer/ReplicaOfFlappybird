using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


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

    public static bool isAlive = true;
    public static int score;
    public static bool scoreUp = false;

    bool unstopping = false;
    float timeCnt;

	// Use this for initialization
	void Start () {
        score = 0;
        isAlive = true;
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
        else
        {
            if (PlayerPrefs.GetInt("HighestScore", 0) < score)
            {
                PlayerPrefs.SetInt("HighestScore", score);
            }
            HighestScore.text = "Highest Score: " + PlayerPrefs.GetInt("HighestScore");
            CurrentScore.text = "Your Score: " + score;
            menu.SetActive(true);
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
}
