using UnityEngine;
using BehaviourTree;

public class SearchNeighbourhood : Node
{
    public override NodeStatus Process()
    {
        FoodSource foodSource = (FoodSource)GetData("foodSource");

        Vector3 newPosition = ColonyManager.Instance.Abc.FindNeighbourFoodSourcePosition(foodSource);
        SetData("destination", newPosition);

        float newFitness = ColonyManager.Instance.Abc.Fit(newPosition);

        if (newFitness > foodSource.Fitness)
        {
            foodSource.NewPosition = newPosition;
            foodSource.NewFitness = newFitness;
        }

        return NodeStatus.Success;
    }
}