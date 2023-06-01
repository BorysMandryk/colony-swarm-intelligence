using BehaviourTree;

public class AddFoodSourceToList : Node
{
    public override NodeStatus Process()
    {
        FoodSource foodSource = (FoodSource)GetData("foodSource");
        ColonyManager.Instance.Abc.AddFoodSource(foodSource);
        return NodeStatus.Success;
    }
}