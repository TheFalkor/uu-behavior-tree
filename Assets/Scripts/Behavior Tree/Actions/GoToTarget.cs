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
            if (entity.targetPosition == (Vector2)entityTransform.position)
            {
                return NodeState.SUCCESS;
            }
            else
            {
                entityTransform.eulerAngles = new Vector3(0, 0, Mathf.Atan2((entityTransform.position - (Vector3)entity.targetPosition).y, (entityTransform.position - (Vector3)entity.targetPosition).x) * Mathf.Rad2Deg + 90);
                entityTransform.position = Vector2.MoveTowards(entityTransform.position, entity.targetPosition, deltaTime * entity.movementSpeed);
                return NodeState.RUNNING;
            }
        }
    }
}
