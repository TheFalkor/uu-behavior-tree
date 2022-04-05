using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Counter : DataPoint
{
    public int counting = 0;

    private void Start()
    {
        dataTransform = transform.Find("DataTransform");
        inventorySprite = transform.Find("InventorySprite").GetComponent<SpriteRenderer>();
    }

    public override bool Use(float deltaTime)
    {
        Debug.Log("Counter.Use has not been added");
        return false;
    }
}
