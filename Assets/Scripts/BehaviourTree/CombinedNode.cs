using System.Collections.Generic;

namespace BehaviourTree
{
    public abstract class CombinedNode : Node
    {
        protected List<Node> children = new List<Node>();

        public CombinedNode(params Node[] children)
        {
            foreach (Node child in children)
            {
                AddChild(child);
            }
        }

        private void AddChild(Node child)
        {
            child.parent = this;
            children.Add(child);
        }

    }
}

