using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorTree;
using BehaviorTree.Actions;
using BehaviorTree.Decorators;

public class Waiter : Entity
{
    [Header("References To Objects")]
    private List<DataPoint> islandPoints = new List<DataPoint>();
    private List<DataPoint> tablePoints = new List<DataPoint>();
    [HideInInspector] public List<CustomerMemory> customerList = new List<CustomerMemory>();

    [Header("Behavior Tree")]
    private Node tree;
    // 1. Serve food
    // 2. Get trash
    // 3. Get order
    

    void Start()
    {
        holdingSprite = transform.GetChild(0).GetComponent<SpriteRenderer>();

        foreach (GameObject go in GameObject.FindGameObjectsWithTag("KitchenIsland"))
        {
            islandPoints.Add(go.GetComponent<DataPoint>());
            dataPointList.Add(go.GetComponent<DataPoint>());
        }

        foreach (GameObject go in GameObject.FindGameObjectsWithTag("Table"))
        {
            tablePoints.Add(go.GetComponent<DataPoint>());
            dataPointList.Add(go.GetComponent<DataPoint>());
        }

        customerList.Add(new CustomerMemory { dataPoint = tablePoints[0], wantedFood = Data.FOOD_SANDWICH });

        movementSpeed = 2;

        // Re-usable
        Node goToTarget = new GoToTarget(this);
        Node pickupItem = new PickupItem(this);
        Node resetStatus = new SetStatus(this, "");
        Node resetTarget = new SetTarget(this, null);
        Node putDownItem = new PutDownItem(this);
        Node waitTime5 = new WaitForTime(0.5f);

        // Serving food
        Node findCustomer = new FindCustomerTable(this);
        Node serveFoodSequence = new Sequence(new List<Node> { findCustomer, goToTarget, waitTime5, putDownItem, resetStatus, resetTarget });
        Node serveHaveFood = new HaveType(this, serveFoodSequence, DataType.FOOD);

        Node setServeStatus = new SetStatus(this, "SERVE");
        Node detectFood = new DetectPointWithType(this, DataType.FOOD, tablePoints);
        Node servePickupSequence = new Sequence(new List<Node> { detectFood, goToTarget, waitTime5, pickupItem, setServeStatus });
        Node serveHaveNothing = new HaveItem(this, servePickupSequence, Data.NONE);

        Node serveStepSelector = new Selector(new List<Node> { serveHaveFood, serveHaveNothing });

        Node serveStatusCheck = new IsStatus(this, serveStepSelector, "SERVE");
        // !Serving food

        tree = new Selector(new List<Node> { serveStatusCheck });
    }


    void Update()
    {
        tree.Tick(Time.deltaTime);
    }
}


public struct CustomerMemory
{
    public Data wantedFood;
    public DataPoint dataPoint;
}