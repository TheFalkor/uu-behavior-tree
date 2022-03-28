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
            if (entity.targetPosition == (Vector2)entity.transform.position)
            {
                return NodeState.SUCCESS;
            }
            else
            {
                entity.transform.eulerAngles = new Vector3(0, 0, Mathf.Atan2((entity.transform.position - (Vector3)entity.targetPosition).y, (entity.transform.position - (Vector3)entity.targetPosition).x) * Mathf.Rad2Deg + 90);
                entity.transform.position = Vector2.MoveTowards(entity.transform.position, entity.targetPosition, Time.deltaTime);
                return NodeState.RUNNING;
            }
        }
    }
}
