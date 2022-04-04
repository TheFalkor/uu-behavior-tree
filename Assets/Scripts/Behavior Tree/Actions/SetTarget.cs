using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace BehaviorTree.Actions
{
    public class SetTarget : Node
    {
        private Entity entity;
        private DataPoint targetPoint;

        public SetTarget(Entity entity, DataPoint targetPoint)
        {
            this.entity = entity;
            this.targetPoint = targetPoint;
        }

        public override NodeState Tick(float deltaTime)
        {
            entity.targetPoint = targetPoint;

            return NodeState.SUCCESS;
        }
    }
}
