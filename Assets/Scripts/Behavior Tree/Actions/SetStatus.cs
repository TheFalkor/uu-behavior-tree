using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace BehaviorTree.Actions
{
    public class SetStatus : Node
    {
        private Entity entity;
        private string status;

        public SetStatus(Entity entity, string status)
        {
            this.entity = entity;
            this.status = status;
        }

        public override NodeState Tick(float deltaTime)
        {
            entity.currentStatus = status;

            return NodeState.SUCCESS;
        }
    }
}
