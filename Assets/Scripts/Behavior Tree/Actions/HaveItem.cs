using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace BehaviorTree.Actions
{
    public class HaveItem : Node
    {
        private Entity entity;
        private Node child;
        private Data targetData;

        public HaveItem(Entity entity, Node child, Data targetData)
        {
            this.entity = entity;
            this.child = child;
            this.targetData = targetData;
        }

        public override NodeState Tick(float deltaTime)
        {
            if (entity.currentlyHoldingData != targetData)
                return NodeState.FAILURE;

            return child.Tick(deltaTime);
        }
    }
}
