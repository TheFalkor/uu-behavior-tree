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

public class DataPoint : MonoBehaviour
{
    public DataType dataType;
    public Data data;
    public Transform dataTransform;
}
