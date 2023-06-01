using UnityEngine;

namespace BehaviourTree
{
    public class SequenceWithMemory : CombinedNode
    {
        private int currentChild;

        public SequenceWithMemory(params Node[] children) : base(children)
        {
        }

        public override NodeStatus Process()
        {
            for (int i = currentChild; i < children.Count; i++)
            {
                NodeStatus childStatus = children[i].Process();
                Debug.Log(currentChild + " " + childStatus);
                switch (childStatus)
                {
                    case NodeStatus.Success:
                        currentChild = i + 1;
                        break;
                    case NodeStatus.Running:
                        return NodeStatus.Running;
                    case NodeStatus.Failure:
                        currentChild = 0;
                        return NodeStatus.Failure;
                    default:
                        break;
                }
            }

            currentChild = 0;
            return NodeStatus.Success;
        }
    }
}

