using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace BehaviorTree.Actions
{
    public class HaveType : Node
    {
        private Entity entity;
        private Node child;
        private DataType targetType;

        public HaveType(Entity entity, Node child, DataType targetType)
        {
            this.entity = entity;
            this.child = child;
            this.targetType = targetType;
        }

        public override NodeState Tick(float deltaTime)
        {
            if (entity.currentlyHoldingType != targetType)
                return NodeState.FAILURE;

            return child.Tick(deltaTime);
        }
    }
}
