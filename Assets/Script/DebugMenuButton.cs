using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugMenuButton : MonoBehaviour
{
    [SerializeField] private GameObject debugPanel;
    // Start is called before the first frame update
    public void onButtonPressed()
    {
        if (debugPanel.activeSelf)
        {
            debugPanel.SetActive(false);
        }
        else
        {
            debugPanel.SetActive(true);
        }
    }
}
