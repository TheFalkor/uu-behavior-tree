using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace BehaviorTree.Actions
{
    public class UseItem : Node
    {
        private Entity entity;

        public UseItem(Entity entity)
        {
            this.entity = entity;
        }

        public override NodeState Tick(float deltaTime)
        {
            if (entity.targetPoint == null || entity.targetPoint.Use(deltaTime))
                return NodeState.SUCCESS;

            return NodeState.RUNNING;
        }
    }
}
