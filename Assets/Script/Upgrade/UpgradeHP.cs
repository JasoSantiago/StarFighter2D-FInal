using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UpgradeHP : MonoBehaviour
{
    [SerializeField] private Starfighter player;
    [SerializeField] private Text textbox;
    private void Start()
    {
        int lvl = player.getMaxHpLvl();
        textbox.text = "Upgrade HP" + "\n" + "Cost: " + (lvl* 10).ToString();
    }
    public void OnButtonPressed()
    {
        int bounties = GameManager.GetInstance().Bounties;
        int lvl = player.getMaxHpLvl();
        if (bounties >= lvl * 10 && lvl < 11)
        {
            GameManager.GetInstance().Bounties -= lvl * 10;
            lvl++;
            player.setMaxHp(player.getMaxHp() + (player.getMaxHp()*0.1f));
            player.SetHPLvl(lvl);
            textbox.text = "Upgrade HP" + "\n" + "Cost: " + (lvl * 10).ToString();
        }
    }
}
