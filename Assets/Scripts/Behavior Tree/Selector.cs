using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviorTree
{
    public class Selector : Node
    {
        private List<Node> nodeList = new List<Node>();

        public Selector(List<Node> nodes)
        {
            nodeList = nodes;
        }

        public override NodeState Tick()
        {
            foreach (Node node in nodeList)
            {
                NodeState result = node.Tick();

                if (result == NodeState.SUCCESS || result == NodeState.RUNNING)
                    return result;
            }

            return NodeState.FAILURE;
        }
    }
}
