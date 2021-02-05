using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum Directions
{
   LEFT, RIGHT
}

public class SwipeEventArgs : EventArgs
{
    private Vector2 swipePosition;
    private Vector2 RawDirection;
    private Directions direction;
    public SwipeEventArgs(Vector2 pos, Vector2 rawDir, Directions dir)
    {
        swipePosition = pos;
        RawDirection = rawDir;
        direction = dir;
    }

    public Vector2 SwipePos
    {
        get
        {
            return swipePosition;
        }
    }

    public Vector2 RawDir
    {
        get
        {
            return RawDirection;
        }
    }

    public Directions directions
    {
        get
        {
            return direction;
        }
    }
}
