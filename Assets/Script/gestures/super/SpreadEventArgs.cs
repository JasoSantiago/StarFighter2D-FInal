using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpreadEventArgs : EventArgs
{
    public Touch Finger1 { get; private set; }
    public Touch Finger2 { get; private set; }
    public float DistanceDiff { get; private set; }

    public SpreadEventArgs(Touch f1, Touch f2, float dist)
    {
        Finger1 = f1;
        Finger2 = f2;
        DistanceDiff = dist;
    }
}

