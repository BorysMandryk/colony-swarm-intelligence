using UnityEngine;
using BehaviourTree;

public class GoToHive : Node
{
    private Transform transform;
    private float speed;

    public GoToHive(Transform transform, float speed)
    {
        this.transform = transform;
        this.speed = speed;
    }

    public override NodeStatus Process()
    {
        Vector3 destination = ColonyManager.Instance.transform.position;
        SetData("destination", destination);

        if (Vector3.Distance(transform.position, destination) < 0.01f)
        {
            FoodSource foodSource = (FoodSource)GetData("foodSource");
            SetData("destination", foodSource.Position);

            return NodeStatus.Success;
        }

        transform.position = Vector3.MoveTowards(transform.position, destination, speed * Time.deltaTime);
        return NodeStatus.Running;
    }
}