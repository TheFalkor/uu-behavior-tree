using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace BehaviorTree.Actions
{
    public class PickupItem : Node
    {
        private Entity entity;

        public PickupItem(Entity entity)
        {
            this.entity = entity;
        }

        public override NodeState Tick(float deltaTime)
        {
            entity.targetPoint.Pickup(entity);
            return NodeState.SUCCESS;
        }
    }
}
