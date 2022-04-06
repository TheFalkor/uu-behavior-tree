using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace BehaviorTree.Actions
{
    public class GoToTarget : Node
    {
        private Entity entity;
        private Transform entityTransform;

        public GoToTarget(Entity entity)
        {
            this.entity = entity;
            entityTransform = entity.transform;
        }

        public override NodeState Tick(float deltaTime)
        {
            if (entity.targetPointTransform.position == entityTransform.position)
            {
                entityTransform.eulerAngles = Vector3.MoveTowards(entityTransform.eulerAngles, entity.targetPointTransform.eulerAngles, deltaTime * entity.movementSpeed * 180); ;

                if (entityTransform.eulerAngles == entity.targetPointTransform.eulerAngles)
                    return NodeState.SUCCESS;
                else
                    return NodeState.RUNNING;
            }
            else
            {
                entityTransform.eulerAngles = new Vector3(0, 0, Mathf.Atan2((entityTransform.position - entity.targetPointTransform.position).y, (entityTransform.position - entity.targetPointTransform.position).x) * Mathf.Rad2Deg + 90);
                entityTransform.position = Vector2.MoveTowards(entityTransform.position, entity.targetPointTransform.position, deltaTime * entity.movementSpeed);
                return NodeState.RUNNING;
            }
        }
    }
}
