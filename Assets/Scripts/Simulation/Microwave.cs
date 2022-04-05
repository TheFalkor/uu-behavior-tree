using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Microwave : DataPoint
{
    private const float COOK_TIME = 1f;
    private float timer = 0;


    private void Start()
    {
        dataTransform = transform.Find("DataTransform");
        inventorySprite = transform.Find("InventorySprite").GetComponent<SpriteRenderer>();
    }

    public override bool Use(float deltaTime)
    {
        if (data == Data.FOOD_NOODLES)
            return true;

        if (timer == 0)
            transform.GetChild(0).GetComponent<Animator>().SetBool("Active", true);

        timer += deltaTime;

        if (timer >= COOK_TIME)
        {
            SetData(DataType.FOOD, Data.FOOD_NOODLES);
            timer = 0;
            transform.GetChild(0).GetComponent<Animator>().SetBool("Active", false);
            return true;
        }

        return false;
    }
}
