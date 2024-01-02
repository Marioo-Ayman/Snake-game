using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;



public class ScoreManager : MonoBehaviour
{

    public Text scoreText;
    public Text TargetText;
    public static int score;
    public int target = 2;
    public static bool allowInputs;
    public Slider countdownBar;
    private bool countDown = true;
    public float countDownTime = 8;
    public float refillTime = 10;
    private bool isGameOver;
    public TextMeshProUGUI lose;
    public TextMeshProUGUI win;
    public GameObject playAgainButton;
    void Start()
    {
        score = 0;
        Time.timeScale = 1f;
        lose.enabled = false;
        win.enabled = false;
        playAgainButton.SetActive(false);
        countdownBar.maxValue = refillTime;
        TargetText.text = "Target: " + target; 
    }

    void Update()
    {
        scoreText.text = "Score: " + Mathf.Round(score);

        if (countdownBar.maxValue != refillTime)
            countdownBar.maxValue = refillTime;

        if (countDown) 
            countdownBar.value -= Time.deltaTime / countDownTime * refillTime;
        else
            countdownBar.value += Time.deltaTime;

        
        if (countdownBar.value <= 0)
        {
            countDown = false;
            allowInputs = false;
        }
        if (countdownBar.value >= refillTime && ScoreManager.score < target)
        {
            
            Debug.Log("Game Over: You didn't reach the target score!");
            Time.timeScale = 0f;
            lose.enabled = true;
            playAgainButton.SetActive(true);
        }

        if (countdownBar.value >= refillTime && ScoreManager.score >= target)
        {

            Debug.Log("Well Played You win");
            Time.timeScale = 0f;
            win.enabled = true;
            playAgainButton.SetActive(true);
        }


    }
    public void PlayAgain()
    {
       
        SceneManager.LoadScene("Main");
    }

}


