using UnityEngine;
using BehaviourTree;

public class OnlookerBeeBT : AgentBT
{
    [SerializeField] private float speed = 10f;
    [SerializeField] private GameObject foodSourcePrefab;

    protected override Node SetupRoot()
    {
        return new Fallback(
                new SequenceWithMemory(
                    new EmployedInHive(),
                    new SelectFoodSourceByProbability(),
                    new Inverter(
                        new ContextBoolCondition("finishedPhase")),
                    new DecrementBlackboardValue("onlookerInHive"),
                    new CreateFoodSourceObject(foodSourcePrefab),
                    new GoToDestination(transform, speed),
                    new SearchNeighbourhood(),
                    new GoToDestination(transform, speed),
                    new UpdateFoodSource(),
                    new CreateFoodSourceObject(foodSourcePrefab),
                    new GoToHive(transform, speed),
                    new IncrementBlackboardValue("onlookerInHive"),
                    new SetContextVariable("finishedPhase", true)),
                new Sequence(
                    new OnlookerInHive(),
                    new Inverter(
                        new EmployedInHive()),
                    new DestroyFoodSourceObject(),
                    new ContextBoolCondition("finishedPhase"),
                    new SetContextVariable("finishedPhase", false))
                );
    }
}
