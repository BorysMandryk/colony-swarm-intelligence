using UnityEngine;
using BehaviourTree;

public class EmployedInHive : Node
{
    public override NodeStatus Process()
    {
        object value;
        if (ColonyManager.Instance.Blackboard.TryGetValue("employedInHive", out value))           
        {
            if ((int)value == ColonyManager.Instance.Abc.EmployedNumber)
            {
                Debug.Log(NodeStatus.Success);
                return NodeStatus.Success;
            }
        }

        return NodeStatus.Failure;
    }
}