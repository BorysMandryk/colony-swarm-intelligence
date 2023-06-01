using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ColonyManager : MonoBehaviour
{
    [SerializeField] private GameObject employedPrefab;
    [SerializeField] private GameObject onlookerPrefab;

    private GameObject[] employedBees;
    private GameObject[] onlookerBees;

    private int currentEmployed;
    private int currentOnlooker;

    public event Action OnGridCreated;

    public Grid<HeatMapGridObject> Grid { get; private set; }
        public ArtificialBeeColony Abc { get; private set; }
    public Dictionary<string, object> Blackboard { get; private set; } 
        = new Dictionary<string, object>();
    public static ColonyManager Instance { get; private set; }

    private void Update()
    {
        if (Blackboard.ContainsKey("employedInHive") && Blackboard.ContainsKey("onlookerInHive"))
        {
            currentEmployed = (int)Blackboard["employedInHive"];
            currentOnlooker = (int)Blackboard["onlookerInHive"];
        }
    }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }

    }

    public void CreateGrid(int width, int height, float cellSize, Vector2 originPosition)
    {
        Grid = new Grid<HeatMapGridObject>(width, height, cellSize, originPosition,
            (int x, int y) => new HeatMapGridObject(x, y));
        OnGridCreated?.Invoke();
    }

    public void InitializeAbc(int employedNumber, int onlookerNumber, int maxTrials, int optimizationProblem)
    {
        Vector3 lowerBound = Grid.OriginPosition;
        Vector3 upperBound = Grid.GetWorldPosition(Grid.Width - 1, Grid.Height - 1);

        Abc = new ArtificialBeeColony(employedNumber, onlookerNumber, maxTrials, lowerBound, upperBound,
                    (ArtificialBeeColony.OptimizationProblem)optimizationProblem, (v) => Grid.GetGridObject(v).Value);

        employedBees = new GameObject[employedNumber];
        onlookerBees = new GameObject[onlookerNumber];

        for (int i = 0; i < employedNumber; i++)
        {
            employedBees[i] = Instantiate(employedPrefab, transform.position, Quaternion.identity);
        }

        for (int i = 0; i < onlookerNumber; i++)
        {
            onlookerBees[i] = Instantiate(onlookerPrefab, transform.position, Quaternion.identity);
        }

        Blackboard["employedInHive"] = employedNumber;
        Blackboard["onlookerInHive"] = onlookerNumber;
    }
}