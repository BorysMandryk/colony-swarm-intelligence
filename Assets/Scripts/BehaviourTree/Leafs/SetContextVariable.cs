using UnityEngine;
using BehaviourTree;

public class SetContextVariable : Node
{
    private string variableName;
    private object value;

    public SetContextVariable(string variableName, object value)
    {
        this.variableName = variableName;
        this.value = value;
    }

    public override NodeStatus Process()
    {
        SetData(variableName, value);
        return NodeStatus.Success;
    }
}