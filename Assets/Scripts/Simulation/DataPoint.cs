using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum DataType
{
    NONE,
    ORDER,
    FOOD,
    DISH,
    WAITING
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
    PLATE_CLEAN,
    WAITING_SANDWICH,
    WAITING_NOODLES,
    WAITING_SAUSAGE
}

public abstract class DataPoint : MonoBehaviour
{
    [HideInInspector] public DataType dataType;
    [HideInInspector] public Data data;
    protected List<Transform> dataTransformList = new List<Transform>();
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

    public Transform GetClosestDataTransform(Entity entity)
    {
        if (dataTransformList.Count == 1)
            return dataTransformList[0];
        else if (dataTransformList.Count == 2)
        {
            if (Vector2.Distance(entity.transform.position, dataTransformList[0].position) < Vector2.Distance(entity.transform.position, dataTransformList[1].position))
                return dataTransformList[0];
            else
                return dataTransformList[1];
        }

        return null;
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
