using BehaviourTree;

public class CheckTrialsLessThanLimit : Node
{
    public override NodeStatus Process()
    {
        FoodSource foodSource = (FoodSource)GetData("foodSource");

        if (foodSource.Trial > ColonyManager.Instance.Abc.MaxTrials)
        {
            return NodeStatus.Failure;
        }
        return NodeStatus.Success;
    }
}
