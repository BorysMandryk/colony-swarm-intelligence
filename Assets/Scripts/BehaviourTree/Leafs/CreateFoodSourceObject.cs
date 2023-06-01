using UnityEngine;
using BehaviourTree;

public class CreateFoodSourceObject : Node
{
    private GameObject prefab;

    public CreateFoodSourceObject(GameObject prefab)
    {
        this.prefab = prefab;
    }

    public override NodeStatus Process()
    {
        GameObject instance = (GameObject)GetData("foodSourceObject");
        if (instance != null)
        {
            MonoBehaviour.Destroy(instance);
        }

        FoodSource foodSource = (FoodSource)GetData("foodSource");
        instance = MonoBehaviour.Instantiate(prefab, foodSource.Position, Quaternion.identity);

        SetData("foodSourceObject", instance);
        return NodeStatus.Success;
    }
}