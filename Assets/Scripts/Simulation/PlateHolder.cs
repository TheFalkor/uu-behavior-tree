using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateHolder : DataPoint
{
    void Start()
    {
        dataTransformList.Add(transform.Find("DataTransform"));
    }

    public override bool Use(float deltaTime)
    {
        throw new System.NotImplementedException();
    }

}
