using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace BehaviorTree.Actions
{
    public class GoToTarget : Node
    {
        private Entity entity;
        private Transform entityTransform;
        private Transform targetTransformMemory;
        private Quaternion targetDirection;

        public GoToTarget(Entity entity)
        {
            this.entity = entity;
            entityTransform = entity.transform;
        }

        public override NodeState Tick(float deltaTime)
        {
            if (entity.targetPointTransform.position == entityTransform.position)
            {
                entityTransform.rotation = Quaternion.RotateTowards(entityTransform.rotation, entity.targetPointTransform.rotation, deltaTime * 360);

                if (entityTransform.eulerAngles == entity.targetPointTransform.eulerAngles)
                    return NodeState.SUCCESS;
                else
                    return NodeState.RUNNING;
            }
            else
            {
                if (targetTransformMemory != entity.targetPointTransform)
                {
                    targetTransformMemory = entity.targetPointTransform;
                    Vector3 angle = new Vector3(0, 0, Mathf.Atan2((entityTransform.position - entity.targetPointTransform.position).y, (entityTransform.position - entity.targetPointTransform.position).x) * Mathf.Rad2Deg + 90);
                    targetDirection = Quaternion.Euler(angle);
                }

                if (entityTransform.eulerAngles != targetDirection.eulerAngles)
                    entityTransform.rotation = Quaternion.RotateTowards(entityTransform.rotation, targetDirection, deltaTime * 360);
                else
                    entityTransform.position = Vector2.MoveTowards(entityTransform.position, entity.targetPointTransform.position, deltaTime * 2);

                return NodeState.RUNNING;
            }
        }
    }
}
