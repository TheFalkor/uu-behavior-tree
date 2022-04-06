using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace BehaviorTree.Actions
{
    public class FindCustomerTable : Node
    {
        private Waiter waiter;

        public FindCustomerTable(Waiter waiter)
        {
            this.waiter = waiter;
        }

        public override NodeState Tick(float deltaTime)
        {
            foreach (CustomerMemory c in waiter.customerList)
            {
                if (c.wantedFood == waiter.currentlyHoldingData)
                {
                    waiter.targetPoint = c.dataPoint;
                    waiter.targetPointTransform = waiter.targetPoint.GetClosestDataTransform(waiter);
                    return NodeState.SUCCESS;
                }
            }

            waiter.targetPoint = null;
            waiter.targetPointTransform = null;
            return NodeState.SUCCESS;
        }
    }
}
