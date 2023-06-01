using UnityEngine;
using BehaviourTree;

public class ContextBoolCondition : Node
{
    private string variableName;

    public ContextBoolCondition(string variableName)
    {
        this.variableName = variableName;
    }

    public override NodeStatus Process()
    {
        object value = GetData(variableName);
        if (value == null)
        {
            return NodeStatus.Failure;
        }
        if (!(bool)value)
        {
            return NodeStatus.Failure;
        }

        return NodeStatus.Success;
    }
}