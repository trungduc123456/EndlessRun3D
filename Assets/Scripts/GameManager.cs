using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    public static GameManager instance;
    public bool IsDead { set; get; }
    public bool IsMagnet { set; get; }
    public bool isGameStarted;
    public Text scoreText;
    public Text coinText;
    public Text modifierText;
    public float score;
    public int coinScore;
    public int allCoin;
    public float modifierScore;
    public float highScore;
    
    public PlayerController playerController;
    private int lastScore;
    public GameObject panelScore;
    public GameObject panelStart;
    public GameObject panelShop;
    public GameObject panelItems;
    public GameObject panelDead;

    // Death Menu
    public Animator deathMenuAnim;
    public Text deathTitleScoreText;
    public Text deathScoreText;
    public Text deathHighScoreText;

    public GameObject buttonFly;
    public GameObject buttonMagnet;
    public GameObject imgWaitFly;
    public GameObject imgWaitMagnet;
    public bool checkSkillFly;
    public bool checkSkillMagnet;
    public float timeWaitFly;
    public float timeWaitMagnet;

    public int countItemFly;
    public int countItemMagnet;

    public GameObject txtDiamondPrefabs;
    public List<GameObject> lstTextDiamoundPrefabs;
    public GameObject panelLeaderBoard;
    public InputField newHighScore;
    public GameObject btnSetNewHighSocre;
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
       
        Debug.Log("cc"+countItemFly);
        
        if(buttonFly != null)
        {
            buttonFly.transform.GetChild(0).GetComponent<Text>().text = countItemFly.ToString();
        }
        if(buttonMagnet != null)
        {
            buttonMagnet.transform.GetChild(0).GetComponent<Text>().text = countItemMagnet.ToString();
        }
        timeWaitFly = timeWaitMagnet = GameSettings.TIME_WAIT_SKILL;
        IsMagnet = false;
        highScore = GameSettings.HighScore;
        modifierScore = 1;
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }
    void InitTextDiamoundPrefabs()
    {
        GameObject _txt = (GameObject)Instantiate(txtDiamondPrefabs);
        _txt.transform.SetParent(GameObject.Find("Canvas").transform);
        _txt.SetActive(false);
        lstTextDiamoundPrefabs.Add(_txt);

    }
    public GameObject GetTextDimaoundFromPool()
    {
        //GameObject temp = null;
        for (int i = 0; i < lstTextDiamoundPrefabs.Count; i++)
        {
            if (!lstTextDiamoundPrefabs[i].activeInHierarchy)
            {
                return lstTextDiamoundPrefabs[i];
               
            }
                
            
        }
        return null;
    }
    void Update()
    {
       
        if (isGameStarted && !IsDead)
        {

            score += (Time.deltaTime * modifierScore);
            if (lastScore != (int)score)
            {
                lastScore = (int)score;
                scoreText.text = score.ToString("0");
                
            }
            

        }
        if(IsMagnet)
        {
            Invoke("StopMagnet", 5f);
        }
        SkillFly();
        SkillMagnet();
        UpdateCountSkill();
    }
    void StopMagnet()
    {
        IsMagnet = false;
        Debug.Log("10 " + IsMagnet);
    }
    public void UpdateModifier(float modifierAmount)
    {
        modifierScore = 1.0f + modifierAmount;
        modifierText.text = "x" + modifierScore.ToString("0.0");
    }
    public void GetCoin()
    {
        coinScore += 1;
        allCoin += 1;
        GameSettings.Coin = allCoin;
       
        coinText.text = coinScore.ToString();
        
       
    }
    public void OnTryAgain()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(1);
    }
    public void OnReturnHome()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }
    public void OnDeath()
    {
        coinText.gameObject.SetActive(false);
        panelDead.SetActive(true);
        panelItems.SetActive(false);
        panelScore.SetActive(false);
        IsDead = true;
        FindObjectOfType<MountianSpawner>().IsScrolling = false;
        FindObjectOfType<CameraController>().IsMoving = false;
        if (score >= highScore)
        {
            GameSettings.HighScore = score;
            
            highScore = (int)score;
        }
        deathHighScoreText.text = ToPrettyString(Convert.ToInt32(highScore));
        deathTitleScoreText.text = "SCORE";
        deathScoreText.text = ToPrettyString(int.Parse(scoreText.text));
        //deathMenuAnim.SetTrigger("Dead");
        if (LeaderBoard.instance.CheckScore((int)score))
        {
            newHighScore.gameObject.SetActive(true);
            btnSetNewHighSocre.SetActive(true);
            //SetNewHighScore();
        }
    }
    public void SetNewHighScore()
    {
        string name = newHighScore.text;
       
        LeaderBoard.instance.Record(name, (int)score);
        
        newHighScore.text = "";
        newHighScore.gameObject.SetActive(false);
        btnSetNewHighSocre.SetActive(false);

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
    public void StartGame()
    {
        allCoin = GameSettings.Coin;
        countItemFly = GameSettings.CountItemFly;
        countItemMagnet = GameSettings.CountItemMagnet;
        isGameStarted = true;
        playerController.StartRunning();
        FindObjectOfType<MountianSpawner>().IsScrolling = true;
        FindObjectOfType<CameraController>().IsMoving = true;
        panelStart.SetActive(false);
        panelScore.SetActive(true);
        if(panelItems != null)
        {
            panelItems.SetActive(true);
        }
        

    }
    public void OpenShop()
    {
        panelShop.SetActive(true);
        panelStart.SetActive(false);
    }
    public void CloseShop()
    {
        panelShop.SetActive(false);
        panelStart.SetActive(true);
    }
    public void btnFly()
    {
        Debug.Log(countItemFly);
        if(countItemFly > 0)
        {
            playerController.isFlying = true;
            countItemFly -= 1;
            GameSettings.CountItemFly = countItemFly;
            imgWaitFly.SetActive(true);
            checkSkillFly = true;
        }
        
    }
    public void btnMagnet()
    {
        Debug.Log(countItemMagnet);
        if(countItemMagnet > 0)
        {
            IsMagnet = true;
            countItemMagnet -= 1;
            GameSettings.CountItemMagnet = countItemMagnet;
            imgWaitMagnet.SetActive(true);
            checkSkillMagnet = true;
        }
        
    }
    void SkillFly()
    {
        if (checkSkillFly)
        {
            if (imgWaitFly.GetComponent<Image>().fillAmount > 0)
            {
                imgWaitFly.GetComponent<Image>().fillAmount -= ((1f / GameSettings.TIME_WAIT_SKILL) * Time.deltaTime);
                if (timeWaitFly > 0)
                {
                    timeWaitFly -= Time.deltaTime;
                    imgWaitFly.transform.GetChild(0).GetComponent<Text>().text = timeWaitFly.ToString("0.0");
                }

            }
            else
            {
                timeWaitFly = GameSettings.TIME_WAIT_SKILL;
                checkSkillFly = false;
                imgWaitFly.GetComponent<Image>().fillAmount = 1f;
                imgWaitFly.gameObject.SetActive(false);
            }
        }
    }
    void SkillMagnet()
    {
        if (checkSkillMagnet)
        {
            if (imgWaitMagnet.GetComponent<Image>().fillAmount > 0)
            {
                imgWaitMagnet.GetComponent<Image>().fillAmount -= ((1f / GameSettings.TIME_WAIT_SKILL) * Time.deltaTime);
                if (timeWaitMagnet > 0)
                {
                    timeWaitMagnet -= Time.deltaTime;
                    imgWaitMagnet.transform.GetChild(0).GetComponent<Text>().text = timeWaitMagnet.ToString("0.0");
                }
            }
            else
            {
                timeWaitMagnet = GameSettings.TIME_WAIT_SKILL;
                checkSkillMagnet = false;
                imgWaitMagnet.GetComponent<Image>().fillAmount = 1f;
                imgWaitMagnet.gameObject.SetActive(false);
            }
        }
    }
    void UpdateCountSkill()
    {
        if (buttonFly != null)
        {
            buttonFly.transform.GetChild(0).GetComponent<Text>().text = GameSettings.CountItemFly.ToString();
        }
        if (buttonMagnet != null)
        {
            buttonMagnet.transform.GetChild(0).GetComponent<Text>().text = GameSettings.CountItemMagnet.ToString();
        }
    }
    public void OpenLeaderBoard()
    {
        panelLeaderBoard.SetActive(true);
        
    }
}
