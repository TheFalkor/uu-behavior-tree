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
            if (entity.targetPoint.dataTransform.position == entityTransform.position)
            {
                entity.transform.rotation = entity.targetPoint.dataTransform.rotation;
                return NodeState.SUCCESS;
            }
            else
            {
                entityTransform.eulerAngles = new Vector3(0, 0, Mathf.Atan2((entityTransform.position - entity.targetPoint.dataTransform.position).y, (entityTransform.position - entity.targetPoint.dataTransform.position).x) * Mathf.Rad2Deg + 90);
                entityTransform.position = Vector2.MoveTowards(entityTransform.position, entity.targetPoint.dataTransform.position, deltaTime * entity.movementSpeed);
                return NodeState.RUNNING;
            }
        }
    }
}
