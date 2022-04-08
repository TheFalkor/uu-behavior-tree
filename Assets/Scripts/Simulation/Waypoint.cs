using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoint : DataPoint
{
    void Start()
    {
        dataTransformList.Add(transform);
    }

    public override bool Use(float deltaTime)
    {
        return false;
    }
}
