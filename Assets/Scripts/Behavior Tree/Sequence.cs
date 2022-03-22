using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviorTree
{
    public class Sequence : Node
    {
        private List<Node> nodeList = new List<Node>();
        
        public Sequence(List<Node> nodes)
        {
            nodeList = nodes;
        }

        public override NodeState Tick()
        {
            foreach (Node node in nodeList)
            {
                NodeState result = node.Tick();

                if (result == NodeState.FAILURE || result == NodeState.RUNNING)
                    return result;
            }

            return NodeState.SUCCESS;
        }
    }
}

