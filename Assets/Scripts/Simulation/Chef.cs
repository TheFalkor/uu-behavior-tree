using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorTree;
using BehaviorTree.Actions;


public class Chef : Entity
{
    [Header("Reference To Objects")]
    private DataPoint sinkPoint;
    private DataPoint plateHolderPoint;
    [Space]
    private DataPoint stovePoint;
    private DataPoint microwavePoint;
    private DataPoint cuttingBoardPoint;


    [Header("Behavior Tree")]
    private Node tree;


    public void Initialize()
    {
        holdingSprite = transform.GetChild(0).GetComponent<SpriteRenderer>();

        foreach (GameObject go in GameObject.FindGameObjectsWithTag("KitchenIsland"))
        {
            dataPointList.Add(go.GetComponent<DataPoint>());
        }

        sinkPoint = GameObject.FindGameObjectWithTag("Sink").GetComponent<DataPoint>();
        plateHolderPoint = GameObject.FindGameObjectWithTag("PlateHolder").GetComponent<DataPoint>();
        stovePoint = GameObject.FindGameObjectWithTag("Stove").GetComponent<DataPoint>();
        microwavePoint = GameObject.FindGameObjectWithTag("Microwave").GetComponent<DataPoint>();
        cuttingBoardPoint = GameObject.FindGameObjectWithTag("CuttingBoard").GetComponent<DataPoint>();

        // Re-usable
        Node goToTarget = new GoToTarget(this);
        Node pickupItem = new PickupItem(this);
        Node resetStatus = new SetStatus(this, "");
        Node resetTarget = new SetTarget(this, null);
        Node putDownItem = new PutDownItem(this);
        Node useItem = new UseItem(this);
        Node waitTime5 = new WaitForTime(0.5f);
        Node UseToolSequence = new Sequence(new List<Node> { goToTarget, putDownItem, useItem, waitTime5, pickupItem });

        // Dishes
        Node SetPlateHoldeTarget = new SetTarget(this, plateHolderPoint);
        Node putAwayDishSequence = new Sequence(new List<Node> { SetPlateHoldeTarget, goToTarget, waitTime5, putDownItem, resetStatus, resetTarget });
        Node dishHaveClean = new HaveItem(this, putAwayDishSequence, Data.PLATE_CLEAN);

        Node targetIsSink = new IsTarget(this, UseToolSequence, sinkPoint);

        Node setDishStatus = new SetStatus(this, "DISH");
        Node setSinkTarget = new SetTarget(this, sinkPoint);
        Node detectDish = new DetectPointWithType(this, DataType.DISH, null);
        Node dishPickupSequence = new Sequence(new List<Node> { detectDish, goToTarget, waitTime5, pickupItem, setSinkTarget, setDishStatus });
        Node dishHaveNothing = new HaveItem(this, dishPickupSequence, Data.NONE);

        Node dishStepSelector = new Selector(new List<Node> { dishHaveClean, targetIsSink, dishHaveNothing });

        Node dishStatusCheck = new IsStatus(this, dishStepSelector, "DISH");
        // !Dishes

        // Cooking
        Node findEmptyPlace = new DetectPointWithType(this, DataType.NONE, new List<DataPoint> { stovePoint, microwavePoint, cuttingBoardPoint });
        Node putUpReadyFood = new Sequence(new List<Node> { findEmptyPlace, goToTarget, waitTime5, putDownItem, resetStatus, resetTarget });
        Node cookHaveFood = new HaveType(this, putUpReadyFood, DataType.FOOD);

        Node usingStove = new IsTarget(this, UseToolSequence, stovePoint);
        Node usingMicrowave = new IsTarget(this, UseToolSequence, microwavePoint);
        Node usingCuttingBoard = new IsTarget(this, UseToolSequence, cuttingBoardPoint);

        Node setCookStatus = new SetStatus(this, "COOK");
        Node decideTool = new DecideTool(this, stovePoint, microwavePoint, cuttingBoardPoint);
        Node detectOrder = new DetectPointWithType(this, DataType.ORDER, null);
        Node pickupOrderSequence = new Sequence(new List<Node> { detectOrder, goToTarget, waitTime5, pickupItem, decideTool, setCookStatus });
        Node cookHaveNothing = new HaveItem(this, pickupOrderSequence, Data.NONE);

        Node cookStepSelector = new Selector(new List<Node> { cookHaveFood, usingStove, usingMicrowave, usingCuttingBoard, cookHaveNothing });

        Node cookStatusCheck = new IsStatus(this, cookStepSelector, "COOK");
        // !Cooking

        tree = new Selector(new List<Node> { dishStatusCheck, cookStatusCheck });
    }
  
    public void Tick(float deltaTime)
    {
        tree.Tick(deltaTime);
    }

}
