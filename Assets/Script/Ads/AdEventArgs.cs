using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

public class AdEventArgs : EventArgs
{
    public string PlacementID
    {
        get;
        private set;
    }

    public ShowResult AdShowResult
    {
        get;
        private set;
    }

    public AdEventArgs(string id, ShowResult res)
    {
        PlacementID = id;
        AdShowResult = res;
    }
}
