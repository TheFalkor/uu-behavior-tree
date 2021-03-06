using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Entity : MonoBehaviour
{
    [Header("Entity Variables")]
    [HideInInspector] public DataPoint targetPoint;
    [HideInInspector] public Transform targetPointTransform;
    [HideInInspector] public float waitTime = 0;
    [HideInInspector] public List<DataPoint> dataPointList;
    [HideInInspector] public Data currentlyHoldingData;
    [HideInInspector] public DataType currentlyHoldingType;
    [HideInInspector] public string currentStatus = "";
    [HideInInspector] public SpriteRenderer holdingSprite;
}
