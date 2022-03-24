using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace BehaviorTree.Actions
{
    public class GoToTarget : Node
    {
        private Entity entity;

        public GoToTarget(Entity entity)
        {
            this.entity = entity;
        }

        public override NodeState Tick()
        {
            if (entity.targetPosition == (Vector2)transform.position)
            {
                return NodeState.SUCCESS;
            }
            else
            {
                transform.position = Vector2.MoveTowards(transform.position, entity.targetPosition, Time.deltaTime);
                return NodeState.RUNNING;
            }
        }
    }
}
