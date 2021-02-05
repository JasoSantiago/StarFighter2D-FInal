using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class LaserUpgrade : MonoBehaviour
{
    [SerializeField] private Starfighter player;
    [SerializeField] private Text textbox;
    private void Start()
    {
        int lvl = player.gatLaserbulletlvl();
        textbox.text = "Upgrade Laser Rounds" + "\n" + "Cost: " + ((lvl + 1) * 5).ToString();
    }
    public void OnButtonPressed()
    {
        int bounties = GameManager.GetInstance().Bounties;
        int lvl = player.gatLaserbulletlvl();
        if (bounties >= (lvl + 1) * 5 && lvl < 6)
        {
            GameManager.GetInstance().Bounties -= (lvl + 1) * 5;
            lvl++;
            player.setLBUlletlvl(lvl);
            textbox.text = "Upgrade Laser Rounds" + "\n" + "Cost: " + ((lvl + 1) * 5).ToString();
        }
    }
}
