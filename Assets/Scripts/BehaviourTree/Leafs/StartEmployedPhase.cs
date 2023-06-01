using BehaviourTree;

public class StartEmployedPhase : Node
{
    public override NodeStatus Process()
    {
        FoodSource foodSource = (FoodSource)GetData("foodSource");
        SetData("destination", foodSource.Position);
        return NodeStatus.Success;
    }
}