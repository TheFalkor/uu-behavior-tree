using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace BehaviorTree.Decorators
{
    public class ForceFailure : Node
    {
        private Node child;

        public ForceFailure(Node child)
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
