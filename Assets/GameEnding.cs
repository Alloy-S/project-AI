using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class GameEnding : MonoBehaviour
{
    public static float playTime = 20;
    public bool timerIsRunning = false;
    public TextMeshProUGUI timeText;
    public Image BlurScreen;

    public Text scoreVal; //to show our value
    // public Text increaseScore
    public Text gameOverText;
    public Text WinningCondition;
    private static int scoreNow;
    // public int scoreValue;

    public bool gameOver;

    public GameObject gameOverWindow;

    public static void setScore(int score){
        scoreNow = score;
    }

    public static void addScore(int score){
        scoreNow += score;
    }

    public static int getScore(){
        return scoreNow;
    }

    private void Start()
    {
        gameOverWindow.SetActive(false);
        // BlurScreen.gameObject.SetActive(false);
        // Starts the timer automatically
        timerIsRunning = true;
        gameOver = false;
    }
    void Update()
    {
        scoreVal.text = scoreNow.ToString("0");
        
        if (timerIsRunning)
        {
            if (playTime > 0)
            {
                playTime -= Time.deltaTime;
                DisplayTime(playTime);
            }
            else
            {
                if (scoreNow >= Flock.total){
                    WinningCondition.text = "YOU WIN!";
                } else {
                    WinningCondition.text = "YOU LOSE:(";
                }
                Debug.Log("Time has run out!");
                playTime = 0;
                timerIsRunning = false;
                // BlurScreen.gameObject.SetActive(true);
                gameOverWindow.SetActive(true);
                Time.timeScale = 0; //pause
                gameOver = true;
            }
       }
    }
    void DisplayTime(float timeToDisplay)
    {
        timeToDisplay += 1;
        float minutes = Mathf.FloorToInt(timeToDisplay / 60); 
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);

        timeText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}