using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KitchenIsland : DataPoint
{
    private void Start()
    {
        dataTransformList.Add(transform.Find("DataTransformL"));
        dataTransformList.Add(transform.Find("DataTransformR"));
        inventorySprite = transform.Find("InventorySprite").GetComponent<SpriteRenderer>();
    }

    public override bool Use(float deltaTime)
    {
        Debug.Log("Counter.Use has not been added");
        return false;
    }
}
