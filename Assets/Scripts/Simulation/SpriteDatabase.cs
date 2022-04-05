using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteDatabase : MonoBehaviour
{
    [Header("Inspector Reference")]
    public Sprite orderSprite;
    [Space]
    public Sprite sandwichSprite;
    public Sprite noodleSprite;
    public Sprite waterSprite;
    [Space]
    public Sprite plateClean;
    public Sprite plateDirty;

    public static SpriteDatabase instance;

    void Start()
    {
        if (!instance)
            instance = this;
    }

    public Sprite GetSprite(Data data)
    {
        switch (data)
        {
            case Data.NONE:
                return null;

            case Data.ORDER_SANDWICH:
            case Data.ORDER_NOODLES:
            case Data.ORDER_WATER:
                return orderSprite;

            case Data.FOOD_SANDWICH:
                return sandwichSprite;

            case Data.FOOD_NOODLES:
                return noodleSprite;

            case Data.FOOD_WATER:
                return waterSprite;

            case Data.PLATE_CLEAN:
                return plateClean;

            case Data.PLATE_DIRTY:
                return plateDirty;
        }

        return null;
    }
}
