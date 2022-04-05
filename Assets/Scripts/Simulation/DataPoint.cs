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

        SetData(DataType.NONE, Data.NONE);
    }

    public void PutDown(Entity entity)
    {
        SetData(entity.currentlyHoldingType, entity.currentlyHoldingData);

        entity.currentlyHoldingData = Data.NONE;
        entity.currentlyHoldingType = DataType.NONE;
        entity.holdingSprite.sprite = null;
    }

    protected void SetData(DataType type, Data data)
    {
        Sprite sprite = null;
        if (type == DataType.NONE)
        {
            this.dataType = DataType.NONE;
            this.data = Data.NONE;
            sprite = null;
        }
        else
        {
            this.dataType = type;
            this.data = data;
            sprite = SpriteDatabase.instance.GetSprite(data);
        }

        if (inventorySprite)
            inventorySprite.sprite = sprite;
    }

    public abstract bool Use(float deltaTime);
}
