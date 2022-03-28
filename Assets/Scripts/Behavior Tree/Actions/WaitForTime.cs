using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace BehaviorTree.Actions
{
    public class WaitForTime : Node
    {
        private Entity entity;

        public WaitForTime(Entity entity)
        {
            this.entity = entity;
        }

        public override NodeState Tick(float deltaTime)
        {
            if (entity.waitTime <= 0)
            {
                return NodeState.SUCCESS;
            }
            else
            {
                entity.waitTime -= deltaTime;
                return NodeState.RUNNING;
            }
        }
    }
}
