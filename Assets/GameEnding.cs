using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
public class GameEnding : MonoBehaviour
{
    public static float playTime;
     bool timerIsRunning = false;
    public TextMeshProUGUI timeText;
    public Image BlurScreen;

    public Text scoreVal; //to show our value
    // public Text increaseScore
    public Text gameOverText;
    public Text WinningCondition;
    private static int scoreNow;
    // public int scoreValue;

     bool gameOver;

    public GameObject gameOverWindow;

    public GameObject cagePlayer1;
    public GameObject cagePlayer2;

    

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

        playTime = 60;
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
                if (cagePlayer1.GetComponent<CountScore>().getScore() < cagePlayer2.GetComponent<CountScore>().getScore()) {
                    WinningCondition.text = "Player 2 WIN!!!";
                } else if (cagePlayer1.GetComponent<CountScore>().getScore() > cagePlayer2.GetComponent<CountScore>().getScore()) {
                    WinningCondition.text = "Player 1 WIN!!!";
                } else {
                    WinningCondition.text = "DRAW";
                }

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

    public void reloadScene()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("MainScene");
    }
}