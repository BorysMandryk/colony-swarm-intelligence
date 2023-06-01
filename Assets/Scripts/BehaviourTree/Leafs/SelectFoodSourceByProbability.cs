using BehaviourTree;

public class SelectFoodSourceByProbability : Node
{
    public override NodeStatus Process()
    {
        FoodSource foodSource = ColonyManager.Instance.Abc.RouletteWheelSelection();
        if (foodSource == null)
        {
            return NodeStatus.Failure;
        }
        SetData("foodSource", foodSource);
        SetData("destination", foodSource.Position);
        return NodeStatus.Success;
    }
}