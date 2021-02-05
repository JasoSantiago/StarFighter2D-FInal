using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Upgrade : MonoBehaviour
{
    [SerializeField] private GameObject mainmenu;
    [SerializeField] private GameObject upgrademenu;
    public void OnBUttonPressed()
    {
        mainmenu.SetActive(false);
        upgrademenu.SetActive(true);
    }
}
