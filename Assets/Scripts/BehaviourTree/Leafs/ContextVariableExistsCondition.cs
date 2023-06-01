using BehaviourTree;

public class ContextVariableExistsCondition : Node
{
    private string variableName;

    public ContextVariableExistsCondition(string variableName)
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

        return NodeStatus.Success;
    }
}