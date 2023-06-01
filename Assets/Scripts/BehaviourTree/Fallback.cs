using System.Collections.Generic;

namespace BehaviourTree
{
    public class Fallback : CombinedNode
    {
        public Fallback(params Node[] children) : base(children)
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
                        return NodeStatus.Success;
                    case NodeStatus.Running:
                        return NodeStatus.Running;
                    case NodeStatus.Failure:
                        break;
                    default:
                        break;
                }
            }

            return NodeStatus.Failure;
        }
    }
}

