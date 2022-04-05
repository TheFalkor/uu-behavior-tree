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
    ORDER_SAUSAGE,
    FOOD_SANDWICH,
    FOOD_NOODLES,
    FOOD_SAUSAGE,
    PLATE_DIRTY,
    PLATE_CLEAN
}

public abstract class DataPoint : MonoBehaviour
{
    public DataType dataType;
    public Data data;
    [HideInInspector] public Transform dataTransform;
    protected SpriteRenderer inventorySprite;

    public void Pickup(Entity entity)
    {
        entity.currentlyHoldingData = data;
        entity.currentlyHoldingType = dataType;
        entity.holdingSprite.sprite = SpriteDatabase.instance.GetSprite(data);

        data = Data.NONE;
        dataType = DataType.NONE;
        if (inventorySprite)
            inventorySprite.sprite = null;
    }

    public void PutDown(Entity entity)
    {
        data = entity.currentlyHoldingData;
        dataType = entity.currentlyHoldingType;
        if (inventorySprite)
            inventorySprite.sprite = SpriteDatabase.instance.GetSprite(entity.currentlyHoldingData);

        entity.currentlyHoldingData = Data.NONE;
        entity.currentlyHoldingType = DataType.NONE;
        entity.holdingSprite.sprite = null;
    }

    public abstract bool Use(float deltaTime);
}
