using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace BehaviorTree.Decorators
{
    public class ForceSuccess : Node
    {
        private Node child;

        public ForceSuccess(Node child)
        {
            this.child = child;
        }

        public override NodeState Tick(float deltaTime)
        {
            child.Tick(deltaTime);
            return NodeState.SUCCESS;
        }
    }
}
