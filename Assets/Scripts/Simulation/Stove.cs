using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stove : DataPoint
{
    private const float COOK_TIME = 4.0f;
    private float timer = 0;


    private void Start()
    {
        dataTransform = transform.Find("DataTransform");
        inventorySprite = transform.Find("InventorySprite").GetComponent<SpriteRenderer>();
    }

    public override bool Use(float deltaTime)
    {
        if (data == Data.FOOD_SAUSAGE)
            return true;

        timer += deltaTime;

        if (timer >= COOK_TIME)
        {
            SetData(DataType.FOOD, Data.FOOD_SAUSAGE);
            timer = 0;
            return true;
        }

        return false;
    }
}
