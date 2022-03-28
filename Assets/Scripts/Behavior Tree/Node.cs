using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace BehaviorTree
{
    using BehaviorTree.Actions;
    public enum NodeState
    {
        RUNNING,
        SUCCESS,
        FAILURE
    }

    public abstract class Node
    {
        private NodeState state;
        
        public abstract NodeState Tick();
    }
}