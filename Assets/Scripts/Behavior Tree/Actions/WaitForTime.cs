using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace BehaviorTree.Actions
{
    public class WaitForTime : Node
    {
        private float waitTime;
        private float currentWaitTime = 0;

        public WaitForTime(float waitTime)
        {
            this.waitTime = waitTime;
            currentWaitTime = waitTime;
        }

        public override NodeState Tick(float deltaTime)
        {
            if (currentWaitTime <= 0)
            {
                currentWaitTime = waitTime;
                return NodeState.SUCCESS;
            }
            else
            {
                currentWaitTime -= deltaTime;
                return NodeState.RUNNING;
            }
        }
    }
}
