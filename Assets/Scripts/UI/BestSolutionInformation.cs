using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BestSolutionInformation : MonoBehaviour
{
    [SerializeField] private TMP_Text bestFoodSourceText;

    public void Update()
    {
        FoodSource foodSource = ColonyManager.Instance.Abc?.GetBestFoodSource();
        if (foodSource == null)
        {
            return;
        }
        bestFoodSourceText.text = $"Best Solution:\n" +
            $"Coordinates: {ColonyManager.Instance.Grid.GetXY((Vector2)foodSource.Position)}\n" +
            $"Position: {(Vector2)foodSource.Position}\n" +
            $"Value: {ColonyManager.Instance.Grid.GetGridObject(foodSource.Position).Value}";
    }
}
