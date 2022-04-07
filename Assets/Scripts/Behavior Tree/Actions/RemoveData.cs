using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace BehaviorTree.Actions
{
    public class RemoveData : Node
    {
        private Entity entity;


        public RemoveData(Entity entity)
        {
            this.entity = entity;

        }

        public override NodeState Tick(float deltaTime)
        {
            entity.currentlyHoldingType = DataType.NONE;
            entity.currentlyHoldingData = Data.NONE;
            return NodeState.SUCCESS;
        }
    }
}
