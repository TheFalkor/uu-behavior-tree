using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace BehaviorTree
{
    public enum NodeState
    {
        RUNNING,
        SUCCESS,
        FAILURE
    }

    public abstract class Node : MonoBehaviour
    {
        private NodeState state;
        
        public abstract NodeState Tick();
    }
}