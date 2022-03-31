using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace BehaviorTree.Actions
{
    public class IsCalm : Node
    {
        private Entity entity;
        private Node child;

        public IsCalm(Entity entity, Node child)
        {
            this.entity = entity;
            this.child = child;
        }

        public override NodeState Tick(float deltaTime)
        {
            if (entity.patienceTimer > 0)
                return NodeState.SUCCESS;

            return child.Tick(deltaTime);
        }
    }
}
