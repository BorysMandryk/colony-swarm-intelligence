using BehaviourTree;

public class UpdateFoodSource : Node
{
    public override NodeStatus Process()
    {
        FoodSource foodSource = (FoodSource)GetData("foodSource");
        foodSource.UpdateFoodSource();
        return NodeStatus.Success;
    }
}