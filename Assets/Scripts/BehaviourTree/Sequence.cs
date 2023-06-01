using UnityEngine;

namespace BehaviourTree
{
    public class Sequence : CombinedNode
    {
        public Sequence(params Node[] children) : base(children)
        {
        }

        public override NodeStatus Process()
        {
            foreach (Node child in children)
            {
                NodeStatus childStatus = child.Process();
                switch (childStatus)
                {
                    case NodeStatus.Success:
                        break;
                    case NodeStatus.Running:
                        return NodeStatus.Running;
                    case NodeStatus.Failure:
                        return NodeStatus.Failure;
                    default:
                        break;
                }
            }

            return NodeStatus.Success;
        }
    }
}

