using BehaviourTree;

public class RandomFoodSourceInitialization : Node
{
    public override NodeStatus Process()
    {
        FoodSource foodSource = (FoodSource)GetData("foodSource");
        ColonyManager.Instance.Abc.RandomInitializeFoodSource(foodSource);
        return NodeStatus.Success;
    }
}
