using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace BehaviorTree.Actions
{
    public class PointHasType : Node
    {
        private Entity entity;
        private DataType targetType;

        public PointHasType(Entity entity, DataType targetType)
        {
            this.entity = entity;
            this.targetType = targetType;
        }

        public override NodeState Tick(float deltaTime)
        {
            if (entity.targetPoint == null || entity.targetPoint.dataType != targetType)
                return NodeState.FAILURE;

            return NodeState.SUCCESS;
        }
    }
}
