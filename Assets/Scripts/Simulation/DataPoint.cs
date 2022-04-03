using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum DataType
{
    NONE,
    ORDER,
    FOOD,
    DISH
}

public enum Data
{
    NONE,
    ORDER_SANDWICH,
    ORDER_NOODLES,
    ORDER_WATER,
    FOOD_SANDWICH,
    FOOD_NOODLES,
    FOOD_WATER,
    PLATE_DIRTY,
    PLATE_CLEAN
}

public abstract class DataPoint : MonoBehaviour
{
    public DataType dataType;
    public Data data;
    public Transform dataTransform;

    public void Pickup(Entity entity)
    {
        entity.currentlyHoldingData = data;
        entity.currentlyHoldingType = dataType;

        data = Data.NONE;
        dataType = DataType.NONE;
        // Update visual
    }

    public void PutDown(Entity entity)
    {
        data = entity.currentlyHoldingData;
        dataType = entity.currentlyHoldingType;

        entity.currentlyHoldingData = Data.NONE;
        entity.currentlyHoldingType = DataType.NONE;

        // Update visual
    }

    public abstract bool Use();
}
