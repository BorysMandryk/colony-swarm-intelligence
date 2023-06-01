using BehaviourTree;

public class IncrementBlackboardValue : Node
{
    private string variableName;

    public IncrementBlackboardValue(string variableName)
    {
        this.variableName = variableName;
    }

    public override NodeStatus Process()
    {
        object value;
        if (ColonyManager.Instance.Blackboard.TryGetValue(variableName, out value))
        {
            ColonyManager.Instance.Blackboard[variableName] = (int)value + 1;
        }
        else
        {
            ColonyManager.Instance.Blackboard[variableName] = 1;
        }

        return NodeStatus.Success;
    }
}