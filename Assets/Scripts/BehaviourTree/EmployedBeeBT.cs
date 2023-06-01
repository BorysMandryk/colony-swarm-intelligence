using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviourTree;

public class EmployedBeeBT : AgentBT
{
    [SerializeField] private float speed = 10f;
    [SerializeField] private GameObject foodSourcePrefab;

    protected override Node SetupRoot()
    {
        return new Sequence(
            new Fallback(
                new ContextVariableExistsCondition("foodSource"),
                new Sequence(        
                    new SetContextVariable("foodSource", new FoodSource()),
                    new RandomFoodSourceInitialization(),
                    new AddFoodSourceToList())),
            new Fallback(              
                new SequenceWithMemory(
                    new Inverter(
                        new ContextBoolCondition("finishedPhase")),
                    new OnlookerInHive(),
                    new StartEmployedPhase(),
                    new DecrementBlackboardValue("employedInHive"),
                    new CreateFoodSourceObject(foodSourcePrefab),
                    new GoToDestination(transform, speed),
                    new SearchNeighbourhood(),
                    new GoToDestination(transform, speed),
                    new UpdateFoodSource(),
                    new CreateFoodSourceObject(foodSourcePrefab),
                    new GoToHive(transform, speed),
                    new IncrementBlackboardValue("employedInHive"),
                    new SetContextVariable("finishedPhase", true)),
                new Sequence(
                    new EmployedInHive(),
                    new Inverter(
                        new OnlookerInHive()),
                    new DestroyFoodSourceObject(),
                    new SetContextVariable("finishedPhase", false))
                ),
            new Fallback(
                new CheckTrialsLessThanLimit(),
                new RandomFoodSourceInitialization())
            );
    }
}
