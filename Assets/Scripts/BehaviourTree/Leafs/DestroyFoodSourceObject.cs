using UnityEngine;
using BehaviourTree;

public class DestroyFoodSourceObject : Node
{
    public override NodeStatus Process()
    {
        GameObject instance = (GameObject)GetData("foodSourceObject");
        if (instance != null)
        {
            MonoBehaviour.Destroy(instance);
        }
        return NodeStatus.Success;
    }
}