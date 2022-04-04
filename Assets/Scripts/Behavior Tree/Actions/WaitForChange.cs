using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace BehaviorTree.Actions
{
    public class WaitForChange : Node
    {
        private Entity entity;
        private DataType targetType;

        public WaitForChange(Entity entity, DataType targetType)
        {
            this.entity = entity;
            this.targetType = targetType;
        }

        public override NodeState Tick(float deltaTime)
        {
            if (entity.targetPoint != null)
                return NodeState.SUCCESS;

            for (int i = 0; i < entity.dataPointList.Count; i++)
            {
                if (entity.dataPointList[i].dataType == targetType)
                {
                    entity.targetPoint = entity.dataPointList[i];
                    return NodeState.SUCCESS;
                }
            }

            return NodeState.FAILURE;
        }
    }
}
