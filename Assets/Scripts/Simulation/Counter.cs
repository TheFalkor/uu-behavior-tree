using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Counter : DataPoint
{
    public int counting = 0;

    private void Start()
    {
        inventorySprite = transform.Find("InventorySprite").GetComponent<SpriteRenderer>();
    }

    public override bool Use()
    {
        counting += 1;

        if (counting >= 5000)
        {
            data = Data.PLATE_CLEAN;
            return true;
        }

        return false;
    }
}
