using UnityEditor.Animations;

namespace BehaviourTree
{
    public abstract class Decorator : Node
    {
        protected Node child;

        public Decorator(Node child)
        {
            this.child = child;
            child.parent = this;
        }
    }
}

