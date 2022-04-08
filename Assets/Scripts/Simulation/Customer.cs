using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorTree;
using BehaviorTree.Actions;

public class Customer : Entity
{
    [Header("References To Objects")]
    private List<DataPoint> tablePoints = new List<DataPoint>();
    private DataPoint doorExit;
    private float lifeTime = 0;

    [Header("Behavior Tree")]
    private Node tree;


    public void Initialize()
    {
        if (!holdingSprite)
        {
            holdingSprite = transform.GetChild(0).GetComponent<SpriteRenderer>();

            foreach (GameObject go in GameObject.FindGameObjectsWithTag("Table"))
            {
                tablePoints.Add(go.GetComponent<DataPoint>());
                dataPointList.Add(go.GetComponent<DataPoint>());
            }

            doorExit = GameObject.FindGameObjectWithTag("Door").GetComponent<DataPoint>();
        }


        Data wantFood;
        Data waitFood;

        int random = Random.Range(0, 3);

        if (random == 0)
        {
            wantFood = Data.ORDER_NOODLES;
            waitFood = Data.WAITING_NOODLES;
        }
        else if (random == 1)
        {
            wantFood = Data.ORDER_SANDWICH;
            waitFood = Data.WAITING_SANDWICH;
        }
        else
        {
            wantFood = Data.ORDER_SAUSAGE;
            waitFood = Data.WAITING_SAUSAGE;
        }

        // Re-usable
        Node goToTarget = new GoToTarget(this);
        Node pickupItem = new PickupItem(this);
        Node resetStatus = new SetStatus(this, "");
        Node resetTarget = new SetTarget(this, null);
        Node putDownItem = new PutDownItem(this);
        Node waitTime5 = new WaitForTime(0.5f);
        Node removeData = new RemoveData(this);
        Node useItem = new UseItem(this);

        // Leave


        Node leaveSequence = new Sequence(new List<Node> { goToTarget, waitTime5, resetTarget, resetStatus });

        Node leaveStatusCheck = new IsStatus(this, leaveSequence, "LEAVE");
        // !Leave

        // Eat
        Node waitForFood = new PointHasType(this, DataType.FOOD);
        Node setExitTarget = new SetTarget(this, doorExit);
        Node setLeaveStatus = new SetStatus(this, "LEAVE");

        Node eatSequence = new Sequence(new List<Node> { waitForFood, useItem, setExitTarget, setLeaveStatus });

        Node eatStatusCheck = new IsStatus(this, eatSequence, "EAT");
        // !Eat

        // Wait
        Node waitOrderTaken = new PointHasType(this, DataType.NONE);
        Node createWaitOrder = new CreateData(this, DataType.WAITING, waitFood);
        Node setEatStatus = new SetStatus(this, "EAT");

        Node waitSequence = new Sequence(new List<Node> { waitOrderTaken, createWaitOrder, putDownItem, setEatStatus });

        Node waitStatusCheck = new IsStatus(this, waitSequence, "WAIT");
        // !Wait

        // Order
        Node setOrderStatus = new SetStatus(this, "ORDER");
        Node detectEmptyTable = new DetectPointWithType(this, DataType.NONE, null);
        Node createOrder = new CreateData(this, DataType.ORDER, wantFood);
        Node setWaitStatus = new SetStatus(this, "WAIT");

        Node orderSequence = new Sequence(new List<Node> { setOrderStatus, detectEmptyTable, goToTarget, createOrder, waitTime5, putDownItem, setWaitStatus });    //  

        Node orderStatusCheck = new IsStatus(this, orderSequence, "ORDER");
        // !Order

        tree = new Selector(new List<Node> { orderStatusCheck, waitStatusCheck, eatStatusCheck, leaveStatusCheck });
    }

    
    public void Tick(float deltaTime)
    {
        tree.Tick(deltaTime);
        lifeTime += deltaTime;

        if (doorExit.transform.position == transform.position && lifeTime >= 5)
        {
            lifeTime = 0;
            Initialize();
        }
    }
}
