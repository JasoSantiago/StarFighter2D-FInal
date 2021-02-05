using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private static GameManager instance = null;

    [SerializeField] private GameObject middlePanel;
    [SerializeField] private GameObject gameover;
    [SerializeField] private GameObject victory;
    [SerializeField] private GameObject nextStageObject;
    [SerializeField] private GameObject GamePanel;
    [SerializeField] private GameObject StageSelectPanel;
    [SerializeField] private Toggle toggle;
    [SerializeField] private Starfighter player;
    [SerializeField] private Text scoretext;
    [SerializeField] private Text lvlText;
    

    public int stageNum = 1;
    public int Bounties = 0;
    public int score = 0;


    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        EnemyManager.GetInstance().SetStage(stageNum);
        toggle.onValueChanged.AddListener(delegate
        {
            toggleValueChanged(toggle);
        });
        
        AdManager.Instance.OnAdDone += OnAdDone;

    }

    private void OnAdDone(object sender, AdEventArgs e)
    {
        if(e.AdShowResult == ShowResult.Finished && e.PlacementID == AdManager.REWARD_AD)
        {
            GamePanel.SetActive(true);
            gameover.SetActive(false);
            player.FullHP();
        }
    }


    public static GameManager GetInstance()
    {
        if (instance != null)
        {
            return instance;
        }
        else
        {
            instance = new GameManager();
            return instance;
        }
    }

    public GameObject GetMiddlePanel()
    {
        return middlePanel;
    }
    public GameObject GetGamePanel()
    {
        return GamePanel;
    }

    public void Gameover()
    {
        UnlockRotation();
        GamePanel.SetActive(false);
        gameover.SetActive(true);
        scoretext.text = "Your Score is " + score.ToString();
        lvlText.text = "You ended on stage " + stageNum.ToString();
        //MusicManager.GetInstance().PlayMenu();
        //EnemyManager.GetInstance().Reset();
    }
    public void Victory()
    {
        UnlockRotation();
        GamePanel.SetActive(false);
        victory.SetActive(true);
        scoretext.text = "Your Score is " + score.ToString();
        lvlText.text = "You ended on stage " + stageNum.ToString();
        MusicManager.GetInstance().PlayMenu();
        EnemyManager.GetInstance().Reset();

        if (stageNum == 3)
        {
            nextStageObject.SetActive(false);
        }
    }

    public void LockRotation()
    {
        switch (Input.deviceOrientation)
        {
            case DeviceOrientation.LandscapeLeft:
            case DeviceOrientation.LandscapeRight:
                Screen.orientation = ScreenOrientation.Landscape;
                break;

            case DeviceOrientation.Portrait:
            case DeviceOrientation.PortraitUpsideDown:
                Screen.orientation = ScreenOrientation.Portrait;
                break;
        }
    }

    public void UnlockRotation()
    {
        Screen.orientation = ScreenOrientation.AutoRotation;
    }

    private void toggleValueChanged(Toggle change)
    {
        Debug.Log(toggle.isOn);
        if (toggle.isOn)
        {
            player.setinvincible(true);
        }
        else
        {
            player.setinvincible(false);
        }
    }

    public void selectStage1()
    {
        stageNum = 1;
        PrepareStart();
    }

    public void selectStage2()
    {
        stageNum = 2;
        PrepareStart();
    }

    public void selectStage3()
    {
        stageNum = 3;
        PrepareStart();
    }

    public void PrepareStart()
    {
        player.reset();
        EnemyManager.GetInstance().Reset();
        MusicManager.GetInstance().PlayStage();
        score = 0;
        StageSelectPanel.SetActive(false);
        GamePanel.SetActive(true);
        LockRotation();
        EnemyManager.GetInstance().SetStage(stageNum);
    }

    public void NextStage()
    {
        if (stageNum < 3)
        {
            LockRotation();
            EnemyManager.GetInstance().Reset();
            stageNum++;
            EnemyManager.GetInstance().SetStage(stageNum);
            GamePanel.SetActive(true);
            victory.SetActive(false);
            MusicManager.GetInstance().PlayStage();
        }
    }
}
