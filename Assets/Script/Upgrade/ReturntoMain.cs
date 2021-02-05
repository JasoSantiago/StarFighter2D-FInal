using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReturntoMain : MonoBehaviour
{
    [SerializeField] private GameObject mainmaenu;
    [SerializeField] private GameObject upgrade;

    public void OnBUttonClicked()
    {
        upgrade.SetActive(false);
        mainmaenu.SetActive(true);
        GameManager.GetInstance().UnlockRotation();
    }
}
