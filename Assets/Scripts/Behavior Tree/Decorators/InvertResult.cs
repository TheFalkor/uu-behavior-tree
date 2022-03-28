using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace BehaviorTree.Decorators
{
    public class InvertResult : Node
    {
        private Node child;

        public InvertResult(Node child)
        {
            this.child = child;
        }

        public override NodeState Tick(float deltaTime)
        {
            NodeState result = child.Tick(deltaTime);

            if (result == NodeState.SUCCESS)
                return NodeState.FAILURE;
            else if (result == NodeState.FAILURE)
                return NodeState.SUCCESS;

            return result;
        }
    }
}
