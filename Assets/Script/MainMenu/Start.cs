using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Start : MonoBehaviour
{
    [SerializeField] private GameObject mainmenu;

    [SerializeField] private GameObject gamepanel;

    [SerializeField] private Starfighter player;

    [SerializeField] private int TestStageNum = 1;
    public void OnButtonPressed()
    {
        mainmenu.SetActive(false);
        gamepanel.SetActive(true);
        player.reset();
        EnemyManager.GetInstance().Reset();
        EnemyManager.GetInstance().SetStage(TestStageNum);
        MusicManager.GetInstance().PlayStage();
        GameManager.GetInstance().score = 0;
        GameManager.GetInstance().stageNum = TestStageNum;
        //GameManager.GetInstance().LockRotation();
    }
}
