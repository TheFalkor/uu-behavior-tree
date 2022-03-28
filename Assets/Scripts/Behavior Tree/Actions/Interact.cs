using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace BehaviorTree.Actions
{
    public class Interact : Node
    {
        private Entity entity;

        public Interact(Entity entity)
        {
            this.entity = entity;
        }

        public override NodeState Tick(float deltaTime)
        {
            entity.Interact();
            return NodeState.SUCCESS;
        }
    }
}
