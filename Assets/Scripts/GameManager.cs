using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    public static GameManager instance;
    public bool IsDead { set; get; }
    private const int COIN_SCORE_AMOUNT = 1;    
    public bool isGameStarted;
    public Text scoreText;
    public Text coinText;
    public Text modifierText;
    public float score;
    public float coinScore;
    public float modifierScore;
    public float highScore;
    public PlayerController playerController;
    private int lastScore;

    // Death Menu
    public Animator deathMenuAnim;
    public Text deathTitleScoreText;
    public Text deathScoreText;
    public Text deathHighScoreText;
     
    void Awake()
    {
        if (instance != null)
            return;
        instance = this;
        modifierText.text = "x" + modifierScore.ToString("0.0");
        scoreText.text = score.ToString("0");
        coinText.text = coinScore.ToString("0");
        //UpdateScores();
    }
    void Start()
    {
        if (PlayerPrefs.HasKey("highScore"))
        {
            highScore = PlayerPrefs.GetFloat("highScore");
        }
        else
        {
            PlayerPrefs.SetFloat("highScore", 0);
            highScore = 0;
        }


        modifierScore = 1;
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }
    void Update()
    {
        if(Input.GetMouseButtonDown(0) && !isGameStarted)
        {
            isGameStarted = true;
            playerController.StartRunning();
        }
        if(isGameStarted && !IsDead)
        {
            
            score += (Time.deltaTime * modifierScore);
            if(lastScore != (int)score)
            {
                lastScore = (int)score;
                scoreText.text = score.ToString("0");

            }

        }
    }
    //void UpdateScores()
    //{
    //    scoreText.text = score.ToString();
    //    coinText.text = coinScore.ToString();
    //    modifierText.text = "x" + modifierScore.ToString("0.0");
    //}
    public void UpdateModifier(float modifierAmount)
    {
        modifierScore = 1.0f + modifierAmount;
        modifierText.text = "x" + modifierScore.ToString("0.0");
    }
    public void GetCoin()
    {
        coinScore++;
        coinText.text = coinScore.ToString();
        score += COIN_SCORE_AMOUNT;
        scoreText.text = ToPrettyString(int.Parse(score.ToString("0")));
        
    }
    public void OnPlayButton()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }
    public void OnDeath()
    {
        //playerController.isRunning = false;
        IsDead = true;
        if(score >= highScore)
        {
            PlayerPrefs.SetFloat("highScore", score);
            highScore = (int)score;
        }
        deathHighScoreText.text = ToPrettyString(Convert.ToInt32(highScore));
        deathTitleScoreText.text = "SCORE";
        deathScoreText.text = ToPrettyString(int.Parse(scoreText.text));
        deathMenuAnim.SetTrigger("Dead");
    }

    public string ToPrettyString(int number)
    {
        string current = "";
        if (number != 0)
        {
            current = string.Format("{0:0,0}", number);
        }
        else
        {
            current = "0";
        }
        return current.Trim();

    }

}
