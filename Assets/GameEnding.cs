using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class GameEnding : MonoBehaviour
{
    public float timeRemaining = 60;
    public bool timerIsRunning = false;
    public TextMeshProUGUI timeText;
    public Image BlurScreen;

    public Text scoreVal; //to show our value
    // public Text increaseScore
    public Text gameOverText;
    public int targetScore;
    public int endScores;
    // public int scoreValue;
    public int growthRate;

    public bool gameOver;

    private void Start()
    {
        BlurScreen.gameObject.SetActive(false);
        // Starts the timer automatically
        timerIsRunning = true;
        gameOver = false;
    }
    void Update()
    {
        scoreVal.text = endScores.ToString("0");
        
        if (timerIsRunning)
        {
            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
                DisplayTime(timeRemaining);
            }
            else
            {
                Debug.Log("Time has run out!");
                timeRemaining = 0;
                timerIsRunning = false;
                BlurScreen.gameObject.SetActive(true);
                Time.timeScale = 0; //pause
                gameOver = true;
            }
       }
       if(gameOver == true)
        {
            ShowScore();
        }
    }
    void DisplayTime(float timeToDisplay)
    {
        timeToDisplay += 1;
        float minutes = Mathf.FloorToInt(timeToDisplay / 60); 
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);

        timeText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    void ShowScore(){
    
        if(endScores != targetScore && targetScore > endScores){
            endScores += growthRate;
        }
    }
}