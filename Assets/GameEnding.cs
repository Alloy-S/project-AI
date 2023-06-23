using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
public class GameEnding : MonoBehaviour
{
    public static float playTime;
    public TextMeshProUGUI timeText;

    public int growthRate;

    public Text scoreVal; //to show our value
    public Text WinningCondition;
    // private static int scoreNow;
    public int targetScore;

    public int endScores;
    bool timeUp;

    public GameObject gameOverWindow;

    public GameObject cagePlayer1;
    public GameObject cagePlayer2;

    private void Start()
    {
        playTime = 60;
        gameOverWindow.SetActive(false);
        // BlurScreen.gameObject.SetActive(false);
        // Starts the timer automatically
        timeUp = false;
        Time.timeScale = 1;
        endScores = 0;
        targetScore = 0;
    }
    void Update()
    {  
        
            if (playTime > 0)
            {
                playTime -= Time.deltaTime;
                DisplayTime(playTime);
            }
            else
            {
                timeUp = true;
                if (cagePlayer1.GetComponent<CountScore>().getScore() < cagePlayer2.GetComponent<CountScore>().getScore()) {
                    WinningCondition.text = "Player 2 WIN!!!";
                    scoreVal.text = showScore(cagePlayer2.GetComponent<CountScore>().getScore()).ToString();
                } else if (cagePlayer1.GetComponent<CountScore>().getScore() > cagePlayer2.GetComponent<CountScore>().getScore()) {
                    WinningCondition.text = "Player 1 WIN!!!";
                    scoreVal.text = showScore(cagePlayer1.GetComponent<CountScore>().getScore()).ToString();
                } else {
                    WinningCondition.text = "DRAW";
                    scoreVal.text = showScore(cagePlayer1.GetComponent<CountScore>().getScore()).ToString();
                }
                // BlurScreen.gameObject.SetActive(true);
                gameOverWindow.SetActive(true);
                Time.timeScale = 0; //pause
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
        targetScore = 0;
        SceneManager.LoadScene("MainScene");
    }

    int showScore(int targetScore)
    {
       if (this.endScores < targetScore) {
            endScores += growthRate;
       }
       return endScores;
    }
}