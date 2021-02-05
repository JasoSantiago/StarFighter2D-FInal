using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn
{
    public EnemyType Type { get; private set; }
    public Vector2 StartLoc { get; private set; }
    public Vector2 Destination { get; private set; }

    public EnemySpawn(EnemyType t, Vector2 start, Vector2 destination)
    {
        Type = t;
        StartLoc = start;
        Destination = destination;
    }
}
