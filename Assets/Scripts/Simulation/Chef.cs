using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorTree;
using BehaviorTree.Actions;
using BehaviorTree.Decorators;


public class Chef : Entity
{

    [Header("Behavior Tree Variables")]
    private Node tree;


    void Start()
    {
        Interact();

        Node walkNode = new GoToTarget(this);
        Node waitNode = new WaitForTime(this);
        Node interactNode = new Interact(this);

        tree = new Sequence(new List<Node> { walkNode, waitNode, interactNode });
    }
  
    void Update()
    {
        tree.Tick();
    }

    public override void Interact()
    {
        targetPosition = new Vector2(Random.Range(0, 5), Random.Range(0, 5));
        waitTime = 3;
    }
}
