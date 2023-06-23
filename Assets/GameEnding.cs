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

    public int growthRate;

    public Text scoreVal; //to show our value
    // public Text increaseScore
    public Text gameOverText;
    public Text WinningCondition;
    // private static int scoreNow;
    public static int targetScore;

    public static int endScores;

    public int check;
    bool gameOver;

    public GameObject gameOverWindow;

    public GameObject cagePlayer1;
    public GameObject cagePlayer2;

    

    public static void setScore(int score){
        targetScore = score;
    }

    public static void addScore(int score){
        targetScore += score;
    }

    public static int getScore(){
        return targetScore;
    }

    private void Start()
    {
        playTime = 8;
        gameOverWindow.SetActive(false);
        // BlurScreen.gameObject.SetActive(false);
        // Starts the timer automatically
        timerIsRunning = true;
        gameOver = false;
    }
    void Update()
    {  
         Debug.Log("adananfajnda");     
        if (timerIsRunning)
        {
            if (playTime > 0)
            {
                playTime -= Time.deltaTime;
                DisplayTime(playTime);
            }
            else
            {
                Debug.Log("end");
                gameOver = true;
                if (cagePlayer1.GetComponent<CountScore>().getScore() < cagePlayer2.GetComponent<CountScore>().getScore()) {
                    WinningCondition.text = "Player 2 WIN!!!";
                    check = 2;
                    if(gameOver == true){
                        scoreVal.text = showScore(endScores, cagePlayer2.GetComponent<CountScore>().getScore()).ToString();
                    }
                } else if (cagePlayer1.GetComponent<CountScore>().getScore() > cagePlayer2.GetComponent<CountScore>().getScore()) {
                    WinningCondition.text = "Player 1 WIN!!!";
                    check = 1;
                } else {
                    WinningCondition.text = "DRAW";
                }
                // scoreVal.text = endScores.ToString();

                playTime = 0;
                timerIsRunning = false;
                // BlurScreen.gameObject.SetActive(true);
                gameOverWindow.SetActive(true);
                Time.timeScale = 0; //pause

                if(gameOver == true){
                    if(check == 1){
                        scoreVal.text = showScore(endScores, cagePlayer1.GetComponent<CountScore>().getScore()).ToString();
                    }else if(check == 2){
                        scoreVal.text = showScore(endScores, cagePlayer2.GetComponent<CountScore>().getScore()).ToString();
                    }
                }
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

    int showScore(int endScores, int targetScore){
         if(endScores != targetScore && targetScore > endScores){
            endScores += growthRate;
        }
        Debug.Log("endScores");
        return endScores;
    }
}