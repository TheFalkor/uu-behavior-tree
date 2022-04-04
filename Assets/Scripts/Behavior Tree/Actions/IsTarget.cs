using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace BehaviorTree.Actions
{
    public class IsTarget : Node
    {
        private Entity entity;
        private Node child;
        private DataPoint targetPoint;

        public IsTarget(Entity entity, Node child, DataPoint targetPoint)
        {
            this.entity = entity;
            this.child = child;
            this.targetPoint = targetPoint;
        }

        public override NodeState Tick(float deltaTime)
        {
            if (entity.targetPoint != targetPoint)
                return NodeState.FAILURE;

            return child.Tick(deltaTime);
        }
    }
}
