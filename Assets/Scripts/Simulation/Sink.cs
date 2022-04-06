using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sink : DataPoint
{
    private const float CLEAN_TIME = 2.0f;
    private float timer = 0;


    private void Start()
    {
        dataTransformList.Add(transform.Find("DataTransform"));
        inventorySprite = transform.Find("InventorySprite").GetComponent<SpriteRenderer>();
    }

    public override bool Use(float deltaTime)
    {
        if (data == Data.PLATE_CLEAN)
            return true;

        timer += deltaTime;

        if (timer >= CLEAN_TIME)
        {
            SetData(DataType.DISH, Data.PLATE_CLEAN);
            timer = 0;
            return true;
        }

        return false;
    }
}
