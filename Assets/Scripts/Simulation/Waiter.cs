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
        Node removeData = new RemoveData(this);
        Node findEmptyPlace = new DetectPointWithType(this, DataType.NONE, tablePoints);

        // Serving food
        Node serveFoodSequence = new Sequence(new List<Node> { goToTarget, waitTime5, putDownItem, removeData, resetStatus, resetTarget });

        Node findSandwichTable = new DetectPointWithData(this, Data.WAITING_SANDWICH, null);
        Node sandwichSequence = new Sequence(new List<Node> { findSandwichTable, serveFoodSequence });
        Node haveSandwich = new HaveItem(this, sandwichSequence, Data.FOOD_SANDWICH);

        Node findNoodlesTable = new DetectPointWithData(this, Data.WAITING_NOODLES, null);
        Node noodlesSequence = new Sequence(new List<Node> { findNoodlesTable, serveFoodSequence });
        Node haveNoodles = new HaveItem(this, noodlesSequence, Data.FOOD_NOODLES);

        Node findSausageTable = new DetectPointWithData(this, Data.WAITING_SAUSAGE, null);
        Node sausageSequence = new Sequence(new List<Node> { findSausageTable, serveFoodSequence });
        Node haveSausage = new HaveItem(this, sausageSequence, Data.FOOD_SAUSAGE);

        Node findServeSelector = new Selector(new List<Node> { haveSandwich, haveNoodles, haveSausage });

        Node serveHaveFood = new HaveType(this, findServeSelector, DataType.FOOD);

        Node setServeStatus = new SetStatus(this, "SERVE");
        Node detectFood = new DetectPointWithType(this, DataType.FOOD, tablePoints);
        Node servePickupSequence = new Sequence(new List<Node> { detectFood, goToTarget, waitTime5, pickupItem, setServeStatus });
        Node serveHaveNothing = new HaveItem(this, servePickupSequence, Data.NONE);

        Node serveStepSelector = new Selector(new List<Node> { serveHaveFood, serveHaveNothing });

        Node serveStatusCheck = new IsStatus(this, serveStepSelector, "SERVE");
        // !Serving food

        // Clean
        Node setCleanStatus = new SetStatus(this, "CLEAN");
        Node detectDish = new DetectPointWithType(this, DataType.DISH, islandPoints);
        Node cleanPickupSequence = new Sequence(new List<Node> { detectDish, goToTarget, waitTime5, pickupItem, setCleanStatus });
        Node cleanHaveNothing = new HaveItem(this, cleanPickupSequence, Data.NONE);

        Node cleanDepositSequence = new Sequence(new List<Node> { findEmptyPlace, goToTarget, waitTime5, putDownItem, resetStatus, resetTarget });
        Node cleanHaveDish = new HaveItem(this, cleanDepositSequence, Data.PLATE_DIRTY);

        Node cleanStepSelector = new Selector(new List<Node> { cleanHaveDish, cleanHaveNothing });

        Node cleanStatusCheck = new IsStatus(this, cleanStepSelector, "CLEAN");
        // !Clean

        // Get order
        Node setOrderStatus = new SetStatus(this, "ORDER");
        Node detectOrder = new DetectPointWithType(this, DataType.ORDER, islandPoints);
        Node orderPickupSequence = new Sequence(new List<Node> { detectOrder, goToTarget, waitTime5, pickupItem, setOrderStatus });
        Node orderHaveNothing = new HaveItem(this, orderPickupSequence, Data.NONE);

        Node orderDepositSequence = new Sequence(new List<Node> { findEmptyPlace, goToTarget, waitTime5, putDownItem, resetStatus, resetTarget });
        Node orderHaveOrder = new HaveType(this, orderDepositSequence, DataType.ORDER);

        Node orderStepSelector = new Selector(new List<Node> { orderHaveOrder, orderHaveNothing });

        Node orderStatusCheck = new IsStatus(this, orderStepSelector, "ORDER");
        // !Get order

        tree = new Selector(new List<Node> { serveStatusCheck, cleanStatusCheck, orderStatusCheck });
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