using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace BehaviorTree.Actions
{
    public class PutDownItem : Node
    {
        private readonly Entity entity;

        public PutDownItem(Entity entity)
        {
            this.entity = entity;
        }

        public override NodeState Tick(float deltaTime)
        {
            if (entity.currentlyHoldingData != Data.NONE)
                entity.targetPoint.PutDown(entity);

            return NodeState.SUCCESS;
        }
    }
}
