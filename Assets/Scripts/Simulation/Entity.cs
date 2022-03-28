using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Entity : MonoBehaviour
{

    [Header("Entity Variables")]
    [HideInInspector] public Vector2 targetPosition;
    [HideInInspector] public float movementSpeed = 1;
    [HideInInspector] public float waitTime;

    public abstract void Interact();
}
