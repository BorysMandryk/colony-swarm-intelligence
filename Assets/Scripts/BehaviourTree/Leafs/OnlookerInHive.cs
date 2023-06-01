using UnityEngine;
using BehaviourTree;

public class OnlookerInHive : Node
{
    public override NodeStatus Process()
    {
        object value;
        if (ColonyManager.Instance.Blackboard.TryGetValue("onlookerInHive", out value))
        {
            if ((int)value == ColonyManager.Instance.Abc.OnlookerNumber)
            {
                return NodeStatus.Success;
            }
        }

        return NodeStatus.Failure;
    }
}