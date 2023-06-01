using UnityEngine;

namespace BehaviourTree
{
    public class Inverter : Decorator
    {
        public Inverter(Node child) : base(child)
        {
        }

        public override NodeStatus Process()
        {
            NodeStatus childStatus = child.Process();
            switch (childStatus)
            {
                case NodeStatus.Success:
                    return NodeStatus.Failure;
                case NodeStatus.Failure:
                    return NodeStatus.Success;
                default:
                    return NodeStatus.Running;
            }
        }
    }
}

