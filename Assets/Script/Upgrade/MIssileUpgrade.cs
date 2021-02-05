using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class MIssileUpgrade : MonoBehaviour
{
    [SerializeField] private Starfighter player;
    [SerializeField] private Text textbox;
    private void Start()
    {
        int lvl = player.getmissilebulletlvl();
        textbox.text = "Upgrade Missiles" + "\n" + "Cost: " + ((lvl + 1) * 5).ToString();
    }
    public void OnButtonPressed()
    {
        int bounties = GameManager.GetInstance().Bounties;
        int lvl = player.getmissilebulletlvl();
        if (bounties >= (lvl + 1) * 5 && lvl < 6)
        {
            GameManager.GetInstance().Bounties -= (lvl + 1) * 5;
            lvl++;
            player.setMBUlletlvl(lvl);
            textbox.text = "Upgrade Missiles" + "\n" + "Cost: " + ((lvl + 1) * 5).ToString();
        }
    }
}
