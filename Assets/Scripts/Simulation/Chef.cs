using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorTree;
using BehaviorTree.Actions;
using BehaviorTree.Decorators;


public class Chef : Entity
{
    [Header("Reference To Objects")]
    public DataPoint dirtDishPlaceTEMP;
    public DataPoint sinkPoint;
    public DataPoint drawerPoint;


    [Header("Behavior Tree Variables")]
    private Node tree;


    void Start()
    {
        holdingSprite = transform.GetChild(0).GetComponent<SpriteRenderer>();
        dataPointList.Add(dirtDishPlaceTEMP);

        movementSpeed = 2;
        // Re-usable
        Node goToTarget = new GoToTarget(this);
        Node pickupItem = new PickupItem(this);
        Node resetStatus = new SetStatus(this, "");
        Node resetTarget = new SetTarget(this, null);
        Node putDownItem = new PutDownItem(this);
        Node useItem = new UseItem(this);

        // Dishes

        Node setDrawerTarget = new SetTarget(this, drawerPoint);
        Node putAwayDishSequence = new Sequence(new List<Node> { setDrawerTarget, goToTarget, putDownItem, resetStatus, resetTarget });
        Node dishHaveClean = new HaveItem(this, putAwayDishSequence, Data.PLATE_CLEAN);

        Node waitBeforePickup = new WaitForTime(0.5f);
        Node dishCleanSequence = new Sequence(new List<Node> { goToTarget, putDownItem, useItem, waitBeforePickup, pickupItem });
        Node targetIsSink = new IsTarget(this, dishCleanSequence, sinkPoint);

        Node setDishStatus = new SetStatus(this, "DISH");
        Node setSinkTarget = new SetTarget(this, sinkPoint);
        Node detectDish = new WaitForChange(this, DataType.DISH);
        Node dishPickupSequence = new Sequence(new List<Node> { detectDish, goToTarget, pickupItem, setSinkTarget, setDishStatus });
        Node dishHaveNothing = new HaveItem(this, dishPickupSequence, Data.NONE);

        Node dishStepSelector = new Selector(new List<Node> { dishHaveClean, targetIsSink, dishHaveNothing });

        Node startStatusCheck = new IsStatus(this, dishStepSelector, "DISH");
        // !Dishes

        tree = new Selector(new List<Node> { startStatusCheck });
    }
  
    void Update()
    {
        tree.Tick(Time.deltaTime);
    }

}
