using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CuttingBoard : DataPoint
{
    private const float COOK_TIME = 1.5f;
    private float timer = 0;


    private void Start()
    {
        dataTransformList.Add(transform.Find("DataTransform"));
        inventorySprite = transform.Find("InventorySprite").GetComponent<SpriteRenderer>();
    }

    public override bool Use(float deltaTime)
    {
        if (data == Data.FOOD_SANDWICH)
            return true;

        timer += deltaTime;

        if (timer >= COOK_TIME)
        {
            SetData(DataType.FOOD, Data.FOOD_SANDWICH);
            timer = 0;
            return true;
        }

        return false;
    }
}
