using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace BehaviorTree.Actions
{
    public class IsStatus : Node
    {
        private Entity entity;
        private Node child;
        private string targetStatus;

        public IsStatus(Entity entity, Node child, string targetStatus)
        {
            this.entity = entity;
            this.child = child;
            this.targetStatus = targetStatus;
        }

        public override NodeState Tick(float deltaTime)
        {
            if (entity.currentStatus != "" && entity.currentStatus != targetStatus)
                return NodeState.FAILURE;

            return child.Tick(deltaTime);
        }
    }
}
