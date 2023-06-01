using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;




public class ArtificialBeeColony
{
    private int employedBees;
    private int onlookerBees;
    private int maxTrials;
    public enum OptimizationProblem
    {
        Min, Max
    }
    private OptimizationProblem optimizationProblem = OptimizationProblem.Min;

    private Vector3 lowerBound;
    private Vector3 upperBound;

    
    private Func<Vector3, float> objectiveFunction;

    //private FoodSource[] foodSources;
    private List<FoodSource> foodSources = new List<FoodSource>();
    private FoodSource[] onlookerFoodSources;

    //public FoodSource[] FoodSources => foodSources;
    public List<FoodSource> FoodSources => foodSources;
    public FoodSource[] OnlookerFoodSources => onlookerFoodSources;  // Те саме

    private FoodSource gBest;
    //public bool AllFoodSourcesInitialized => employedBees == foodSources.Count;
    //public event Action<Vector3, int> OnEmployedNewPosition;
    //public event Action<Vector3, int> OnOnlookerNewPosition;

    public int EmployedNumber => employedBees;
    public int OnlookerNumber => onlookerBees;
    public int MaxTrials => maxTrials;

    public ArtificialBeeColony(int employedBees, int onlookerBees, int maxTrials, 
        Vector3 lowerBound, Vector3 upperBound,OptimizationProblem optimizationProblem, 
        Func<Vector3, float> objectiveFunction)
    {
        this.employedBees = employedBees;
        this.onlookerBees = onlookerBees;
        this.maxTrials = maxTrials;
        this.lowerBound = lowerBound;
        this.upperBound = upperBound;
        this.optimizationProblem = optimizationProblem;
        this.objectiveFunction = objectiveFunction;
    }

    public float Fit(Vector3 position)
    {
        float fm = objectiveFunction(position);
        if (optimizationProblem == OptimizationProblem.Max)
        {
            fm = -fm;
        }
        
        if (fm >= 0)
        {
            return 1 / (1 + fm);
        }
        else
        {
            return 1 + Mathf.Abs(fm);
        }
    }

    //public void Initialize()
    //{
    //    foodSources = new FoodSource[employedBees];

    //    for (int m = 0; m < employedBees; m++)
    //    {
    //        RandomInitialization(m);
    //    }
    //}

    private void RandomInitialization(int m)
    {
        foodSources[m] = new FoodSource();
        foodSources[m].Position = new Vector3(
                        lowerBound[0] + UnityEngine.Random.Range(0f, 1f) * (upperBound[0] - lowerBound[0]),
                        lowerBound[1] + UnityEngine.Random.Range(0f, 1f) * (upperBound[1] - lowerBound[1]));
        foodSources[m].NewPosition = foodSources[m].Position;
        foodSources[m].Fitness = Fit(foodSources[m].Position);
        foodSources[m].NewFitness = foodSources[m].Fitness;
        foodSources[m].Trial = 0;
    }

    public void RandomInitializeFoodSource(FoodSource foodSource)
    {        
        foodSource.Position = new Vector3(
                        lowerBound[0] + UnityEngine.Random.Range(0f, 1f) * (upperBound[0] - lowerBound[0]),
                        lowerBound[1] + UnityEngine.Random.Range(0f, 1f) * (upperBound[1] - lowerBound[1]));
        foodSource.NewPosition = foodSource.Position;
        foodSource.Fitness = Fit(foodSource.Position);
        foodSource.NewFitness = foodSource.Fitness;
        foodSource.Trial = 0;
    }

    //public void EmployedPhase()
    //{
    //    for (int m = 0; m < employedBees; m++)
    //    {
    //        FindEmployedNeighbourhoodSource(m);
    //    }
    //}

    public void UpdateFoodSources()
    {
        foreach (FoodSource fs in foodSources)
        {
            fs.UpdateFoodSource();
        }
    }


    public Vector3 FindNeighbourFoodSourcePosition(FoodSource foodSource)
    {
        int k;
        do
        {
            k = UnityEngine.Random.Range(0, employedBees);
        } while (foodSources[k].Equals(foodSource));
        int i = UnityEngine.Random.Range(0, 2);
        float phi = UnityEngine.Random.Range(-1f, 1f);

        Vector3 newPosition = foodSource.Position;
        newPosition[i] = foodSource.Position[i] + phi
            * (foodSource.Position[i] - foodSources[k].Position[i]);

        newPosition[i] = Mathf.Clamp(newPosition[i], lowerBound[i], upperBound[i]);

        return newPosition;
    }

    //private void FindEmployedNeighbourhoodSource(int m)
    //{
    //    int k;
    //    do
    //    {
    //        k = UnityEngine.Random.Range(0, employedBees);
    //    } while (k == m);
    //    int i = UnityEngine.Random.Range(0, 2);
    //    float phi = UnityEngine.Random.Range(-1f, 1f);

    //    Vector3 newPosition = foodSources[m].Position;
    //    newPosition[i] = foodSources[m].Position[i] + phi
    //        * (foodSources[m].Position[i] - foodSources[k].Position[i]);

    //    newPosition[i] = Mathf.Clamp(newPosition[i], lowerBound[i], upperBound[i]);

    //    //OnEmployedNewPosition?.Invoke(newPosition, m);

    //    float newFitness = Fit(newPosition);

    //    if (newFitness > foodSources[m].Fitness)
    //    {
    //        foodSources[m].NewPosition = newPosition;
    //        foodSources[m].NewFitness = newFitness;
    //    }
    //}

    //private FoodSource FindNeighbourhoodOnlookerSource(int m, FoodSource[] originalFoodSources)
    //{
    //    int k;
    //    do
    //    {
    //        k = UnityEngine.Random.Range(0, employedBees);
    //    } while (k == m);
    //    int i = UnityEngine.Random.Range(0, 2);
    //    float phi = UnityEngine.Random.Range(-1f, 1f);

    //    Vector3 newPosition = foodSources[m].Position;
    //    newPosition[i] = foodSources[m].Position[i] + phi
    //        * (foodSources[m].Position[i] - foodSources[k].Position[i]);

    //    newPosition[i] = Mathf.Clamp(newPosition[i], lowerBound[i], upperBound[i]);

    //    //OnOnlookerNewPosition.Invoke(newPosition, m);
    //    //employedBeeObjects[m].SetDestination(newPosition);

    //    FoodSource onlooker = (FoodSource)originalFoodSources[m].Clone();

    //    float newFitness = Fit(newPosition);

    //    if (newFitness > foodSources[m].Fitness)
    //    {
    //        onlooker.NewPosition = newPosition;
    //        onlooker.NewFitness = newFitness;
    //        if (newFitness > foodSources[m].NewFitness)
    //        {
    //            foodSources[m].NewPosition = newPosition;
    //            foodSources[m].NewFitness = newFitness;
    //        }
    //    }

    //    return onlooker;
    //}

    public FoodSource RouletteWheelSelection()
    {
        float random = UnityEngine.Random.Range(0f, 1f);

        if (foodSources.Count != employedBees)
        {
            return null;
        }

        float fitnessSum = foodSources.Sum(fs => fs.Fitness);
        float probability = 0f;
        for (int i = 0; i < employedBees; i++)
        {
            probability += foodSources[i].Fitness / fitnessSum;
            if (random <= probability)
            {
                return foodSources[i];
            }
        }
        return null;
    }

    //public void OnlookerPhase()
    //{
    //    float[] probabilites = new float[foodSources.Length];
    //    float fitnessSum = foodSources.Sum(fs => fs.Fitness);

    //    probabilites[0] = foodSources[0].Fitness / fitnessSum;
    //    for (int m = 1; m < foodSources.Length; m++)
    //    {
    //        probabilites[m] = probabilites[m - 1] + foodSources[m].Fitness / fitnessSum;
    //    }
    //    probabilites[foodSources.Length - 1] = 1f;

    //    onlookerFoodSources = new FoodSource[onlookerBees];
    //    FoodSource[] constantFoodSources = (FoodSource[])foodSources.Clone();
    //    for (int m = 0; m < onlookerBees; m++)
    //    {
    //        float random = UnityEngine.Random.Range(0f, 1f);
    //        for (int i = 0; i < employedBees; i++)
    //        {
    //            if (random < probabilites[i])
    //            {
    //                onlookerFoodSources[m] = FindNeighbourhoodOnlookerSource(i, constantFoodSources);
    //                break;
    //            }
    //        }
    //    }


    //    //UpdateFoodSources();
    //}

    //public void ScoutPhase()
    //{
    //    for (int m = 0; m < employedBees; m++)
    //    {
    //        if (foodSources[m].Trial > maxTrials)
    //        {
    //            RandomInitialization(m);
    //        }
    //    }
    //}

    public void AddFoodSource(FoodSource foodSource)
    {
        FoodSources.Add(foodSource);
    }

    public FoodSource GetBestFoodSource()
    {
        if (foodSources.Count != employedBees)
        {
            return null;
        }

        foreach (FoodSource fs in foodSources)
        {
            if (gBest == null || gBest.Fitness < fs.Fitness)
            {
                gBest = (FoodSource)fs.Clone();
            }
        }
        return gBest;
    }
}