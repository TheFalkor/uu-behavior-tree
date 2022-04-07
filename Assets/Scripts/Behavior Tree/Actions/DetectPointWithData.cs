using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace BehaviorTree.Actions
{
    public class DetectPointWithData : Node
    {
        private Entity entity;
        private Data targetData;
        private List<DataPoint> ignorePoints;

        public DetectPointWithData(Entity entity, Data targetData, List<DataPoint> ignorePoints)
        {
            this.entity = entity;
            this.targetData = targetData;
            this.ignorePoints = ignorePoints;
        }

        public override NodeState Tick(float deltaTime)
        {
            if (ignorePoints == null || !ignorePoints.Contains(entity.targetPoint))
            {
                if (entity.targetPoint != null && entity.targetPoint.data == targetData)
                    return NodeState.SUCCESS;
            }


            for (int i = 0; i < entity.dataPointList.Count; i++)
            {
                if ((ignorePoints == null || !ignorePoints.Contains(entity.dataPointList[i])) && entity.dataPointList[i].data == targetData)
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
