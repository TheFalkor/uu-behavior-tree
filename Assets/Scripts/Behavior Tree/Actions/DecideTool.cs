using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace BehaviorTree.Actions
{
    public class DecideTool : Node
    {
        private Entity entity;
        private DataPoint stovePoint;
        private DataPoint microwavePoint;
        private DataPoint cuttingPoint;

        public DecideTool(Entity entity, DataPoint stove, DataPoint microwave, DataPoint cuttingBoard)
        {
            this.entity = entity;

            stovePoint = stove;
            microwavePoint = microwave;
            cuttingPoint = cuttingBoard;
        }

        public override NodeState Tick(float deltaTime)
        {
            if (entity.currentlyHoldingData == Data.ORDER_NOODLES)
                entity.targetPoint = microwavePoint;
            else if (entity.currentlyHoldingData == Data.ORDER_SAUSAGE)
                entity.targetPoint = stovePoint;
            else if (entity.currentlyHoldingData == Data.ORDER_SANDWICH)
                entity.targetPoint = cuttingPoint;

            entity.targetPointTransform = entity.targetPoint.GetClosestDataTransform(entity);
            return NodeState.SUCCESS;
        }
    }
}
