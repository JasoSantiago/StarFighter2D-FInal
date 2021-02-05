using System.Collections;
using System.Collections.Generic;
using System.Net.Mime;
using UnityEngine;
using UnityEngine.UI;

public class BountyDisplay : MonoBehaviour
{
    [SerializeField] private Text textbox;

    void Update()
    {
        int bounties = GameManager.GetInstance().Bounties;
        textbox.text = "Bounty: " + bounties.ToString();
    }
}
