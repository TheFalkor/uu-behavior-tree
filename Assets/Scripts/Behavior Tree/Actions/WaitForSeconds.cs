using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace BehaviorTree.Actions
{
    public class WaitForSeconds : Node
    {
        private Entity entity;

        public WaitForSeconds(Entity entity)
        {
            this.entity = entity;
        }

        public override NodeState Tick()
        {
            if (entity.waitTime <= 0)
            {
                return NodeState.SUCCESS;
            }
            else
            {
                entity.waitTime -= Time.deltaTime;
                return NodeState.RUNNING;
            }
        }
    }
}
