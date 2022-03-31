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
        movementSpeed = 2;

        Node walkNode = new GoToTarget(this);
        Node waitNode = new WaitForTime(this);

        tree = new Sequence(new List<Node> { walkNode, waitNode });
    }
  
    void Update()
    {
        tree.Tick(Time.deltaTime);
    }

    public override bool DetectChange()
    {
        return false;
    }
}
