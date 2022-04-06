using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace BehaviorTree.Actions
{
    public class DetectPointWithType : Node
    {
        private Entity entity;
        private DataType targetType;
        private List<DataPoint> ignorePoints;

        public DetectPointWithType(Entity entity, DataType targetType, List<DataPoint> ignorePoints)
        {
            this.entity = entity;
            this.targetType = targetType;
            this.ignorePoints = ignorePoints;
        }

        public override NodeState Tick(float deltaTime)
        {
            if (ignorePoints == null || !ignorePoints.Contains(entity.targetPoint))
            {
                if (entity.targetPoint != null && entity.targetPoint.dataType == targetType)
                    return NodeState.SUCCESS;
            }


            for (int i = 0; i < entity.dataPointList.Count; i++)
            {
                if ((ignorePoints == null || !ignorePoints.Contains(entity.dataPointList[i])) && entity.dataPointList[i].dataType == targetType)
                {
                    entity.targetPoint = entity.dataPointList[i];
                    entity.targetPointTransform = entity.targetPoint.GetClosestDataTransform(entity);
                    return NodeState.SUCCESS;
                }
            }

            return NodeState.FAILURE;
        }
    }
}
