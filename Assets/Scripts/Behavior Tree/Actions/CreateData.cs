using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace BehaviorTree.Actions
{
    public class CreateData : Node
    {
        private Entity entity;
        private DataType createType;
        private Data createData;

        public CreateData(Entity entity, DataType type, Data data)
        {
            this.entity = entity;
            this.createType = type;
            this.createData = data;
        }

        public override NodeState Tick(float deltaTime)
        {
            entity.currentlyHoldingType = createType;
            entity.currentlyHoldingData = createData;
            return NodeState.SUCCESS;
        }
    }
}
