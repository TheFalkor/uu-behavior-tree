using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Table : DataPoint
{
    private const float EAT_TIME = 4.0f;
    private float timer = 0;


    private void Start()
    {
        dataTransformList.Add(transform.Find("DataTransformL"));
        dataTransformList.Add(transform.Find("DataTransformR"));
        inventorySprite = transform.Find("InventorySprite").GetComponent<SpriteRenderer>();
    }

    public override bool Use(float deltaTime)
    {
        if (data == Data.PLATE_DIRTY)
            return true;

        timer += deltaTime;

        if (timer >= EAT_TIME)
        {
            SetData(DataType.DISH, Data.PLATE_DIRTY);
            timer = 0;
            return true;
        }

        return false;
    }
}
