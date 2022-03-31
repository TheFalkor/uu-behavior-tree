using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace BehaviorTree.Actions
{
    public class WaitForChange : Node
    {
        private Entity entity;
        private DataType targetType;
        private bool countPatience;

        public WaitForChange(Entity entity, DataType targetType, bool countPatience)
        {
            this.entity = entity;
            this.targetType = targetType;
            this.countPatience = countPatience;
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

            if (countPatience)
                entity.patienceTimer -= deltaTime;

            if (entity.patienceTimer > 0)
                return NodeState.RUNNING;

            return NodeState.FAILURE;
        }
    }
}
